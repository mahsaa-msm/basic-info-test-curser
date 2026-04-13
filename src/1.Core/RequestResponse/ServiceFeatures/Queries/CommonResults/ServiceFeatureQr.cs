using Master.Data.Core.Resources;
using Master.Data.Core.Resources.Utils.Extensions;

namespace Master.Data.Core.RequestResponse.ServiceFeatures.Queries.CommonResults;

public sealed class ServiceFeatureQr
{
    public long Id { get; set; }
    public ServiceFeatureCategory Key { get; set; }
    public string KeyTitle
    {
        get => EnumExtensions.GetEnumDescription(Key);
    }
    public string ServiceName { get; set; }
    public string FeatureName { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
