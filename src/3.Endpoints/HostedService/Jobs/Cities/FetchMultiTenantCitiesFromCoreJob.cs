using Master.Data.Core.RequestResponse.Cities.Commands.Fetch;
using Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob;
using Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Models;
using Microsoft.Extensions.Options;

namespace Master.Data.Endpoints.HostedService.Jobs.Cities;

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
