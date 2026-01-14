using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.Tenants.Queries.GetIAllSelectItem;

public sealed class GetAllTenantsSelectItemQuery : IQuery<List<TenantIdKeyQr>>, IWebRequest
{
    public string Path => $"/api/tenant/GetAllTenants";
}
