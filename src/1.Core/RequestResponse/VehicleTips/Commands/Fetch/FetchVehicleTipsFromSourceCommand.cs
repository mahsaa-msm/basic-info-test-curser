using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleTips.Commands.Fetch;

public sealed class FetchVehicleTipsFromSourceCommand : ICommand, IWebRequest
{
    public string Path => "/Api/VehicleTip/FetchVehicleTipsFromSource";
}
