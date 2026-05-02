using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Queries.GetAll;

public sealed class GetAllVehicleBrandQuery : IQuery<List<VehicleBrandSelectItemQr>>, IWebRequest
{
    public bool? IsActive { get; set; }

    public string Path => "/Api/VehicleBrand/GetAllVehicleBrands";
}
