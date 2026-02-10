using Master.Data.Core.RequestResponse.AgreementObligations.Commands.Fetch;
using Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob;
using Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Models;
using Microsoft.Extensions.Options;

namespace Master.Data.Endpoints.HostedService.Jobs.AgreementObligations;

public sealed class FetchMultiTenantAgreementObligationsFromCoreJob :
    BaseBackgroundJob<FetchMultiTenantAgreementObligationsFromSourceCommand, FetchMultiTenantAgreementObligationsFromCoreJob>
{
    public FetchMultiTenantAgreementObligationsFromCoreJob(IServiceScopeFactory serviceScopeFactory,
                                                           ILogger<FetchMultiTenantAgreementObligationsFromCoreJob> logger,
                                                           IOptions<JobScheduleOption> jobSchedule)
        : base(serviceScopeFactory, logger, jobSchedule)
    {
    }
}
