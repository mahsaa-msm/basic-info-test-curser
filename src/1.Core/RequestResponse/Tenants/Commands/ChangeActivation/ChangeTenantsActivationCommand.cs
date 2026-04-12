using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.Tenants.Commands.ChangeActivation;

public sealed class ChangeTenantsActivationCommand : ICommand, IWebRequest
{
    public List<long> TenantIds { get; set; } = new();
    public bool IsActive { get; set; }

    public string Path => "/Api/Tenant/ChangeTenantsActivation";
}
