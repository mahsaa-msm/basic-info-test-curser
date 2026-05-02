namespace Vehicle.Insurance.Core.RequestResponse.VehicleTips.Queries.GetAllPagedFilter;

public sealed class VehicleTipListItemQr
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string BrandCoreId { get; set; } = string.Empty;
    public string VehicleTypeCoreId { get; set; } = string.Empty;
    public string VehicleSystemCoreId { get; set; } = string.Empty;
    public long Priority { get; set; }
    public bool IsActive { get; set; }
}
