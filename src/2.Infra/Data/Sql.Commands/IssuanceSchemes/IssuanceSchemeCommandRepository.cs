using Master.Data.Core.Contracts.IssuanceSchemes.Commands;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.IssuanceSchemes.Entities;
using Master.Data.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands;
using Zamin.Infra.Data.Sql.Commands.Extensions;

namespace Master.Data.Infra.Data.Sql.Commands.IssuanceSchemes;

public sealed class IssuanceSchemeCommandRepository : BaseCommandRepository<IssuanceScheme, MasterDataCommandDbContext, long>,
    IIssuanceSchemeCommandRepository
{
    public IssuanceSchemeCommandRepository(MasterDataCommandDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<IssuanceScheme>> GetByIds(List<long> contriesIds)
        => await _dbContext.IssuanceSchemes.Where(c => contriesIds.Contains(c.Id)).ToListAsync();

    public async Task<long> GetNextPriority()
    {
        var maxPriority = await _dbContext.IssuanceSchemes.IgnoreQueryFilters().MaxAsync(c => c.Priority);
        return maxPriority is not null ? maxPriority.Value + 1 : 1;
    }

    public async Task<List<IssuanceScheme>> GetSubordinateIssuanceSchemes(Priority current, Priority @new)
        => await _dbContext.IssuanceSchemes.Where(c => c.Priority > current && c.Priority <= @new).ToListAsync();


    public async Task<List<IssuanceScheme>> GetSuperiorIssuanceSchemes(Priority current, Priority @new)
        => await _dbContext.IssuanceSchemes.Where(c => c.Priority < current && c.Priority >= @new).ToListAsync();

    public async Task<List<IssuanceScheme>> GetSubordinateIssuanceSchemes(Priority current)
        => await _dbContext.IssuanceSchemes.Where(c => c.Priority > current).ToListAsync();

    public async Task<IssuanceScheme?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId)
        => await _dbContext.IssuanceSchemes.FirstOrDefaultAsync(c => c.CoreId == coreId);

    public bool IsCreatedByCore(IssuanceScheme issuanceScheme)
    {
        var createdByUserIdObject = _dbContext.GetShadowPropertyValue(issuanceScheme, AuditableShadowProperties.CreatedByUserId);
        var canParse = long.TryParse((string?)createdByUserIdObject, out long createdByUserId);
        return createdByUserIdObject is null || !canParse || createdByUserId < 1;
    }

    public async Task<List<IssuanceScheme>> GetAllAsync()
        => await _dbContext.IssuanceSchemes.ToListAsync();

    public async Task<List<IssuanceScheme>> GetByTenantId(long tenantId)
        => await _dbContext.IssuanceSchemes
            .IgnoreQueryFilters()
            .Where(c => c.TenantId == tenantId)
            .ToListAsync();
}