using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.RequestResponse.IssuanceSchemes.Queries.GetAllPagedFilter;

public sealed class GetAllIssuanceSchemesPagedFilterQuery : PageQuery<PagedData<IssuanceSchemeListItemQr>>, IWebRequest
{
    public string? CoreId { get; set; }
    public string? Title { get; set; }
    public string? DisplayTitle { get; set; }
    public string? Code { get; set; }
    public DateTime? FromStartDateUtc { get; set; }
    public DateTime? ToStartDateUtc { get; set; }
    public DateTime? FromIssueDateUtc { get; set; }
    public DateTime? ToIssueDateUtc { get; set; }
    public string? InsuranceTypeCoreId { get; set; } = string.Empty;
    public AdjustmentType? AdjustmentType { get; set; }
    public double? AdjustmentPercent  { get; set; }
    public bool? IsActive { get; set; }
    public long? Priority { get; set; }

    public string Path => "/Api/IssuanceScheme/GetAllIssuanceSchemesPagedFilter";
}
