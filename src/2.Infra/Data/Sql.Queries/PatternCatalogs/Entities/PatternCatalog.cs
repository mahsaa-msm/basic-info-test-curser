using Master.Data.Infra.Data.Sql.Queries.Common.Entites;

namespace Master.Data.Infra.Data.Sql.Queries.PatternCatalogs.Entities;

public sealed class PatternCatalog : BaseTenantEntity
{
    public string Key { get; set; } = default!;
    public string Pattern { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime CreatedDateUtc { get; set; }
    public DateTime? LastModifiedDateUtc { get; set; }
    public long Priority { get; set; }
    public bool IsActive { get; set; }
}
