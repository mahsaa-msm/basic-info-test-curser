using Vehicle.Insurance.Core.RequestResponse.IssuanceSchemes.Commands.Fetch;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.BackgroundJob;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Models;
using Microsoft.Extensions.Options;

namespace Vehicle.Insurance.Endpoints.HostedService.Jobs.IssuanceSchemes;

public sealed class FetchMultiTenantIssuanceSchemesFromCoreJob :
    BaseBackgroundJob<FetchMultiTenantIssuanceSchemesFromSourceCommand, FetchMultiTenantIssuanceSchemesFromCoreJob>
{
    public FetchMultiTenantIssuanceSchemesFromCoreJob(IServiceScopeFactory serviceScopeFactory,
                                                ILogger<FetchMultiTenantIssuanceSchemesFromCoreJob> logger,
                                                IOptions<JobScheduleOption> jobSchedule)
        : base(serviceScopeFactory, logger, jobSchedule)
    {
    }
}

