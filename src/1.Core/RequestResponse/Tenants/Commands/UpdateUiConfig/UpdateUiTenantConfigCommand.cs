using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.Tenants.Commands.UpdateUiConfig;
public sealed class UpdateUiTenantConfigCommand : ICommand, IWebRequest
{
    public long TenantId { get; set; }
    public string Theme { get; set; } = default!;

    public string Path => "/Api/Tenant/UpdateUiTenantConfig";
}
