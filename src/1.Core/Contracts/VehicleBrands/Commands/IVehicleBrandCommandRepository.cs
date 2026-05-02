using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.VehicleBrands.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace Vehicle.Insurance.Core.Contracts.VehicleBrands.Commands;

public interface IVehicleBrandCommandRepository : ICommandRepository<VehicleBrand, long>
{
    Task<VehicleBrand?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId);
    Task<List<VehicleBrand>> GetByIds(List<long> ids);
    Task<long> GetNextPriority();
    Task<List<VehicleBrand>> GetSubordinateVehicleBrands(Priority current, Priority @new);
    Task<List<VehicleBrand>> GetSubordinateVehicleBrands(Priority current);
    Task<List<VehicleBrand>> GetSuperiorVehicleBrands(Priority current, Priority @new);
    bool IsCreatedByCore(VehicleBrand entity);
}
