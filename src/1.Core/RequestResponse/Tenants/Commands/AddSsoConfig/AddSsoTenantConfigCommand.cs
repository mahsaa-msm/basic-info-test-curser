using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.Tenants.Commands.AddSsoConfig;

public sealed class AddSsoTenantConfigCommand : ICommand, IWebRequest
{
    public long TenantId { get; set; }
    public string SsoBasePath { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string OauthType { get; set; } = default!;

    public string Path => "/Api/Tenant/AddSsoTenantConfig";
}
