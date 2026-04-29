using Vehicle.Insurance.Core.RequestResponse.Provinces.Commands.Fetch;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.BackgroundJob;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Models;
using Microsoft.Extensions.Options;

namespace Vehicle.Insurance.Endpoints.HostedService.Jobs.Provinces;

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

