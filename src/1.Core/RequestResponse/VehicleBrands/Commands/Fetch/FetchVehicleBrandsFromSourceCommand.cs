using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Commands.Fetch;

public sealed class FetchVehicleBrandsFromSourceCommand : ICommand, IWebRequest
{
    public string Path => "/Api/VehicleBrand/FetchVehicleBrandsFromSource";
}
