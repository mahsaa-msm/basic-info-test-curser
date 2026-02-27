using Master.Data.Core.Resources;

namespace Master.Data.Core.RequestResponse.InsuranceTypes.Queries.GetAllPagedFilter;

public sealed class InsuranceTypeListItemQr
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public ServiceFeatureCategory? ServiceFeatureCategory { get; set; }
    public long Priority { get; set; }
    public bool IsActive { get; set; }
}