using Vehicle.Insurance.Core.RequestResponse.Countries.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.Countries.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.Countries.Queries.GetById;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.Contracts.Countries.Queries;

public interface ICountryQueryRepository : IQueryRepository
{
    Task<CountryQr> Execute(GetCountryByIdQuery query);
    Task<List<CountrySelectItemQr>> Execute(GetAllCountryQuery query);
    Task<PagedData<CountryListItemQr>> Execute(GetAllCountriesPagedFilterQuery query);
}

