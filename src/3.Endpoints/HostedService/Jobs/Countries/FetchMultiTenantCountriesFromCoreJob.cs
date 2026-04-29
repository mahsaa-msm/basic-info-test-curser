using Vehicle.Insurance.Core.RequestResponse.Countries.Commands.Fetch;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.BackgroundJob;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Models;
using Microsoft.Extensions.Options;

namespace Vehicle.Insurance.Endpoints.HostedService.Jobs.Countries;

public sealed class FetchMultiTenantCountriesFromCoreJob :
    BaseBackgroundJob<FetchMultiTenantCountriesFromSourceCommand, FetchMultiTenantCountriesFromCoreJob>
{
    public FetchMultiTenantCountriesFromCoreJob(IServiceScopeFactory serviceScopeFactory,
                                                ILogger<FetchMultiTenantCountriesFromCoreJob> logger,
                                                IOptions<JobScheduleOption> jobSchedule)
        : base(serviceScopeFactory, logger, jobSchedule)
    {
    }
}

