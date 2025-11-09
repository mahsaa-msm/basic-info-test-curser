using Master.Data.Core.RequestResponse.Cities.Queries.GetAll;
using Master.Data.Core.RequestResponse.Cities.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.Cities.Queries.GetById;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.Contracts.Cities.Queries;
public interface ICityQueryRepository : IQueryRepository
{
    Task<CityQr> Execute(GetCityByIdQuery query);
    Task<List<CitySelectItemQr>> Execute(GetAllCitiesQuery query);
    Task<PagedData<CityListItemQr>> Execute(GetAllCitiesPagedFilterQuery query);
}
