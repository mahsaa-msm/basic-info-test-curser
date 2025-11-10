using Master.Data.Core.RequestResponse.Provinces.Commands.Fetch;
using Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob;
using Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Models;
using Microsoft.Extensions.Options;

namespace Master.Data.Endpoints.HostedService.Jobs.Provinces;

public sealed class FetchMultiTenantProvincesFromCoreJob :
    BaseBackgroundJob<FetchMultiTenantProvincesFromSourceCommand, FetchMultiTenantProvincesFromCoreJob>
{
    public FetchMultiTenantProvincesFromCoreJob(IServiceScopeFactory serviceScopeFactory,
                                                ILogger<FetchMultiTenantProvincesFromCoreJob> logger,
                                                IOptions<JobScheduleOption> jobSchedule)
        : base(serviceScopeFactory, logger, jobSchedule)
    {
    }
}
