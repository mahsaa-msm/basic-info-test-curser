using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.RequestResponse.Tenants.Commands.Remove;

public sealed class RemoveTenantConfigCommand : ICommand, IWebRequest
{
    public long TenantId { get; set; }
    public ConfigType ConfigType { get; set; }

    public string Path => "/Api/Tenant/RemoveTenantConfig";
}
