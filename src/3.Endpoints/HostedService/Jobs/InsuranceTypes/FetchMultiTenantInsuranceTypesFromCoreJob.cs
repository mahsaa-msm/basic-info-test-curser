using Master.Data.Core.RequestResponse.InsuranceTypes.Commands.Fetch;
using Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob;
using Master.Data.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Models;
using Microsoft.Extensions.Options;

namespace Master.Data.Endpoints.HostedService.Jobs.InsuranceTypes;

public sealed class FetchMultiTenantInsuranceTypesFromCoreJob :
    BaseBackgroundJob<FetchMultiTenantInsuranceTypesFromSourceCommand, FetchMultiTenantInsuranceTypesFromCoreJob>
{
    public FetchMultiTenantInsuranceTypesFromCoreJob(IServiceScopeFactory serviceScopeFactory,
                                                ILogger<FetchMultiTenantInsuranceTypesFromCoreJob> logger,
                                                IOptions<JobScheduleOption> jobSchedule)
        : base(serviceScopeFactory, logger, jobSchedule)
    {
    }
}
