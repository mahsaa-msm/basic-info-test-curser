using Master.Data.Infra.Data.Sql.Queries.Tenants.Entities;

namespace Master.Data.Infra.Data.Sql.Queries.Common.Entites;
public abstract class BaseTenantEntity : BaseTenantEntity<long>
{
}

public abstract class BaseTenantEntity<TId>
{
    public long TenantId { get; set; }
    public Guid? TenantBusinessId { get; set; }
    public TId Id { get; set; }
    public Guid BusinessId { get; set; }
    public string? CreatedByUserId { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public string? ModifiedByUserId { get; set; }
    public DateTime? ModifiedDateTime { get; set; }

    public Tenant Tenant { get; set; } = new();
}
