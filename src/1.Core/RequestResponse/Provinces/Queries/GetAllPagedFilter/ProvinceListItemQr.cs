namespace Master.Data.Core.RequestResponse.Provinces.Queries.GetAllPagedFilter;
public sealed class ProvinceListItemQr
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public long Priority { get; set; }
    public bool IsActive { get; set; }
    public string CountryCoreId { get; set; } = string.Empty;
}
