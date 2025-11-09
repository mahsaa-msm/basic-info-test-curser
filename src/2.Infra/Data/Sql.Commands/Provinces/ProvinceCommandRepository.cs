using Master.Data.Core.Contracts.Countries.Commands;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands.Extensions;
using Zamin.Infra.Data.Sql.Commands;
using Master.Data.Core.Domain.Provinces.Entities;
using Master.Data.Core.Contracts.Provinces.Commands;

namespace Master.Data.Infra.Data.Sql.Commands.Provinces;
public sealed class ProvinceCommandRepository : BaseCommandRepository<Province, MasterDataCommandDbContext, long>,
    IProvinceCommandRepository
{
    public ProvinceCommandRepository(MasterDataCommandDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<List<Province>> GetByIds(List<long> provincesIds)
        => await _dbContext.Provinces.Where(c => provincesIds.Contains(c.Id)).ToListAsync();

    public async Task<long> GetNextPriority()
    {
        var maxPriority = await _dbContext.Provinces.IgnoreQueryFilters().MaxAsync(c => c.Priority);
        return maxPriority is not null ? maxPriority.Value + 1 : 1;
    }

    public async Task<List<Province>> GetSubordinateProvinces(Priority current, Priority @new)
        => await _dbContext.Provinces.Where(c => c.Priority > current && c.Priority <= @new).ToListAsync();


    public async Task<List<Province>> GetSuperiorProvinces(Priority current, Priority @new)
        => await _dbContext.Provinces.Where(c => c.Priority < current && c.Priority >= @new).ToListAsync();

    public async Task<List<Province>> GetSubordinateProvinces(Priority current)
        => await _dbContext.Provinces.Where(c => c.Priority > current).ToListAsync();

    public async Task<Province?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId)
        => await _dbContext.Provinces.FirstOrDefaultAsync(c => c.CoreId == coreId);

    public bool IsCreatedByCore(Province province)
        => _dbContext.GetShadowPropertyValue(province, AuditableShadowProperties.CreatedByUserId) is null;

    public async Task<List<Province>> GetAllAsync()
        => await _dbContext.Provinces.ToListAsync();

    public async Task<List<Province>> GetByTenantId(long tenantId)
        => await _dbContext.Provinces
            .IgnoreQueryFilters()
            .Where(c => c.TenantId == tenantId)
            .ToListAsync();
}