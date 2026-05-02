using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Queries.GetById;

public sealed class GetVehicleBrandByIdQuery : IQuery<VehicleBrandQr?>, IWebRequest
{
    public long VehicleBrandId { get; set; }

    public string Path => "/Api/VehicleBrand/GetVehicleBrandById";
}
