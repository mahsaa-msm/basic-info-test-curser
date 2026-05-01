using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.VehicleColors.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace Vehicle.Insurance.Core.Contracts.VehicleColors.Commands;

public interface IVehicleColorCommandRepository : ICommandRepository<VehicleColor, long>
{
    Task<VehicleColor?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId);
    Task<List<VehicleColor>> GetByIds(List<long> contriesIds);
    Task<List<VehicleColor>> GetByTenantId(long tenantId);
    Task<List<VehicleColor>> GetAllAsync();
    Task<List<VehicleColor>> GetSubordinateVehicleColors(Priority current, Priority @new);
    Task<List<VehicleColor>> GetSubordinateVehicleColors(Priority current);
    Task<List<VehicleColor>> GetSuperiorVehicleColors(Priority current, Priority @new);
    Task<long> GetNextPriority();
    bool IsCreatedByCore(VehicleColor vehicleColor);
    void DeletePhysical(VehicleColor vehicleColor);
}

