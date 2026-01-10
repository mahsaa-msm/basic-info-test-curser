using Master.Data.Core.RequestResponse.Countries.Commands.Fetch;
using Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob;
using Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Models;
using Microsoft.Extensions.Options;

namespace Master.Data.Endpoints.HostedService.Jobs.Countries;

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
