using Master.Data.Core.Contracts.Countries.Queries;
using Master.Data.Core.RequestResponse.Countries.Queries.GetAll;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Countries.Queries.GetAll;

public class GetAllCountriesHandler : QueryHandler<GetAllCountryQuery, List<CountrySelectItemQr>>
{
    private readonly ICountryQueryRepository _countryQueryRepository;

    public GetAllCountriesHandler(ZaminServices zaminServices,
                                  ICountryQueryRepository countryQueryRepository)
        : base(zaminServices)
    {
        _countryQueryRepository = countryQueryRepository;
    }

    public override async Task<QueryResult<List<CountrySelectItemQr>>> Handle(GetAllCountryQuery query)
        => Result(await _countryQueryRepository.Execute(query));
}
