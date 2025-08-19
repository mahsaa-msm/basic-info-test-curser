using Master.Data.Core.RequestResponse.Tenants.Queries.GetById;
using Master.Data.Core.RequestResponse.Tenants.Queries.GetPagedFilter;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.Contracts.Tenants.Queries;
public interface ITenantQueryRepository : IQueryRepository
{
    public Task<TenantGraphQr?> Execute(GetTenantByIdQuery query);
    public Task<PagedData<TenantSelectItemQr>> ExecuteAsync(GetTenantPagedFilterQuery query);
}
