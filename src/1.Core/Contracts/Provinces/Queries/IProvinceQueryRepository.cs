using Master.Data.Core.RequestResponse.Provinces.Queries.GetAll;
using Master.Data.Core.RequestResponse.Provinces.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.Provinces.Queries.GetById;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.Contracts.Provinces.Queries;

public interface IProvinceQueryRepository : IQueryRepository
{
    Task<ProvinceQr> Execute(GetProvinceByIdQuery query);
    Task<List<ProvinceSelectItemQr>> Execute(GetAllProvincesQuery query);
    Task<PagedData<ProvinceListItemQr>> Execute(GetAllProvincesPagedFilterQuery query);
}
