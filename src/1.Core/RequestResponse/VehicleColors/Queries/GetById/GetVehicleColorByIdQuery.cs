using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleColors.Queries.GetById;

public sealed class GetVehicleColorByIdQuery : IQuery<VehicleColorQr>, IWebRequest
{
    public long VehicleColorId { get; set; }

    public string Path => "/Api/VehicleColor/GetVehicleColorById";
}

