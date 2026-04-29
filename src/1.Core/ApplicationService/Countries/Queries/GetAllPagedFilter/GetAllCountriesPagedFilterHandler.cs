using Vehicle.Insurance.Core.Contracts.Countries.Queries;
using Vehicle.Insurance.Core.RequestResponse.Countries.Queries.GetAllPagedFilter;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.Countries.Queries.GetAllPagedFilter;

public class GetAllCountriesPagedFilterHandler : QueryHandler<GetAllCountriesPagedFilterQuery, PagedData<CountryListItemQr>>
{
    private readonly ICountryQueryRepository _queryRepository;

    public GetAllCountriesPagedFilterHandler(ZaminServices zaminServices,
                                             ICountryQueryRepository queryRepository)
        : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<PagedData<CountryListItemQr>>> Handle(GetAllCountriesPagedFilterQuery query)
        => Result(await _queryRepository.Execute(query));
}
