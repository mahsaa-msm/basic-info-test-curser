using Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Constants;
using Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Models;
using NCrontab;
using Zamin.Extensions.DependencyInjection.Abstractions;

namespace Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob;

public class JobSchedulerService : IJobSchedulerService, IScopeLifetime
{
    private readonly ILogger<JobSchedulerService> _logger;

    public JobSchedulerService(ILogger<JobSchedulerService> logger)
    {
        _logger = logger;
    }

    public bool ShouldRunNow(JobOption job)
    {
        if (!job.IsEnabled) return false;

        return job.ScheduleType switch
        {
            ScheduleType.Simple => IsSimpleScheduleAllowed(job),
            ScheduleType.Advanced => IsAdvancedScheduleAllowed(job),
            ScheduleType.Cron => IsCronScheduleAllowed(job),
            _ => false
        };
    }

    public DateTime? GetNextRunTime(JobOption job)
    {
        if (!job.IsEnabled) return null;

        return job.ScheduleType switch
        {
            ScheduleType.Simple => GetNextSimpleRunTime(job),
            ScheduleType.Advanced => GetNextAdvancedRunTime(job),
            ScheduleType.Cron => GetNextCronRunTime(job),
            _ => null
        };
    }

    public TimeSpan GetTimeUntilNextRun(JobOption job)
    {
        var nextRun = GetNextRunTime(job);
        return nextRun.HasValue ? nextRun.Value - DateTime.Now : TimeSpan.MaxValue;
    }

    #region Simple Schedule Logic
    private bool IsSimpleScheduleAllowed(JobOption job)
    {
        if (job.StartImmediately) return true;
        if (!job.Period.HasValue) return false;

        var now = DateTime.Now;
        if (job.Hour.HasValue && job.Hour != now.Hour) return false;
        if (job.Minute.HasValue && job.Minute != now.Minute) return false;

        return true;
    }

    private DateTime? GetNextSimpleRunTime(JobOption job)
    {
        if (!job.Period.HasValue) return null;

        var now = DateTime.Now;
        var nextRun = now;

        if (job.Hour.HasValue && job.Minute.HasValue)
        {
            nextRun = new DateTime(now.Year, now.Month, now.Day, job.Hour.Value, job.Minute.Value, 0);
            if (nextRun < now)
            {
                nextRun = nextRun.AddDays(1);
            }
        }
        else
        {
            nextRun = now.AddMinutes(job.Period.Value);
        }

        return nextRun;
    }
    #endregion

    #region Advanced Schedule Logic
    private bool IsAdvancedScheduleAllowed(JobOption job)
    {
        var now = DateTime.Now;

        if (!IsInActiveDateRange(job, now)) return false;
        if (!IsInAllowedDays(job, now)) return false;

        return IsInAllowedTimeWindow(job, now);
    }

    private DateTime? GetNextAdvancedRunTime(JobOption job)
    {
        var current = DateTime.Now;

        for (int i = 0; i < 365; i++)
        {
            var checkDate = current.AddDays(i);

            if (!IsInActiveDateRange(job, checkDate) || !IsInAllowedDays(job, checkDate))
                continue;

            var dayRestrictions = job.TimeRestrictions
                .Where(r => r.DayOfWeek == checkDate.DayOfWeek && !IsExcludedDate(r, checkDate))
                .OrderBy(r => r.StartTime)
                .ToList();

            foreach (var restriction in dayRestrictions)
            {
                var restrictionStart = checkDate.Date + restriction.StartTime;

                if (restrictionStart > current)
                    return restrictionStart;
            }
        }

        return null;
    }

    private bool IsInActiveDateRange(JobOption job, DateTime date)
    {
        if (job.ActiveDateRange == null) return true;

        var afterStart = !job.ActiveDateRange.StartDate.HasValue ||
                       date >= job.ActiveDateRange.StartDate.Value.Date;
        var beforeEnd = !job.ActiveDateRange.EndDate.HasValue ||
                      date <= job.ActiveDateRange.EndDate.Value.Date;

        return afterStart && beforeEnd;
    }

    private bool IsInAllowedDays(JobOption job, DateTime date)
    {
        if (!job.AllowedDays.Any()) return true;
        return job.AllowedDays.Contains(date.DayOfWeek);
    }

    private bool IsInAllowedTimeWindow(JobOption job, DateTime dateTime)
    {
        var dayRestrictions = job.TimeRestrictions
            .Where(r => r.DayOfWeek == dateTime.DayOfWeek && !IsExcludedDate(r, dateTime))
            .ToList();

        if (!dayRestrictions.Any()) return false;

        var currentTime = dateTime.TimeOfDay;
        return dayRestrictions.Any(r => currentTime >= r.StartTime && currentTime <= r.EndTime);
    }

    private bool IsExcludedDate(TimeRestriction restriction, DateTime date)
    {
        return restriction.ExcludedDates.Any(excluded => excluded.Date == date.Date);
    }
    #endregion

    #region Cron Schedule Logic
    private bool IsCronScheduleAllowed(JobOption job)
    {
        if (!job.CronExpressions.Any()) return false;

        foreach (var cronExpression in job.CronExpressions)
        {
            try
            {
                var expression = CrontabSchedule.Parse(cronExpression);
                var nextOccurrence = expression.GetNextOccurrence(DateTime.Now.AddMinutes(-1));
                if (nextOccurrence <= DateTime.Now && DateTime.Now < nextOccurrence.AddMinutes(1))
                    return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Invalid cron expression: {CronExpression}", cronExpression);
            }
        }

        return false;
    }

    private DateTime? GetNextCronRunTime(JobOption job)
    {
        if (!job.CronExpressions.Any()) return null;

        DateTime? nextRun = null;

        foreach (var cronExpression in job.CronExpressions)
        {
            try
            {
                var expression = CrontabSchedule.Parse(cronExpression);
                var occurrence = expression.GetNextOccurrence(DateTime.Now);

                if (nextRun == null || occurrence < nextRun)
                    nextRun = occurrence;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Invalid cron expression: {CronExpression}", cronExpression);
            }
        }

        return nextRun;
    }

    public List<DateTime> GetAllCronOccurrences(JobOption job, DateTime from, DateTime to)
    {
        var allOccurrences = new List<DateTime>();

        if (!job.CronExpressions.Any()) return allOccurrences;

        foreach (var cronExpression in job.CronExpressions)
        {
            try
            {
                var expression = CrontabSchedule.Parse(cronExpression);
                var occurrences = expression.GetNextOccurrences(from, to);
                allOccurrences.AddRange(occurrences);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Invalid cron expression: {CronExpression}", cronExpression);
            }
        }

        return allOccurrences.OrderBy(d => d).ToList();
    }

    public bool IsAnyCronExpressionValid(JobOption job)
    {
        if (!job.CronExpressions.Any()) return false;

        foreach (var cronExpression in job.CronExpressions)
        {
            try
            {
                CrontabSchedule.Parse(cronExpression);
                return true;
            }
            catch
            {
                // Continue checking other expressions
            }
        }

        return false;
    }

    #endregion
}