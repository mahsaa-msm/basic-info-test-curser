using Master.Data.Core.ApplicationService.Common.FinglishConverterService;
using Master.Data.Core.Contracts.CoreInsuranceApis.InsuranceTypes;
using Master.Data.Core.Contracts.InsuranceTypes.Commands;
using Master.Data.Core.Contracts.Tenants.Queries;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.InsuranceTypes.Entities;
using Master.Data.Core.Domain.InsuranceTypes.Parameters;
using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.InsuranceType.GetAll;
using Master.Data.Core.RequestResponse.InsuranceTypes.Commands.Fetch;
using Master.Data.Core.RequestResponse.Tenants.Queries.GetIAllSelectItem;
using Master.Data.Core.Resources;
using Microsoft.Extensions.Logging;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Common;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.InsuranceTypes.Commands.Fetch;

public sealed class FetchMultiTenantInsuranceTypesFromSourceHandler : CommandHandler<FetchMultiTenantInsuranceTypesFromSourceCommand>
{
    private readonly IInsuranceTypeCommandRepository _commandRepository;
    private readonly ITenantQueryRepository _tenantQueryRepository;
    private readonly ICoreInsuranceGetAllInsuranceTypesCaller _coreInsuranceGetAllInsuranceTypesCaller;
    private readonly ILogger<FetchMultiTenantInsuranceTypesFromSourceHandler> _logger;
    private readonly IFinglishConverter _finglishConverter;

    public FetchMultiTenantInsuranceTypesFromSourceHandler(ZaminServices zaminServices,
                                           IInsuranceTypeCommandRepository commandRepository,
                                           ITenantQueryRepository tenantQueryRepository,
                                           ICoreInsuranceGetAllInsuranceTypesCaller coreInsuranceGetAllInsuranceTypesCaller,
                                           ILogger<FetchMultiTenantInsuranceTypesFromSourceHandler> logger,
                                           IFinglishConverter finglishConverter)
        : base(zaminServices)
    {
        _commandRepository = commandRepository;
        _tenantQueryRepository = tenantQueryRepository;
        _coreInsuranceGetAllInsuranceTypesCaller = coreInsuranceGetAllInsuranceTypesCaller;
        _logger = logger;
        _finglishConverter = finglishConverter;
    }

    public override async Task<CommandResult> Handle(FetchMultiTenantInsuranceTypesFromSourceCommand command)
    {
        var coreInsuranceTypesResponse = await _coreInsuranceGetAllInsuranceTypesCaller.Call(new GetAllInsuranceTypesRequest());

        if (coreInsuranceTypesResponse.IsFailure ||
            coreInsuranceTypesResponse.Value is null ||
            !coreInsuranceTypesResponse.Value.Any())
        {
            _logger.LogWarning(string.Format(ProjectTranslation.FETCH_DATA_FROM_CORE_FAILED,
                                             ProjectTranslation.INSURANCE_TYPE));
            return Result(ApplicationServiceStatus.NotFound);
        }

        var tenants = await _tenantQueryRepository.ExecuteAsync(new GetAllTenantsSelectItemQuery());

        long nextPriority = await _commandRepository.GetNextPriority();

        foreach (var tenant in tenants)
        {
            var tenantInsuranceTypes = await _commandRepository.GetByTenantId(tenant.Id);

            foreach (var coreInsuranceType in coreInsuranceTypesResponse.Value)
            {
                var insuranceType = tenantInsuranceTypes
                    .FirstOrDefault(c => c.CoreId == CoreId.FromLong(coreInsuranceType.anvaBimehID));

                if (insuranceType is null)
                {
                    var newInsuranceType = InsuranceType.CreateWithTenantId(new CreateInsuranceTypeWithTenantIdParameter(tenant.Id,
                                                                                                       coreInsuranceType.noeBimeh,
                                                                                                       coreInsuranceType.noeBimeh,
                                                                                                       coreInsuranceType.anvaBimehID,
                                                                                                       !string.IsNullOrEmpty(coreInsuranceType.code) ?
                                                                                                           coreInsuranceType.code :
                                                                                                           _finglishConverter.Convert(coreInsuranceType.noeBimeh),
                                                                                                       null,
                                                                                                       nextPriority,
                                                                                                       tenant.Key));
                    await _commandRepository.InsertAsync(newInsuranceType);
                    nextPriority++;
                }
                else
                {
                    if (!Title.FromString(coreInsuranceType.noeBimeh).Equals(insuranceType.Title))
                        insuranceType.Update(new UpdateInsuranceTypeParameter(coreInsuranceType.noeBimeh,
                                                                  insuranceType.DisplayTitle,
                                                                  !string.IsNullOrEmpty(coreInsuranceType.code) ?
                                                                      coreInsuranceType.code :
                                                                      _finglishConverter.Convert(coreInsuranceType.noeBimeh),
                                                                  insuranceType.ServiceFeatureCategory,
                                                                  insuranceType.Priority));
                }
            }
        }

        await _commandRepository.CommitAsync();

        return Ok();
    }
}
