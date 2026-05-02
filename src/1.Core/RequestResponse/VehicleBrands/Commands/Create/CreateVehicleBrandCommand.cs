using Vehicle.Insurance.Core.Domain.VehicleBrands.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Commands.Create;

public sealed class CreateVehicleBrandCommand : ICommand<long>, IWebRequest
{
    public string Title { get; set; } = string.Empty;
    public string? DisplayTitle { get; set; }
    public string CoreId { get; set; } = string.Empty;

    public CreateVehicleBrandParameter ToCreateParameter(long priority) => new(Title,
        DisplayTitle,
        CoreId,
        priority);

    public string Path => "/Api/VehicleBrand/CreateVehicleBrand";
}
