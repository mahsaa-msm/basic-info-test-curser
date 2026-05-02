using Vehicle.Insurance.Core.Contracts.VehicleTips.Commands;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.VehicleTips.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands;
using Zamin.Infra.Data.Sql.Commands.Extensions;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.VehicleTips;

public sealed class VehicleTipCommandRepository :
    BaseCommandRepository<VehicleTip, VehicleInsuranceCommandDbContext, long>,
    IVehicleTipCommandRepository
{
    public VehicleTipCommandRepository(VehicleInsuranceCommandDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<VehicleTip>> GetByIds(List<long> ids)
        => await _dbContext.VehicleTips.Where(c => ids.Contains(c.Id)).ToListAsync();

    public async Task<long> GetNextPriority()
    {
        var maxPriority = await _dbContext.VehicleTips.MaxAsync(c => c.Priority);
        return maxPriority is not null ? maxPriority.Value + 1 : 1;
    }

    public async Task<List<VehicleTip>> GetSubordinateVehicleTips(Priority current, Priority @new)
        => await _dbContext.VehicleTips.Where(c => c.Priority > current && c.Priority <= @new).ToListAsync();

    public async Task<List<VehicleTip>> GetSuperiorVehicleTips(Priority current, Priority @new)
        => await _dbContext.VehicleTips.Where(c => c.Priority < current && c.Priority >= @new).ToListAsync();

    public async Task<List<VehicleTip>> GetSubordinateVehicleTips(Priority current)
        => await _dbContext.VehicleTips.Where(c => c.Priority > current).ToListAsync();

    public async Task<VehicleTip?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId)
        => await _dbContext.VehicleTips.FirstOrDefaultAsync(c => c.CoreId == coreId);

    public bool IsCreatedByCore(VehicleTip entity)
    {
        var createdByUserIdObject = _dbContext.GetShadowPropertyValue(entity, AuditableShadowProperties.CreatedByUserId);
        var canParse = long.TryParse((string?)createdByUserIdObject, out long createdByUserId);
        return createdByUserIdObject is null || !canParse || createdByUserId < 1;
    }
}
