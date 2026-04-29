using Vehicle.Insurance.Core.RequestResponse.Tenants.Queries.GetById;
using Vehicle.Insurance.Core.RequestResponse.Tenants.Queries.GetIAllSelectItem;
using Vehicle.Insurance.Core.RequestResponse.Tenants.Queries.GetPagedFilter;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.Contracts.Tenants.Queries;

public interface ITenantQueryRepository : IQueryRepository
{
    public Task<TenantGraphQr?> Execute(GetTenantByIdQuery query);
    public Task<List<TenantIdKeyQr>> ExecuteAsync(GetAllTenantsSelectItemQuery query);
    public Task<PagedData<TenantSelectItemQr>> ExecuteAsync(GetTenantPagedFilterQuery query);
}

