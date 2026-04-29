using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.Delete;

public sealed class DeleteVehicleColorCommand : ICommand, IWebRequest
{
    public long VehicleColorId { get; set; }

    public string Path => "/Api/VehicleColor/DeleteVehicleColor";
}

