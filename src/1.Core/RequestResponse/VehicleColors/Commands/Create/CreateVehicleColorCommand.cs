using Vehicle.Insurance.Core.Domain.VehicleColors.Parameters;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.Create;

public sealed class CreateVehicleColorCommand : ICommand<long>, IWebRequest
{
    public string Title { get; set; } = string.Empty;
    public string? DisplayTitle { get; set; }
    public string ColorHash { get; set; } = string.Empty;
    public string CoreId { get; set; } = string.Empty;

    public CreateVehicleColorParameter ToCreateParameter(long priority) => new(Title,
                                                                         DisplayTitle,
                                                                         CoreId,
                                                                         ColorHash,
                                                                         priority);

    public string Path => "/Api/VehicleColor/CreateVehicleColor";
}



