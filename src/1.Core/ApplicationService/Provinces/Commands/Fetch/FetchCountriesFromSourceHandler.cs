//using Master.Data.Core.ApplicationService.Common.FinglishConverterService;
//using Master.Data.Core.Contracts.CoreInsuranceApis.Countries;
//using Master.Data.Core.Contracts.Countries.Commands;
//using Master.Data.Core.Domain.Common.ValueObjects;
//using Master.Data.Core.Domain.Countries.Entities;
//using Master.Data.Core.Domain.Countries.Parameters;
//using Master.Data.Core.RequestResponse.CoreInsuranceApis.Queries.Country.GetAll;
//using Master.Data.Core.RequestResponse.Countries.Commands.Fetch;
//using Master.Data.Core.Resources;
//using Microsoft.Extensions.Logging;
//using Zamin.Core.ApplicationServices.Commands;
//using Zamin.Core.Domain.Toolkits.ValueObjects;
//using Zamin.Core.RequestResponse.Commands;
//using Zamin.Core.RequestResponse.Common;
//using Zamin.Utilities;

//namespace Master.Data.Core.ApplicationService.Countries.Commands.Fetch;
//public sealed class FetchCountriesFromSourceHandler : CommandHandler<FetchCountriesFromSourceCommand>
//{
//    private readonly ICountryCommandRepository _commandRepository;
//    private readonly ICoreInsuranceGetAllCountriesCaller _coreInsuranceGetAllCountriesCaller;
//    private readonly ILogger<FetchCountriesFromSourceHandler> _logger;
//    private readonly IFinglishConverter _finglishConverter;

//    public FetchCountriesFromSourceHandler(ZaminServices zaminServices,
//                                           ICountryCommandRepository commandRepository,
//                                           ICoreInsuranceGetAllCountriesCaller coreInsuranceGetAllCountriesCaller,
//                                           ILogger<FetchCountriesFromSourceHandler> logger,
//                                           IFinglishConverter finglishConverter)
//        : base(zaminServices)
//    {
//        _commandRepository = commandRepository;
//        _coreInsuranceGetAllCountriesCaller = coreInsuranceGetAllCountriesCaller;
//        _logger = logger;
//        _finglishConverter = finglishConverter;
//    }

//    public override async Task<CommandResult> Handle(FetchCountriesFromSourceCommand command)
//    {
//        var coreCountriesResponse = await _coreInsuranceGetAllCountriesCaller.Call(new GetAllCountriesRequest());

//        if (coreCountriesResponse.IsFailure ||
//            coreCountriesResponse.Value is null ||
//            coreCountriesResponse.Value.content is null ||
//            !coreCountriesResponse.Value.content.itemList.Any())
//        {
//            _logger.LogWarning(string.Format(ProjectTranslation.FETCH_DATA_FROM_CORE_FAILED,
//                                             ProjectTranslation.COUNTRY));
//            return Result(ApplicationServiceStatus.NotFound);
//        }

//        var countries = await _commandRepository.GetAllAsync();
//        long nextPriority = await _commandRepository.GetNextPriority();

//        foreach (var coreCountry in coreCountriesResponse.Value.content.itemList)
//        {
//            var country = countries
//                .FirstOrDefault(c => c.CoreId == CoreId.FromString(coreCountry.id));

//            if (country is null)
//            {
//                var newCountry = Country.Create(new CreateCountryParameter(coreCountry.title,
//                                                                           coreCountry.title,
//                                                                           coreCountry.id,
//                                                                           !string.IsNullOrEmpty(coreCountry.centInsurCode) ?
//                                                                               coreCountry.centInsurCode :
//                                                                               _finglishConverter.Convert(coreCountry.title),
//                                                                           nextPriority));
//                await _commandRepository.InsertAsync(newCountry);
//                nextPriority++;
//            }
//            else
//            {
//                if (country.Title != Title.FromString(coreCountry.title))
//                    country.Update(new UpdateCountryParameter(coreCountry.title,
//                                                              country.DisplayTitle,
//                                                              !string.IsNullOrEmpty(coreCountry.centInsurCode) ?
//                                                                  coreCountry.centInsurCode :
//                                                                  _finglishConverter.Convert(coreCountry.title),
//                                                              country.Priority));
//            }
//        }

//        await _commandRepository.CommitAsync();

//        return Ok();
//    }
//}