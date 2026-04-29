using Vehicle.Insurance.Infra.Data.Sql.Queries.Common.Entites;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Infra.Data.Sql.Queries.PatternCatalogs.Entities;

public sealed class PatternCatalog : BaseTenantEntity
{
    public string Key { get; set; } = default!;
    public string Pattern { get; set; } = default!;
    public PatternCatalogType Type { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDateUtc { get; set; }
    public DateTime? LastModifiedDateUtc { get; set; }
    public long Priority { get; set; }
    public bool IsActive { get; set; }
}

