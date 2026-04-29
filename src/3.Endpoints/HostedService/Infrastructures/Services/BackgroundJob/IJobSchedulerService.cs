using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Models;

namespace Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.BackgroundJob;

public interface IJobSchedulerService
{
    DateTime? GetNextRunTime(JobOption job);
    bool ShouldRunNow(JobOption job);
    TimeSpan GetTimeUntilNextRun(JobOption job);
}
