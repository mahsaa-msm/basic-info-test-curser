using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.RequestResponse.Tenants.Commands.Remove;

public sealed class RemoveTenantConfigCommand : ICommand, IWebRequest
{
    public long TenantId { get; set; }
    public ConfigType ConfigType { get; set; }

    public string Path => "/Api/Tenant/RemoveTenantConfig";
}

