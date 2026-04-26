using Master.Data.Core.Resources;
using Master.Data.Infra.Data.Sql.Queries.Common.Entites;

namespace Master.Data.Infra.Data.Sql.Queries.ServiceFeatures.Entities;

public sealed class ServiceFeature : BaseTenantEntity
{
    public ServiceFeatureCategory Key { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public string FeatureName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsIssuable { get; set; }
    public bool CanViewHistory { get; set; }
    public string? InsuranceTypeCoreId { get; set; }
    public bool IsActive { get; set; }
}

