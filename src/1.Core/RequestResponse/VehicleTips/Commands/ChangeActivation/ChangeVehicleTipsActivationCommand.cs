using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleTips.Commands.ChangeActivation;

public sealed class ChangeVehicleTipsActivationCommand : ICommand, IWebRequest
{
    public List<long> VehicleTipsId { get; set; } = new();
    public bool IsActive { get; set; }

    public string Path => "/Api/VehicleTip/ChangeVehicleTipsActivation";
}
