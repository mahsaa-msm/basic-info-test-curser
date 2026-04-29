using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Queries.GetById;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.Contracts.VehicleColors.Queries;

public interface IVehicleColorQueryRepository : IQueryRepository
{
    Task<VehicleColorQr> Execute(GetVehicleColorByIdQuery query);
    Task<List<VehicleColorSelectItemQr>> Execute(GetAllVehicleColorQuery query);
    Task<PagedData<VehicleColorListItemQr>> Execute(GetAllVehicleColorsPagedFilterQuery query);
}


