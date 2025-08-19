using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.Tenants.Commands.Create;
public sealed class CreateTenantCommand : ICommand<long?>, IWebRequest
{
    public string Name { get; set; } = default!;

    public string Path => "/Api/Tenant/CreateTenant";
}
