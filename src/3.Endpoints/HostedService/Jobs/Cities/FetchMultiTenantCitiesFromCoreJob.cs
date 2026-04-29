using Vehicle.Insurance.Core.RequestResponse.Cities.Commands.Fetch;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.BackgroundJob;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Models;
using Microsoft.Extensions.Options;

namespace Vehicle.Insurance.Endpoints.HostedService.Jobs.Cities;

public sealed class FetchMultiTenantCitiesFromCoreJob :
    BaseBackgroundJob<FetchMultiTenantCitiesFromSourceCommand, FetchMultiTenantCitiesFromCoreJob>
{
    public FetchMultiTenantCitiesFromCoreJob(IServiceScopeFactory serviceScopeFactory,
                                                ILogger<FetchMultiTenantCitiesFromCoreJob> logger,
                                                IOptions<JobScheduleOption> jobSchedule)
        : base(serviceScopeFactory, logger, jobSchedule)
    {
    }
}

