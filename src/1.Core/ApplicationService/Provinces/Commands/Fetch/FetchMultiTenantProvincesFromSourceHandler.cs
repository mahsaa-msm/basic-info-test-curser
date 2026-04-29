using Vehicle.Insurance.Core.ApplicationService.Common.FinglishConverterService;
using Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.Provinces;
using Vehicle.Insurance.Core.Contracts.Provinces.Commands;
using Vehicle.Insurance.Core.Contracts.Tenants.Queries;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.Provinces.Entities;
using Vehicle.Insurance.Core.Domain.Provinces.Parameters;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.Province.GetAll;
using Vehicle.Insurance.Core.RequestResponse.Provinces.Commands.Fetch;
using Vehicle.Insurance.Core.RequestResponse.Tenants.Queries.GetIAllSelectItem;
using Vehicle.Insurance.Core.Resources;
using Microsoft.Extensions.Logging;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Common;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.Provinces.Commands.Fetch;

public sealed class FetchMultiTenantProvincesFromSourceHandler : CommandHandler<FetchMultiTenantProvincesFromSourceCommand>
{
    private readonly IProvinceCommandRepository _provinceCommandRepository;
    private readonly ITenantQueryRepository _tenantQueryRepository;
    private readonly ICoreInsuranceGetAllProvincesCaller _coreInsuranceGetAllProvincesCaller;
    private readonly ILogger<FetchMultiTenantProvincesFromSourceHandler> _logger;
    private readonly IFinglishConverter _finglishConverter;

    public FetchMultiTenantProvincesFromSourceHandler(ZaminServices zaminServices,
                                                      IProvinceCommandRepository provinceCommandRepository,
                                                      ITenantQueryRepository tenantQueryRepository,
                                                      ICoreInsuranceGetAllProvincesCaller coreInsuranceGetAllProvincesCaller,
                                                      ILogger<FetchMultiTenantProvincesFromSourceHandler> logger,
                                                      IFinglishConverter finglishConverter)
        : base(zaminServices)
    {
        _provinceCommandRepository = provinceCommandRepository;
        _tenantQueryRepository = tenantQueryRepository;
        _coreInsuranceGetAllProvincesCaller = coreInsuranceGetAllProvincesCaller;
        _logger = logger;
        _finglishConverter = finglishConverter;
    }

    public override async Task<CommandResult> Handle(FetchMultiTenantProvincesFromSourceCommand command)
    {
        var coreProvincesResponse = await _coreInsuranceGetAllProvincesCaller.Call(new GetAllProvincesRequest());

        if (coreProvincesResponse.IsFailure ||
            coreProvincesResponse.Value is null ||
            !coreProvincesResponse.Value.Any())
        {
            _logger.LogWarning(string.Format(ProjectTranslation.FETCH_DATA_FROM_CORE_FAILED,
                                             ProjectTranslation.PROVINCE));
            return Result(ApplicationServiceStatus.NotFound);
        }

        var tenants = await _tenantQueryRepository.ExecuteAsync(new GetAllTenantsSelectItemQuery());

        long nextPriority = await _provinceCommandRepository.GetNextPriority();

        foreach (var tenant in tenants)
        {
            var tenantProvinces = await _provinceCommandRepository.GetByTenantId(tenant.Id);

            foreach (var coreProvince in coreProvincesResponse.Value.DistinctBy(c => c.ostanID))
            {
                try
                {
                    var province = tenantProvinces
                            .FirstOrDefault(c => c.CoreId == CoreId.FromLong(coreProvince.ostanID));

                    if (province is null)
                    {
                        var newProvince = Province.CreateWithTenantId(new CreateProvinceWithTenantIdParameter(tenant.Id,
                                                                                                              coreProvince.naamOstan,
                                                                                                              coreProvince.naamOstan,
                                                                                                              coreProvince.ostanID,
                                                                                                              !string.IsNullOrEmpty(coreProvince.codeOstan) ?
                                                                                                                  coreProvince.codeOstan :
                                                                                                                  _finglishConverter.Convert(coreProvince.naamOstan),
                                                                                                              nextPriority,
                                                                                                              coreProvince.keshvarID,
                                                                                                              tenant.Key));
                        await _provinceCommandRepository.InsertAsync(newProvince);
                        nextPriority++;
                    }
                    else
                    {
                        if (!DIPTitle.FromString(coreProvince.naamOstan).Equals(province.Title) ||
                            !CoreId.FromLong(coreProvince.keshvarID).Equals(province.CountryCoreId))
                            province.Update(new UpdateProvinceParameter(coreProvince.naamOstan,
                                                                        province.DisplayTitle,
                                                                        !string.IsNullOrEmpty(coreProvince.codeOstan) ?
                                                                            coreProvince.codeOstan :
                                                                            _finglishConverter.Convert(coreProvince.naamOstan),
                                                                        province.Priority,
                                                                        coreProvince.keshvarID));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }

        await _provinceCommandRepository.CommitAsync();

        return Ok();
    }
}

