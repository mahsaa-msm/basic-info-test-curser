using Vehicle.Insurance.Core.ApplicationService.Common.FinglishConverterService;
using Vehicle.Insurance.Core.Contracts.Cities.Commands;
using Vehicle.Insurance.Core.Contracts.CoreInsuranceApis.Cities;
using Vehicle.Insurance.Core.Contracts.Tenants.Queries;
using Vehicle.Insurance.Core.Domain.Cities.Entities;
using Vehicle.Insurance.Core.Domain.Cities.Parameters;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.RequestResponse.Cities.Commands.Fetch;
using Vehicle.Insurance.Core.RequestResponse.CoreInsuranceApis.Queries.City.GetAll;
using Vehicle.Insurance.Core.RequestResponse.Tenants.Queries.GetIAllSelectItem;
using Vehicle.Insurance.Core.Resources;
using Microsoft.Extensions.Logging;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Common;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.Cities.Commands.Fetch;

public sealed class FetchMultiTenantCitiesFromSourceHandler : CommandHandler<FetchMultiTenantCitiesFromSourceCommand>
{
    private readonly ICityCommandRepository _cityCommandRepository;
    private readonly ITenantQueryRepository _tenantQueryRepository;
    private readonly ICoreInsuranceGetAllCitiesCaller _coreInsuranceGetAllCitiesCaller;
    private readonly ILogger<FetchMultiTenantCitiesFromSourceHandler> _logger;
    private readonly IFinglishConverter _finglishConverter;

    public FetchMultiTenantCitiesFromSourceHandler(ZaminServices zaminServices,
                                                   ICityCommandRepository cityCommandRepository,
                                                   ITenantQueryRepository tenantQueryRepository,
                                                   ICoreInsuranceGetAllCitiesCaller coreInsuranceGetAllCitiesCaller,
                                                   ILogger<FetchMultiTenantCitiesFromSourceHandler> logger,
                                                   IFinglishConverter finglishConverter)
        : base(zaminServices)
    {
        _cityCommandRepository = cityCommandRepository;
        _tenantQueryRepository = tenantQueryRepository;
        _coreInsuranceGetAllCitiesCaller = coreInsuranceGetAllCitiesCaller;
        _logger = logger;
        _finglishConverter = finglishConverter;
    }

    public override async Task<CommandResult> Handle(FetchMultiTenantCitiesFromSourceCommand command)
    {
        var coreCitiesResponse = await _coreInsuranceGetAllCitiesCaller.Call(new GetAllCitiesRequest());

        if (coreCitiesResponse.IsFailure ||
            coreCitiesResponse.Value is null ||
            !coreCitiesResponse.Value.Any())
        {
            _logger.LogWarning(string.Format(ProjectTranslation.FETCH_DATA_FROM_CORE_FAILED,
                                             ProjectTranslation.CITY));
            return Result(ApplicationServiceStatus.NotFound);
        }

        var tenants = await _tenantQueryRepository.ExecuteAsync(new GetAllTenantsSelectItemQuery());

        long nextPriority = await _cityCommandRepository.GetNextPriority();

        foreach (var tenant in tenants)
        {
            var tenantCities = await _cityCommandRepository.GetByTenantId(tenant.Id);

            foreach (var coreCity in coreCitiesResponse.Value.DistinctBy(c => c.shahrID))
            {
                try
                {
                    var city = tenantCities
                        .FirstOrDefault(c => c.CoreId == CoreId.FromLong(coreCity.shahrID));

                    if (city is null)
                    {
                        var newCity = City.CreateWithTenantId(new CreateCityWithTenantIdParameter(tenant.Id,
                                                                                                  coreCity.naamShahr,
                                                                                                  coreCity.naamShahr,
                                                                                                  coreCity.shahrID,
                                                                                                  _finglishConverter.Convert(coreCity.naamShahr),
                                                                                                  nextPriority,
                                                                                                  coreCity.ostanID,
                                                                                                  tenant.Key));
                        await _cityCommandRepository.InsertAsync(newCity);
                        nextPriority++;
                    }
                    else
                    {
                        if (!DIPTitle.FromString(coreCity.naamShahr).Equals(city.Title) ||
                            !CoreId.FromLong(coreCity.shahrID).Equals(city.ProvinceCoreId))
                            city.Update(new UpdateCityParameter(coreCity.naamShahr,
                                                                city.DisplayTitle,
                                                                _finglishConverter.Convert(coreCity.naamShahr),
                                                                city.Priority,
                                                                coreCity.ostanID));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }

        await _cityCommandRepository.CommitAsync();

        return Ok();
    }
}

