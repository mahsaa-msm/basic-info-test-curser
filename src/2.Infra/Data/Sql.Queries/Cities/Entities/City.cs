using Master.Data.Infra.Data.Sql.Queries.Common.Entites;
using Master.Data.Infra.Data.Sql.Queries.Provinces.Entities;

namespace Master.Data.Infra.Data.Sql.Queries.Cities.Entities;
public sealed class City : BaseTenantEntity
{
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public long Priority { get; set; }
    public string ProvinceCoreId { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    public Province Province { get; set; } = new();
}
