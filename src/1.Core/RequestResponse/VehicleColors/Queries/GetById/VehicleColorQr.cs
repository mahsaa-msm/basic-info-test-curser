using Vehicle.Insurance.Core.Resources;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleColors.Queries.GetById;

public sealed class VehicleColorQr
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;
    public string ColorHash { get; set; } = string.Empty;
    public long Priority { get; set; }
    public bool IsActive { get; set; }
    public bool IsEditable { get; set; }

}



