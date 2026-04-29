using Vehicle.Insurance.Core.Domain.VehicleColors.Parameters;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleColors.Commands.Update;

public sealed class UpdateVehicleColorCommand : ICommand, IWebRequest
{
    public long VehicleColorId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DisplayTitle { get; set; } = string.Empty;
    public string ColorHash { get; set; } = string.Empty;
    public long Priority { get; set; }

    public UpdateVehicleColorParameter ToParameter() => new(Title,
                                                             DisplayTitle,
                                                             ColorHash,
                                                             Priority);

    public string Path => "/Api/VehicleColor/UpdateVehicleColor";
}



