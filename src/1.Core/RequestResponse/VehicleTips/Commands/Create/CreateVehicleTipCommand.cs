using Vehicle.Insurance.Core.Domain.VehicleTips.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleTips.Commands.Create;

public sealed class CreateVehicleTipCommand : ICommand<long>, IWebRequest
{
    public string Title { get; set; } = string.Empty;
    public string? DisplayTitle { get; set; }
    public string CoreId { get; set; } = string.Empty;
    public string BrandCoreId { get; set; } = string.Empty;
    public string VehicleTypeCoreId { get; set; } = string.Empty;
    public string VehicleSystemCoreId { get; set; } = string.Empty;

    public CreateVehicleTipParameter ToCreateParameter(long priority) => new(Title,
        DisplayTitle,
        CoreId,
        BrandCoreId,
        VehicleTypeCoreId,
        VehicleSystemCoreId,
        priority);

    public string Path => "/Api/VehicleTip/CreateVehicleTip";
}
