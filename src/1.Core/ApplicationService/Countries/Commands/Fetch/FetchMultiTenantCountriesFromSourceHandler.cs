using Master.Data.Core.ApplicationService.Common.FinglishConverterService;
using Master.Data.Core.Contracts.CoreInsuranceApis.Countries;
using Master.Data.Core.Contracts.Countries.Commands;
using Master.Data.Core.Contracts.Tenants.Queries;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.Countries.Entities;
using Master.Data.Core.Domain.Countries.Parameters;
using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.Country.GetAll;
using Master.Data.Core.RequestResponse.Countries.Commands.Fetch;
using Master.Data.Core.RequestResponse.Tenants.Queries.GetIAllSelectItem;
using Master.Data.Core.Resources;
using Microsoft.Extensions.Logging;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Common;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Countries.Commands.Fetch;

public sealed class FetchMultiTenantCountriesFromSourceHandler : CommandHandler<FetchMultiTenantCountriesFromSourceCommand>
{
    private readonly ICountryCommandRepository _commandRepository;
    private readonly ITenantQueryRepository _tenantQueryRepository;
    private readonly ICoreInsuranceGetAllCountriesCaller _coreInsuranceGetAllCountriesCaller;
    private readonly ILogger<FetchMultiTenantCountriesFromSourceHandler> _logger;
    private readonly IFinglishConverter _finglishConverter;

    public FetchMultiTenantCountriesFromSourceHandler(ZaminServices zaminServices,
                                           ICountryCommandRepository commandRepository,
                                           ITenantQueryRepository tenantQueryRepository,
                                           ICoreInsuranceGetAllCountriesCaller coreInsuranceGetAllCountriesCaller,
                                           ILogger<FetchMultiTenantCountriesFromSourceHandler> logger,
                                           IFinglishConverter finglishConverter)
        : base(zaminServices)
    {
        _commandRepository = commandRepository;
        _tenantQueryRepository = tenantQueryRepository;
        _coreInsuranceGetAllCountriesCaller = coreInsuranceGetAllCountriesCaller;
        _logger = logger;
        _finglishConverter = finglishConverter;
    }

    public override async Task<CommandResult> Handle(FetchMultiTenantCountriesFromSourceCommand command)
    {
        var coreCountriesResponse = await _coreInsuranceGetAllCountriesCaller.Call(new GetAllCountriesRequest());

        if (coreCountriesResponse.IsFailure ||
            coreCountriesResponse.Value is null ||
            coreCountriesResponse.Value.content is null ||
            !coreCountriesResponse.Value.content.itemList.Any())
        {
            _logger.LogWarning(string.Format(ProjectTranslation.FETCH_DATA_FROM_CORE_FAILED,
                                             ProjectTranslation.COUNTRY));
            return Result(ApplicationServiceStatus.NotFound);
        }

        var tenants = await _tenantQueryRepository.ExecuteAsync(new GetAllTenantsSelectItemQuery());

        long nextPriority = await _commandRepository.GetNextPriority();

        foreach (var tenant in tenants)
        {
            var tenantCountries = await _commandRepository.GetByTenantId(tenant.Id);

            foreach (var coreCountry in coreCountriesResponse.Value.content.itemList.DistinctBy(c => c.id))
            {
                try
                {
                    var country = tenantCountries
                            .FirstOrDefault(c => c.CoreId == CoreId.FromString(coreCountry.id));

                    if (country is null)
                    {
                        var newCountry = Country.CreateWithTenantId(new CreateCountryWithTenantIdParameter(tenant.Id,
                                                                                                           coreCountry.title,
                                                                                                           coreCountry.title,
                                                                                                           coreCountry.id,
                                                                                                           !string.IsNullOrEmpty(coreCountry.centInsurCode) ?
                                                                                                               coreCountry.centInsurCode :
                                                                                                               _finglishConverter.Convert(coreCountry.title),
                                                                                                           nextPriority,
                                                                                                           tenant.Key));
                        await _commandRepository.InsertAsync(newCountry);
                        nextPriority++;
                    }
                    else
                    {
                        if (!DIPTitle.FromString(coreCountry.title).Equals(country.Title))
                            country.Update(new UpdateCountryParameter(coreCountry.title,
                                                                      country.DisplayTitle,
                                                                      !string.IsNullOrEmpty(coreCountry.centInsurCode) ?
                                                                          coreCountry.centInsurCode :
                                                                          _finglishConverter.Convert(coreCountry.title),
                                                                      country.Priority));
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
