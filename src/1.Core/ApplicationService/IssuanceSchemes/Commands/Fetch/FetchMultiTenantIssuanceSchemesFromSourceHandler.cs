using Vehicle.Insurance.Core.ApplicationService.Common.FinglishConverterService;
using Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.IssuanceSchemes;
using Vehicle.Insurance.Core.Contracts.IssuanceSchemes.Commands;
using Vehicle.Insurance.Core.Contracts.Tenants.Queries;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.IssuanceSchemes.Entities;
using Vehicle.Insurance.Core.Domain.IssuanceSchemes.Parameters;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.IssuanceScheme.GetAll;
using Vehicle.Insurance.Core.RequestResponse.IssuanceSchemes.Commands.Fetch;
using Vehicle.Insurance.Core.RequestResponse.Tenants.Queries.GetIAllSelectItem;
using Vehicle.Insurance.Core.Resources;
using Microsoft.Extensions.Logging;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Common;
using Zamin.Utilities;
using Zamin.Utilities.Extensions;

namespace Vehicle.Insurance.Core.ApplicationService.IssuanceSchemes.Commands.Fetch;
public sealed class FetchMultiTenantIssuanceSchemesFromSourceHandler : CommandHandler<FetchMultiTenantIssuanceSchemesFromSourceCommand>
{
    private readonly IIssuanceSchemeCommandRepository _commandRepository;
    private readonly ITenantQueryRepository _tenantQueryRepository;
    private readonly ICoreInsuranceGetAllIssuanceSchemesCaller _coreInsuranceGetAllIssuanceSchemesCaller;
    private readonly ILogger<FetchMultiTenantIssuanceSchemesFromSourceHandler> _logger;
    private readonly IFinglishConverter _finglishConverter;

    public FetchMultiTenantIssuanceSchemesFromSourceHandler(ZaminServices zaminServices,
                                           IIssuanceSchemeCommandRepository commandRepository,
                                           ITenantQueryRepository tenantQueryRepository,
                                           ICoreInsuranceGetAllIssuanceSchemesCaller coreInsuranceGetAllIssuanceSchemesCaller,
                                           ILogger<FetchMultiTenantIssuanceSchemesFromSourceHandler> logger,
                                           IFinglishConverter finglishConverter)
        : base(zaminServices)
    {
        _commandRepository = commandRepository;
        _tenantQueryRepository = tenantQueryRepository;
        _coreInsuranceGetAllIssuanceSchemesCaller = coreInsuranceGetAllIssuanceSchemesCaller;
        _logger = logger;
        _finglishConverter = finglishConverter;
    }

    public override async Task<CommandResult> Handle(FetchMultiTenantIssuanceSchemesFromSourceCommand command)
    {
        var coreIssuanceSchemesResponse = await _coreInsuranceGetAllIssuanceSchemesCaller.Call(new GetAllIssuanceSchemesRequest());

        if (coreIssuanceSchemesResponse.IsFailure ||
            coreIssuanceSchemesResponse.Value is null ||
            !coreIssuanceSchemesResponse.Value.Any())
        {
            _logger.LogWarning(string.Format(ProjectTranslation.FETCH_DATA_FROM_CORE_FAILED,
                                             ProjectTranslation.ISSUANCE_SCHEME));
            return Result(ApplicationServiceStatus.NotFound);
        }

        var tenants = await _tenantQueryRepository.ExecuteAsync(new GetAllTenantsSelectItemQuery());

        long nextPriority = await _commandRepository.GetNextPriority();

        foreach (var tenant in tenants)
        {
            var tenantIssuanceSchemes = await _commandRepository.GetByTenantId(tenant.Id);

            foreach (var coreIssuanceScheme in coreIssuanceSchemesResponse.Value.DistinctBy(c => c.tarhSodoorID))
            {
                try
                {
                    var issuanceScheme = tenantIssuanceSchemes
                        .FirstOrDefault(c => c.CoreId == CoreId.FromLong(coreIssuanceScheme.tarhSodoorID));

                    if (issuanceScheme is null)
                    {
                        var newIssuanceScheme = IssuanceScheme.Create(new CreateIssuanceSchemeParameter(coreIssuanceScheme.naam,
                                                                                                      coreIssuanceScheme.naam,
                                                                                                      coreIssuanceScheme.tarhSodoorID,
                                                                                                      !string.IsNullOrEmpty(coreIssuanceScheme.code) ? coreIssuanceScheme.code : _finglishConverter.Convert(coreIssuanceScheme.naam),
                                                                                                      coreIssuanceScheme.tarikhShorooAz is not null ? coreIssuanceScheme.tarikhShorooAz.ToUtcDateTime() : null,
                                                                                                      coreIssuanceScheme.tarikhShorooTa is not null ? coreIssuanceScheme.tarikhShorooTa.ToUtcDateTime() : null,
                                                                                                      coreIssuanceScheme.azTarikhSodoor is not null ? coreIssuanceScheme.azTarikhSodoor.ToUtcDateTime() : null,
                                                                                                      coreIssuanceScheme.taTarikhSodoor is not null ? coreIssuanceScheme.taTarikhSodoor.ToUtcDateTime() : null,
                                                                                                      coreIssuanceScheme.noeBimehID,
                                                                                                      coreIssuanceScheme.takhfifEzafeh,
                                                                                                      coreIssuanceScheme.darsadTakhfifEzafeh,
                                                                                                      coreIssuanceScheme.faal,
                                                                                                      nextPriority));
                        await _commandRepository.InsertAsync(newIssuanceScheme);
                        nextPriority++;
                    }
                    else
                    {
                        if (issuanceScheme.Title != DIPTitle.FromString(coreIssuanceScheme.naam))
                            issuanceScheme.Update(new UpdateIssuanceSchemeParameter(coreIssuanceScheme.naam,
                                                                                    issuanceScheme.DisplayTitle,
                                                                                    !string.IsNullOrEmpty(coreIssuanceScheme.code) ? coreIssuanceScheme.code : _finglishConverter.Convert(coreIssuanceScheme.naam),
                                                                                    coreIssuanceScheme.tarikhShorooAz is not null ? coreIssuanceScheme.tarikhShorooAz.ToUtcDateTime() : null,
                                                                                    coreIssuanceScheme.tarikhShorooTa is not null ? coreIssuanceScheme.tarikhShorooTa.ToUtcDateTime() : null,
                                                                                    coreIssuanceScheme.azTarikhSodoor is not null ? coreIssuanceScheme.azTarikhSodoor.ToUtcDateTime() : null,
                                                                                    coreIssuanceScheme.taTarikhSodoor is not null ? coreIssuanceScheme.taTarikhSodoor.ToUtcDateTime() : null,
                                                                                    coreIssuanceScheme.noeBimehID,
                                                                                    coreIssuanceScheme.takhfifEzafeh,
                                                                                    coreIssuanceScheme.darsadTakhfifEzafeh,
                                                                                    issuanceScheme.Priority));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }

        await _commandRepository.CommitAsync();

        return Ok();
    }
}

