using Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Constants;
using Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Models;
using Microsoft.Extensions.Options;
using Zamin.Core.Contracts.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;

namespace Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob;

public abstract class BaseBackgroundJob<TCommand, TJob> : BackgroundService
    where TCommand : class, ICommand, new()
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    protected readonly ILogger<BaseBackgroundJob<TCommand, TJob>> _logger;
    private readonly IOptions<JobScheduleOption> _jobScheduleOptions;
    protected readonly string _jobName;

    public BaseBackgroundJob(IServiceScopeFactory serviceScopeFactory,
                             ILogger<BaseBackgroundJob<TCommand, TJob>> logger,
                             IOptions<JobScheduleOption> jobScheduleOptions)
    {
        _jobName = typeof(TJob).Name;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        _jobScheduleOptions = jobScheduleOptions;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var jobOption = GetJobOption();
        if (jobOption == null)
        {
            _logger.LogWarning("Job configuration not found for: {JobName}", _jobName);
            return;
        }

        _logger.LogInformation("Starting job: {JobName} with schedule type: {ScheduleType}",
                               _jobName,
                               jobOption.ScheduleType);

        // Initial delay for scheduled jobs
        if (!jobOption.StartImmediately)
        {
            var initialDelay = GetInitialDelay(jobOption);
            if (initialDelay > TimeSpan.Zero)
            {
                _logger.LogInformation("Job {JobName} waiting {Delay} before first run",
                                       _jobName,
                                       initialDelay);
                await Task.Delay(initialDelay, stoppingToken);
            }
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();
            var scheduler = scope.ServiceProvider.GetRequiredService<IJobSchedulerService>();
            var dispatcher = scope.ServiceProvider.GetService<ICommandDispatcher>();

            if (scheduler.ShouldRunNow(jobOption))
            {
                await ExecuteJobCycle(dispatcher, jobOption, stoppingToken);
            }
            else
            {
                var nextRun = scheduler.GetNextRunTime(jobOption);
                if (nextRun.HasValue)
                {
                    var delay = nextRun.Value - DateTime.Now;
                    if (delay > TimeSpan.Zero)
                    {
                        _logger.LogInformation("Job {JobName} next run at {NextRun}, waiting {Delay}",
                                               _jobName,
                                               nextRun.Value,
                                               delay);
                        await Task.Delay(delay, stoppingToken);
                    }
                }
                else
                {
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
            }
        }

        LogInformation(JobState.Stopped);
    }

    private async Task ExecuteJobCycle(ICommandDispatcher dispatcher,
                                       JobOption jobOption,
                                       CancellationToken stoppingToken)
    {
        using (_logger.BeginScope("CorrelationId is {correlationId}",
                                  Guid.NewGuid().ToString()))
        {
            try
            {
                DateTime startDateTime = DateTime.Now;
                _logger.LogInformation("Job name: '{jobName}' started at '{startDateTime}'",
                                       _jobName,
                                       startDateTime);

                if (dispatcher is not null)
                {
                    await dispatcher.Send(new TCommand());
                }
                else
                {
                    _logger.LogWarning("CommandDispatcher is null for job: {JobName}", _jobName);
                }

                DateTime endDateTime = DateTime.Now;
                _logger.LogInformation("Job name: '{jobName}' end at '{endDateTime}' ({totalSeconds} s)",
                                       _jobName,
                                       endDateTime,
                                       (endDateTime - startDateTime).TotalSeconds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing job: {JobName}", _jobName);
            }
        }

        LogInformation(JobState.Done);

        // Wait for next period based on schedule type
        if (jobOption.ScheduleType == ScheduleType.Simple && jobOption.Period.HasValue)
        {
            await Task.Delay(TimeSpan.FromMinutes(jobOption.Period.Value), stoppingToken);
        }
        else
        {
            // For advanced and cron, wait 1 minute and re-evaluate
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private JobOption? GetJobOption()
    {
        var jobOption = _jobScheduleOptions.Value.Jobs
            .FirstOrDefault(j => j.Name.Equals(_jobName, StringComparison.OrdinalIgnoreCase));

        if (jobOption is null)
            _logger.LogWarning("Job configuration not found for: {JobName}. Available jobs: {AvailableJobs}",
                               _jobName,
                               string.Join(", ", _jobScheduleOptions.Value.Jobs.Select(j => j.Name)));
        else
            ValidationBeforeSetJob(jobOption);

        return jobOption;
    }

    private void ValidationBeforeSetJob(JobOption job)
    {
        if (job.ScheduleType == ScheduleType.Simple)
        {
            if (job.Hour < 0 || job.Hour > 23)
            {
                throw new ArgumentException(_jobName + " Hour must be between 0 and 23");
            }

            if (job.Minute < 0 || job.Minute > 59)
            {
                throw new ArgumentException(_jobName + " Minute must be between 0 and 59");
            }
        }
        else if (job.ScheduleType == ScheduleType.Cron)
        {
            if (string.IsNullOrEmpty(job.CronExpression))
            {
                throw new ArgumentException(_jobName + " CronExpression is required for Cron schedule type");
            }
        }
    }

    private TimeSpan GetInitialDelay(JobOption jobOption)
    {
        var nextRun = GetNextRunTime(jobOption);
        return nextRun.HasValue ? nextRun.Value - DateTime.Now : TimeSpan.Zero;
    }

    private DateTime? GetNextRunTime(JobOption jobOption)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var scheduler = scope.ServiceProvider.GetRequiredService<IJobSchedulerService>();
        return scheduler.GetNextRunTime(jobOption);
    }

    protected virtual void LogInformation(JobState state)
        => _logger.LogInformation("{job} {state} at {time}",
                                  _jobName,
                                  state,
                                  DateTime.Now);
}