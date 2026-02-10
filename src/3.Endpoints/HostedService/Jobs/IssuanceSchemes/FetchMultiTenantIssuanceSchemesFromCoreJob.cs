using Master.Data.Core.RequestResponse.IssuanceSchemes.Commands.Fetch;
using Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob;
using Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Models;
using Microsoft.Extensions.Options;

namespace Master.Data.Endpoints.HostedService.Jobs.IssuanceSchemes;

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
