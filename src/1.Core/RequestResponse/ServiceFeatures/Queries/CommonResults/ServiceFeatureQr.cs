using Master.Data.Core.Resources.Utils.Extensions;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.RequestResponse.ServiceFeatures.Queries.CommonResults;

public sealed class ServiceFeatureQr
{
    public long Id { get; set; }
    public ServiceFeatureKey Key { get; set; }
    public string KeyTitle
    {
        get => EnumExtensions.GetEnumDescription(Key);
    }
    public string ServiceName { get; set; }
    public string FeatureName { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}
