using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleTips.Queries.GetById;

public sealed class GetVehicleTipByIdQuery : IQuery<VehicleTipQr?>, IWebRequest
{
    public long VehicleTipId { get; set; }

    public string Path => "/Api/VehicleTip/GetVehicleTipById";
}
