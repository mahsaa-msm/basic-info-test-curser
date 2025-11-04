using Master.Data.Core.Contracts.CoreInsuranceApis.Countries;
using Master.Data.Core.Contracts.Countries.Commands;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.Countries.Entities;
using Master.Data.Core.Domain.Countries.Parameters;
using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.Country.GetAll;
using Master.Data.Core.RequestResponse.Countries.Commands.Fetch;
using Master.Data.Core.Resources;
using Master.Data.Core.Resources.Utils;
using Microsoft.Extensions.Logging;
using Zamin.Core.ApplicationServices.Commands;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Common;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Countries.Commands.Fetch;
public sealed class FetchCountriesFromSourceHandler : CommandHandler<FetchCountriesFromSourceCommand>
{
    private readonly ICountryCommandRepository _commandRepository;
    private readonly IGetAllCountriesCaller _getAllCountriesCaller;
    private readonly ILogger<FetchCountriesFromSourceHandler> _logger;

    public FetchCountriesFromSourceHandler(ZaminServices zaminServices,
                                           ICountryCommandRepository commandRepository,
                                           IGetAllCountriesCaller getAllCountriesCaller,
                                           ILogger<FetchCountriesFromSourceHandler> logger)
        : base(zaminServices)
    {
        _commandRepository = commandRepository;
        _getAllCountriesCaller = getAllCountriesCaller;
        _logger = logger;
    }

    public override async Task<CommandResult> Handle(FetchCountriesFromSourceCommand command)
    {
        var coreCountriesResponse = await _getAllCountriesCaller.Call(new GetAllCountriesRequest());

        if (coreCountriesResponse.IsFailure || !coreCountriesResponse.Value.Any())
        {
            _logger.LogWarning(string.Format(ProjectTranslation.FETCH_DATA_FROM_CORE_FAILED,
                                             ProjectTranslation.COUNTRY));
            return Result(ApplicationServiceStatus.NotFound);
        }

        var countries = await _commandRepository.GetAllIgnoreQueryFiltersAsync();
        long nextPriority = await _commandRepository.GetNextPriority();
        var finglishConverter = new FinglishConverter();

        foreach (var coreCountry in coreCountriesResponse.Value)
        {
            var country = countries.FirstOrDefault(c => c.CoreId == CoreId.FromLong(coreCountry.id));
            if (country is not null)
                country.Update(new UpdateCountryParameter(coreCountry.title,
                                                          country.DisplayTitle,
                                                          !string.IsNullOrEmpty(coreCountry.centInsurCode) ?
                                                              coreCountry.centInsurCode :
                                                              finglishConverter.Convert(coreCountry.title),
                                                          country.Priority));
            else
            {
                var newCountry = Country.Create(new CreateCountryParameter(coreCountry.title,
                                                                           coreCountry.title,
                                                                           coreCountry.id,
                                                                           !string.IsNullOrEmpty(coreCountry.centInsurCode) ?
                                                                               coreCountry.centInsurCode :
                                                                               finglishConverter.Convert(coreCountry.title),
                                                                           nextPriority));
                await _commandRepository.InsertAsync(newCountry);
            }

            nextPriority++;
        }

        await _commandRepository.CommitAsync();

        return Ok();
    }
}