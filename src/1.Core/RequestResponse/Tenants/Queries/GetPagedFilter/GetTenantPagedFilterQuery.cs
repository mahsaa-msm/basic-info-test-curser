using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.Tenants.Queries.GetPagedFilter;

public sealed class GetTenantPagedFilterQuery : PageQuery<PagedData<TenantSelectItemQr>>, IWebRequest
{
    public string? Name { get; set; }
    public string? Slug { get; set; }

    public string Path => $"/api/Tenant/GetTenantsPagedFilter";
}

