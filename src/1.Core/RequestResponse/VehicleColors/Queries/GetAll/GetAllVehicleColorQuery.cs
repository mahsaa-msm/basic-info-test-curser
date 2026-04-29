using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleColors.Queries.GetAll;

public sealed class GetAllVehicleColorQuery : IQuery<List<VehicleColorSelectItemQr>>, IWebRequest
{
    public bool? IsActive { get; set; }

    public string Path => "/Api/VehicleColor/GetAllVehicleColors";
}

