using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.ChangeActivation;

public sealed class ChangeVehicleColorsActivationCommand : ICommand, IWebRequest
{
    public List<long> VehicleColorsId { get; set; } = new();
    public bool IsActive { get; set; }

    public string Path => "/Api/VehicleColor/ChangeVehicleColorsActivation";
}

