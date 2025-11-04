using Master.Data.Core.Contracts.Countries.Commands;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.Countries.Entities;
using Master.Data.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands;
using Zamin.Infra.Data.Sql.Commands.Extensions;

namespace Master.Data.Infra.Data.Sql.Commands.Countries;

public class CountryCommandRepository : BaseCommandRepository<Country, MasterDataCommandDbContext, long>,
    ICountryCommandRepository
{
    public CountryCommandRepository(MasterDataCommandDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Country>> GetByIds(List<long> contriesIds)
        => await _dbContext.Countries.Where(c => contriesIds.Contains(c.Id)).ToListAsync();

    public async Task<long> GetNextPriority()
    {
        var maxPriority = await _dbContext.Countries.IgnoreQueryFilters().MaxAsync(c => c.Priority);
        return maxPriority is not null ? maxPriority.Value + 1 : 1;
    }

    public async Task<List<Country>> GetSubordinateCountries(Priority current, Priority @new)
        => await _dbContext.Countries.Where(c => c.Priority > current && c.Priority <= @new).ToListAsync();


    public async Task<List<Country>> GetSuperiorCountries(Priority current, Priority @new)
        => await _dbContext.Countries.Where(c => c.Priority < current && c.Priority >= @new).ToListAsync();

    public async Task<List<Country>> GetSubordinateCountries(Priority current)
        => await _dbContext.Countries.Where(c => c.Priority > current).ToListAsync();

    public async Task<Country?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId)
        => await _dbContext.Countries.FirstOrDefaultAsync(c => c.CoreId == coreId);

    public bool IsCreatedByCore(Country country)
        => _dbContext.GetShadowPropertyValue(country, AuditableShadowProperties.CreatedByUserId) is null;

    public async Task<List<Country>> GetAllIgnoreQueryFiltersAsync()
        => await _dbContext.Countries.IgnoreQueryFilters().ToListAsync();
}