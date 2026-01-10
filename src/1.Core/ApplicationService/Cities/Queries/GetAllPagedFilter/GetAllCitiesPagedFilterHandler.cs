using Master.Data.Core.Contracts.Cities.Queries;
using Master.Data.Core.RequestResponse.Cities.Queries.GetAllPagedFilter;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Cities.Queries.GetAllPagedFilter;

public class GetAllCitiesPagedFilterHandler : QueryHandler<GetAllCitiesPagedFilterQuery, PagedData<CityListItemQr>>
{
    private readonly ICityQueryRepository _cityQueryRepository;

    public GetAllCitiesPagedFilterHandler(ZaminServices zaminServices,
                                          ICityQueryRepository cityQueryRepository)
        : base(zaminServices)
    {
        _cityQueryRepository = cityQueryRepository;
    }

    public override async Task<QueryResult<PagedData<CityListItemQr>>> Handle(GetAllCitiesPagedFilterQuery query)
        => Result(await _cityQueryRepository.Execute(query));
}