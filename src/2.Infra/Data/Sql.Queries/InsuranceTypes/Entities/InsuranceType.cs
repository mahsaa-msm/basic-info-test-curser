using Master.Data.Core.Resources;
using Master.Data.Infra.Data.Sql.Queries.Common.Entites;

namespace Master.Data.Infra.Data.Sql.Queries.InsuranceTypes.Entities;

public sealed class InsuranceType : BaseTenantEntity
{
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public ServiceFeatureCategory? ServiceFeatureCategory { get; set; }
    public long Priority { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}
