using Master.Data.Core.Contracts.Cities.Queries;
using Master.Data.Core.RequestResponse.Cities.Queries.GetAll;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.Cities.Queries.GetAll;

public class GetAllCitiesHandler : QueryHandler<GetAllCitiesQuery, List<CitySelectItemQr>>
{
    private readonly ICityQueryRepository _cityQueryRepository;

    public GetAllCitiesHandler(ZaminServices zaminServices,
                                  ICityQueryRepository cityQueryRepository)
        : base(zaminServices)
    {
        _cityQueryRepository = cityQueryRepository;
    }

    public override async Task<QueryResult<List<CitySelectItemQr>>> Handle(GetAllCitiesQuery query)
        => Result(await _cityQueryRepository.Execute(query));
}
