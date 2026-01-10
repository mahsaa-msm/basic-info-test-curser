using Master.Data.Infra.Data.Sql.Queries.Common.Entites;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Infra.Data.Sql.Queries.ServiceFeatures.Entities;

public sealed class ServiceFeature : BaseTenantEntity
{
    public ServiceFeatureKey Key { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public string FeatureName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

