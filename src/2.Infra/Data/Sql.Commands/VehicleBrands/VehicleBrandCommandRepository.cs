using Vehicle.Insurance.Core.Contracts.VehicleBrands.Commands;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.VehicleBrands.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands;
using Zamin.Infra.Data.Sql.Commands.Extensions;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.VehicleBrands;

public sealed class VehicleBrandCommandRepository :
    BaseCommandRepository<VehicleBrand, VehicleInsuranceCommandDbContext, long>,
    IVehicleBrandCommandRepository
{
    public VehicleBrandCommandRepository(VehicleInsuranceCommandDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<VehicleBrand>> GetByIds(List<long> ids)
        => await _dbContext.VehicleBrands.Where(c => ids.Contains(c.Id)).ToListAsync();

    public async Task<long> GetNextPriority()
    {
        var maxPriority = await _dbContext.VehicleBrands.MaxAsync(c => c.Priority);
        return maxPriority is not null ? maxPriority.Value + 1 : 1;
    }

    public async Task<List<VehicleBrand>> GetSubordinateVehicleBrands(Priority current, Priority @new)
        => await _dbContext.VehicleBrands
            .Where(c => c.Priority > current && c.Priority <= @new).ToListAsync();

    public async Task<List<VehicleBrand>> GetSuperiorVehicleBrands(Priority current, Priority @new)
        => await _dbContext.VehicleBrands
            .Where(c => c.Priority < current && c.Priority >= @new).ToListAsync();

    public async Task<List<VehicleBrand>> GetSubordinateVehicleBrands(Priority current)
        => await _dbContext.VehicleBrands.Where(c => c.Priority > current).ToListAsync();

    public async Task<VehicleBrand?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId)
        => await _dbContext.VehicleBrands.FirstOrDefaultAsync(c => c.CoreId == coreId);

    public bool IsCreatedByCore(VehicleBrand entity)
    {
        var createdByUserIdObject = _dbContext.GetShadowPropertyValue(entity, AuditableShadowProperties.CreatedByUserId);
        var canParse = long.TryParse((string?)createdByUserIdObject, out long createdByUserId);
        return createdByUserIdObject is null || !canParse || createdByUserId < 1;
    }
}
