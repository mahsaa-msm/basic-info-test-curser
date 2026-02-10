using Master.Data.Core.ApplicationService.Common.FinglishConverterService;
using Master.Data.Core.Contracts.Cities.Commands;
using Master.Data.Core.Contracts.CoreInsuranceApis.Cities;
using Master.Data.Core.Domain.Cities.Entities;
using Master.Data.Core.Domain.Cities.Parameters;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.RequestResponse.Cities.Commands.Fetch;
using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.City.GetAll;
using Master.Data.Core.Resources;
using Microsoft.Extensions.Logging;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.Domain.Toolkits.ValueObjects;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Common;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Cities.Commands.Fetch;

public sealed class FetchCitiesFromSourceHandler : CommandHandler<FetchCitiesFromSourceCommand>
{
    private readonly ICityCommandRepository _cityCommandRepository;
    private readonly ICoreInsuranceGetAllCitiesCaller _coreInsuranceGetAllCitiesCaller;
    private readonly ILogger<FetchCitiesFromSourceHandler> _logger;
    private readonly IFinglishConverter _finglishConverter;

    public FetchCitiesFromSourceHandler(ZaminServices zaminServices,
                                        ICityCommandRepository cityCommandRepository,
                                        ICoreInsuranceGetAllCitiesCaller coreInsuranceGetAllCitiesCaller,
                                        ILogger<FetchCitiesFromSourceHandler> logger,
                                        IFinglishConverter finglishConverter)
        : base(zaminServices)
    {
        _cityCommandRepository = cityCommandRepository;
        _coreInsuranceGetAllCitiesCaller = coreInsuranceGetAllCitiesCaller;
        _logger = logger;
        _finglishConverter = finglishConverter;
    }

    public override async Task<CommandResult> Handle(FetchCitiesFromSourceCommand command)
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

        var cities = await _cityCommandRepository.GetAllAsync();
        long nextPriority = await _cityCommandRepository.GetNextPriority();

        foreach (var coreCity in coreCitiesResponse.Value.DistinctBy(c => c.shahrID))
        {
            try
            {
                var city = cities
                    .FirstOrDefault(c => c.CoreId == CoreId.FromLong(coreCity.shahrID));

                if (city is null)
                {
                    var newCity = City.Create(new CreateCityParameter(coreCity.naamShahr,
                                                                      coreCity.naamShahr,
                                                                      coreCity.shahrID,
                                                                      _finglishConverter.Convert(coreCity.naamShahr),
                                                                      nextPriority,
                                                                      coreCity.ostanID));
                    await _cityCommandRepository.InsertAsync(newCity);
                    nextPriority++;
                }
                else
                {
                    if (city.Title != Title.FromString(coreCity.naamShahr) ||
                        city.ProvinceCoreId != CoreId.FromLong(coreCity.shahrID))
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

        await _cityCommandRepository.CommitAsync();

        return Ok();
    }
}