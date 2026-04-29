using Vehicle.Insurance.Core.Contracts.VehicleColors.Commands;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.VehicleColors.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands;
using Zamin.Infra.Data.Sql.Commands.Extensions;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.VehicleColors;

public sealed class VehicleColorCommandRepository : BaseCommandRepository<VehicleColor, VehicleInsuranceCommandDbContext, long>,
    IVehicleColorCommandRepository
{
    public VehicleColorCommandRepository(VehicleInsuranceCommandDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<VehicleColor>> GetByIds(List<long> contriesIds)
        => await _dbContext.VehicleColors.Where(c => contriesIds.Contains(c.Id)).ToListAsync();

    public async Task<long> GetNextPriority()
    {
        var maxPriority = await _dbContext.VehicleColors.IgnoreQueryFilters().MaxAsync(c => c.Priority);
        return maxPriority is not null ? maxPriority.Value + 1 : 1;
    }

    public async Task<List<VehicleColor>> GetSubordinateVehicleColors(Priority current, Priority @new)
        => await _dbContext.VehicleColors.Where(c => c.Priority > current && c.Priority <= @new).ToListAsync();


    public async Task<List<VehicleColor>> GetSuperiorVehicleColors(Priority current, Priority @new)
        => await _dbContext.VehicleColors.Where(c => c.Priority < current && c.Priority >= @new).ToListAsync();

    public async Task<List<VehicleColor>> GetSubordinateVehicleColors(Priority current)
        => await _dbContext.VehicleColors.Where(c => c.Priority > current).ToListAsync();

    public async Task<VehicleColor?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId)
        => await _dbContext.VehicleColors.FirstOrDefaultAsync(c => c.CoreId == coreId);

    public bool IsCreatedByCore(VehicleColor vehicleColor)
    {
        var createdByUserIdObject = _dbContext.GetShadowPropertyValue(vehicleColor, AuditableShadowProperties.CreatedByUserId);
        var canParse = long.TryParse((string?)createdByUserIdObject, out long createdByUserId);
        return createdByUserIdObject is null || !canParse || createdByUserId < 1;
    }

    public async Task<List<VehicleColor>> GetAllAsync()
        => await _dbContext.VehicleColors.ToListAsync();

    public async Task<List<VehicleColor>> GetByTenantId(long tenantId)
        => await _dbContext.VehicleColors
            .IgnoreQueryFilters()
            .Where(c => c.TenantId == tenantId)
            .ToListAsync();
}

