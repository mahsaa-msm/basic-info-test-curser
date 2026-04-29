namespace Vehicle.Insurance.Core.RequestResponse.Cities.Queries.GetAllPagedFilter;

public sealed class CityListItemQr
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public long Priority { get; set; }
    public bool IsActive { get; set; }
    public string ProvinceCoreId { get; set; } = string.Empty;
    public string? ProvinceDisplayTitle { get; set; }
}

