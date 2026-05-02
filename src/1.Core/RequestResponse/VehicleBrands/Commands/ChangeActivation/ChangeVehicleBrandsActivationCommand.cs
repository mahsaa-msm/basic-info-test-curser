using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Commands.ChangeActivation;

public sealed class ChangeVehicleBrandsActivationCommand : ICommand, IWebRequest
{
    public List<long> VehicleBrandsId { get; set; } = new();
    public bool IsActive { get; set; }

    public string Path => "/Api/VehicleBrand/ChangeVehicleBrandsActivation";
}
