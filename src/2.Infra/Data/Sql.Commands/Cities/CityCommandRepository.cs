using Master.Data.Core.Contracts.Cities.Commands;
using Master.Data.Core.Domain.Cities.Entities;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands;
using Zamin.Infra.Data.Sql.Commands.Extensions;

namespace Master.Data.Infra.Data.Sql.Commands.Cities;

public sealed class CityCommandRepository : BaseCommandRepository<City, MasterDataCommandDbContext, long>,
    ICityCommandRepository
{
    public CityCommandRepository(MasterDataCommandDbContext dbContext)
    : base(dbContext)
    {
    }

    public async Task<List<City>> GetByIds(List<long> citiesIds)
        => await _dbContext.Cities.Where(c => citiesIds.Contains(c.Id)).ToListAsync();

    public async Task<long> GetNextPriority()
    {
        var maxPriority = await _dbContext.Cities.IgnoreQueryFilters().MaxAsync(c => c.Priority);
        return maxPriority is not null ? maxPriority.Value + 1 : 1;
    }

    public async Task<List<City>> GetSubordinateCities(Priority current, Priority @new)
        => await _dbContext.Cities.Where(c => c.Priority > current && c.Priority <= @new).ToListAsync();


    public async Task<List<City>> GetSuperiorCities(Priority current, Priority @new)
        => await _dbContext.Cities.Where(c => c.Priority < current && c.Priority >= @new).ToListAsync();

    public async Task<List<City>> GetSubordinateCities(Priority current)
        => await _dbContext.Cities.Where(c => c.Priority > current).ToListAsync();

    public async Task<City?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId)
        => await _dbContext.Cities.FirstOrDefaultAsync(c => c.CoreId == coreId);

    public bool IsCreatedByCore(City city)
    {
        var createdByUserIdObject = _dbContext.GetShadowPropertyValue(city, AuditableShadowProperties.CreatedByUserId);
        var canParse = long.TryParse((string?)createdByUserIdObject, out long createdByUserId);
        return createdByUserIdObject is null || !canParse || createdByUserId < 1;
    }

    public async Task<List<City>> GetAllAsync()
        => await _dbContext.Cities.ToListAsync();

    public async Task<List<City>> GetByTenantId(long tenantId)
        => await _dbContext.Cities
            .IgnoreQueryFilters()
            .Where(c => c.TenantId == tenantId)
            .ToListAsync();
}