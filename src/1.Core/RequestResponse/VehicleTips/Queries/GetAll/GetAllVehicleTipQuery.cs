using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleTips.Queries.GetAll;

public sealed class GetAllVehicleTipQuery : IQuery<List<VehicleTipSelectItemQr>>, IWebRequest
{
    public bool? IsActive { get; set; }

    public string Path => "/Api/VehicleTip/GetAllVehicleTips";
}
