using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Queries.GetById;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.Contracts.VehicleTips.Queries;

public interface IVehicleTipQueryRepository : IQueryRepository
{
    Task<VehicleTipQr?> Execute(GetVehicleTipByIdQuery query);
    Task<List<VehicleTipSelectItemQr>> Execute(GetAllVehicleTipQuery query);
    Task<PagedData<VehicleTipListItemQr>> Execute(GetAllVehicleTipsPagedFilterQuery query);
}
