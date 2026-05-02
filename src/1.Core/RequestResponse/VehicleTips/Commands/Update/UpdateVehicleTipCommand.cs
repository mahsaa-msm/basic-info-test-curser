using Vehicle.Insurance.Core.Domain.VehicleTips.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleTips.Commands.Update;

public sealed class UpdateVehicleTipCommand : ICommand, IWebRequest
{
    public long VehicleTipId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string BrandCoreId { get; set; } = string.Empty;
    public string VehicleTypeCoreId { get; set; } = string.Empty;
    public string VehicleSystemCoreId { get; set; } = string.Empty;
    public long Priority { get; set; }

    public UpdateVehicleTipParameter ToParameter() => new(Title,
        DisplayTitle,
        BrandCoreId,
        VehicleTypeCoreId,
        VehicleSystemCoreId,
        Priority);

    public string Path => "/Api/VehicleTip/UpdateVehicleTip";
}
