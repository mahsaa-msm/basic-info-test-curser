using Vehicle.Insurance.Core.Resources;

namespace Vehicle.Insurance.Core.RequestResponse.InsuranceTypes.Queries.GetById;

public sealed class InsuranceTypeQr
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public ServiceFeatureCategory? ServiceFeatureCategory { get; set; }
    public long Priority { get; set; }
    public bool IsActive { get; set; }
    public bool IsEditable { get; set; }

}
