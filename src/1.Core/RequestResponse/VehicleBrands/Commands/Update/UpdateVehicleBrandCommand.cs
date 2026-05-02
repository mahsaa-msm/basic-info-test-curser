using Vehicle.Insurance.Core.Domain.VehicleBrands.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Commands.Update;

public sealed class UpdateVehicleBrandCommand : ICommand, IWebRequest
{
    public long VehicleBrandId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public long Priority { get; set; }

    public UpdateVehicleBrandParameter ToParameter() => new(Title,
        DisplayTitle,
        Priority);

    public string Path => "/Api/VehicleBrand/UpdateVehicleBrand";
}
