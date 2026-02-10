using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Core.RequestResponse.InsuranceUnits.Queries.GetAllPagedFilter;

public sealed class InsuranceUnitListItemQr
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string CityCoreId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public InsuranceUnitType Type { get; set; }
    public InsuranceUnitState State { get; set; }
    public long Priority { get; set; }
    public bool IsActive { get; set; }
}
