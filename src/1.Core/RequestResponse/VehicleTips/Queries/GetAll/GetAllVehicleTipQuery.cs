using Master.Data.Core.RequestResponse.VehicleTips.Queries.CommonResults;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.VehicleTips.Queries.GetAll;
public sealed class GetAllVehicleTipQuery : IQuery<List<VehicleTipItemQr>>
{
    public int VehicleTypeId { get; set; }
    public int VehicleBrandId { get; set; }
}
