using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.Tenants.Commands.ChangeActivation;

public sealed class ChangeTenantActivationCommand : ICommand, IWebRequest
{
    public long TenantId { get; set; }
    public bool IsActive { get; set; }

    public string Path => "/Api/Tenant/ChangeTenantActivation";
}
