namespace Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Queries.GetById;

public sealed class VehicleBrandQr
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public long Priority { get; set; }
    public bool IsActive { get; set; }
    public bool IsEditable { get; set; }
}
