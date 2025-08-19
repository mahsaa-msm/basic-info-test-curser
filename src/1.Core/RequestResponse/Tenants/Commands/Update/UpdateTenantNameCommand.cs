using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.Tenants.Commands.Update;
public sealed class UpdateTenantNameCommand : ICommand, IWebRequest
{
    public long TenantId { get; set; }
    public string Name { get; set; } = default!;

    public string Path => "/Api/Tenant/UpdateTenantName";
}
