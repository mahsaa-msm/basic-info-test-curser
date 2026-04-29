using Vehicle.Insurance.Core.RequestResponse.Cities.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.Cities.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.Cities.Queries.GetById;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.Contracts.Cities.Queries;

public interface ICityQueryRepository : IQueryRepository
{
    Task<CityQr> Execute(GetCityByIdQuery query);
    Task<List<CitySelectItemQr>> Execute(GetAllCitiesQuery query);
    Task<PagedData<CityListItemQr>> Execute(GetAllCitiesPagedFilterQuery query);
}

