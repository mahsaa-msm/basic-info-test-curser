using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.Tenants.Queries.GetById;

public sealed class GetTenantByIdQuery : IQuery<TenantGraphQr?>, IWebRequest
{
    public long Id { get; set; }

    public string Path => $"/api/tenant/GetTenantById";
}
