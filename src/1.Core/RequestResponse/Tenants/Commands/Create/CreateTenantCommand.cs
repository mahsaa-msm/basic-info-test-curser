using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.Tenants.Commands.Create;

public sealed class CreateTenantCommand : ICommand<long?>, IWebRequest
{
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;

    public string Path => "/Api/Tenant/CreateTenant";
}

