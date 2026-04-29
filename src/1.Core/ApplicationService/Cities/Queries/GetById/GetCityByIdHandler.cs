using Vehicle.Insurance.Core.Contracts.Cities.Queries;
using Vehicle.Insurance.Core.RequestResponse.Cities.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.Cities.Queries.GetById;

public sealed class GetCityByIdHandler : QueryHandler<GetCityByIdQuery, CityQr>
{
    private readonly ICityQueryRepository _cityQueryRepository;

    public GetCityByIdHandler(ZaminServices zaminServices,
                              ICityQueryRepository cityQueryRepository)
        : base(zaminServices)
    {
        _cityQueryRepository = cityQueryRepository;
    }

    public override async Task<QueryResult<CityQr>> Handle(GetCityByIdQuery query)
        => Result(await _cityQueryRepository.Execute(query));
}

