using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.RequestResponse.IssuanceSchemes.Queries.GetAllPagedFilter;

public sealed class IssuanceSchemeListItemQr
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public DateTime? FromStartDateUtc { get; set; }
    public DateTime? ToStartDateUtc { get; set; }
    public DateTime? FromIssueDateUtc { get; set; }
    public DateTime? ToIssueDateUtc { get; set; }
    public string InsuranceTypeCoreId { get; set; } = string.Empty;
    public AdjustmentType? AdjustmentType { get; set; }
    public double? AdjustmentPercent  { get; set; }
    public long Priority { get; set; }
    public bool IsActive { get; set; }
}