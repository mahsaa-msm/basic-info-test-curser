using Master.Data.Core.Resources;

namespace Master.Data.Core.RequestResponse.ServiceFeatures.Queries.GetAllByKey;

public sealed class GetAllServiceFeaturesByKeyQr
{
    public string ServiceName { get; set; } = string.Empty;
    public string FeatureName { get; set; } = string.Empty;
    public ServiceFeatureCategory Key { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public List<long> TenantIds { get; set; } = new();
}
