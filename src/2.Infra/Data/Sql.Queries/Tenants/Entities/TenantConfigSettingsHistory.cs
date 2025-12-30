using Master.Data.Infra.Data.Sql.Queries.Common.Entites;

namespace Master.Data.Infra.Data.Sql.Queries.Tenants.Entities;
public sealed class TenantConfigSettingsHistory : QueryObject
{
    public long TenantConfigId { get; set; }
    public string? OldSettings { get; set; }
    public string NewSettings { get; set; }
    public DateTime ChangeDateUtc { get; set; }

    public TenantConfig TenantConfig { get; set; } = new();
}