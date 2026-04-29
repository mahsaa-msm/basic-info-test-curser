using Vehicle.Insurance.Infra.Data.Sql.Queries.Common.Entites;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.Tenants.Entities;

public sealed class Tenant : QueryObject
{
    public Guid TenantKey { get; set; }
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTime CreatedDateUtc { get; set; }
    public List<TenantConfig> Configs { get; set; } = new();
}

