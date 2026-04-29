using Vehicle.Insurance.Core.RequestResponse.AgreementObligations.Commands.Fetch;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.BackgroundJob;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Models;
using Microsoft.Extensions.Options;

namespace Vehicle.Insurance.Endpoints.HostedService.Jobs.AgreementObligations;

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

