using Vehicle.Insurance.Core.Resources;
using Vehicle.Insurance.Core.Resources.Utils.Extensions;

namespace Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Queries.CommonResults;

public sealed class ServiceFeatureQr
{
    public long Id { get; set; }
    public ServiceFeatureCategory Key { get; set; }
    public string KeyTitle
    {
        get => EnumExtensions.GetEnumDescription(Key);
    }
    public string ServiceName { get; set; } = string.Empty;
    public string FeatureName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsIssuable { get; set; }
    public bool CanViewHistory { get; set; }
    public string? InsuranceTypeCoreId { get; set; }
    public bool IsActive { get; set; }
}

