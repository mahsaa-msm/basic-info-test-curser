using Vehicle.Insurance.Core.Contracts.PatternCatalogs.Commands;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.PatternCatalogs.Entities;
using Vehicle.Insurance.Core.Domain.PatternCatalogs.ValueObjects;
using Vehicle.Insurance.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.PatternCatalogs;

public sealed class PatternCatalogCommandRepository : BaseCommandRepository<PatternCatalog, VehicleInsuranceCommandDbContext, long>,
    IPatternCatalogCommandRepository
{
    public PatternCatalogCommandRepository(VehicleInsuranceCommandDbContext dbContext)
    : base(dbContext)
    {
    }

    public async Task<List<PatternCatalog>> GetByIds(List<long> citiesIds)
        => await _dbContext.PatternCatalogs.Where(c => citiesIds.Contains(c.Id)).ToListAsync();

    public async Task<long> GetNextPriority()
    {
        var maxPriority = await _dbContext.PatternCatalogs.IgnoreQueryFilters().MaxAsync(c => c.Priority);
        return maxPriority is not null ? maxPriority.Value + 1 : 1;
    }

    public async Task<List<PatternCatalog>> GetSubordinatePatternCatalogs(Priority current, Priority @new)
        => await _dbContext.PatternCatalogs.Where(c => c.Priority > current && c.Priority <= @new).ToListAsync();


    public async Task<List<PatternCatalog>> GetSuperiorPatternCatalogs(Priority current, Priority @new)
        => await _dbContext.PatternCatalogs.Where(c => c.Priority < current && c.Priority >= @new).ToListAsync();

    public async Task<List<PatternCatalog>> GetSubordinatePatternCatalogs(Priority current)
        => await _dbContext.PatternCatalogs.Where(c => c.Priority > current).ToListAsync();

    public async Task<List<PatternCatalog>> GetAllAsync()
        => await _dbContext.PatternCatalogs.ToListAsync();

    public async Task<List<PatternCatalog>> GetByTenantId(long tenantId)
        => await _dbContext.PatternCatalogs
            .IgnoreQueryFilters()
            .Where(c => c.TenantId == tenantId)
            .ToListAsync();

    public async Task<PatternCatalog?> GetByKeyAsync(PatternKey key)
        => await _dbContext.PatternCatalogs
                    .FirstOrDefaultAsync(c => c.Key.Equals(key));
}
