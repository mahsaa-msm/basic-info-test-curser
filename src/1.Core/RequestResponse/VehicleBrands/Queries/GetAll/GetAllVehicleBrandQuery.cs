using Master.Data.Core.RequestResponse.VehicleBrands.Queries.CommonResults;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.VehicleBrands.Queries.GetAll;
public sealed class GetAllVehicleBrandQuery : IQuery<List<VehicleBrandItemQr>>
{
    public int VehicleTypeId { get; set; }
}
