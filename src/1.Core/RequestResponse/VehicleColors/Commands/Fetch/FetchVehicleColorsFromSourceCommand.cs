using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.Fetch;

public sealed class FetchVehicleColorsFromSourceCommand : ICommand, IWebRequest
{
    public string Path => "/Api/VehicleColor/FetchVehicleColorsFromSource";
}

