using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.VehicleTips.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace Vehicle.Insurance.Core.Contracts.VehicleTips.Commands;

public interface IVehicleTipCommandRepository : ICommandRepository<VehicleTip, long>
{
    Task<VehicleTip?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId);
    Task<List<VehicleTip>> GetByIds(List<long> ids);
    Task<long> GetNextPriority();
    Task<List<VehicleTip>> GetSubordinateVehicleTips(Priority current, Priority @new);
    Task<List<VehicleTip>> GetSubordinateVehicleTips(Priority current);
    Task<List<VehicleTip>> GetSuperiorVehicleTips(Priority current, Priority @new);
    bool IsCreatedByCore(VehicleTip entity);
}
