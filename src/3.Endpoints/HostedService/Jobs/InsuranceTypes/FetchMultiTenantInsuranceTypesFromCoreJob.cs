using Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Commands.Fetch;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.BackgroundJob;
using Vehicle.Insurance.Endpoints.HostedService.Infrastructures.Services.BackgroundJob.Models;
using Microsoft.Extensions.Options;

namespace Vehicle.Insurance.Endpoints.HostedService.Jobs.InsuranceTypes;

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

