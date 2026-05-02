using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Queries.GetById;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.Contracts.VehicleBrands.Queries;

public interface IVehicleBrandQueryRepository : IQueryRepository
{
    Task<VehicleBrandQr?> Execute(GetVehicleBrandByIdQuery query);
    Task<List<VehicleBrandSelectItemQr>> Execute(GetAllVehicleBrandQuery query);
    Task<PagedData<VehicleBrandListItemQr>> Execute(GetAllVehicleBrandsPagedFilterQuery query);
}
