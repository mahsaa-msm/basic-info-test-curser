using Master.Data.Core.Contracts.InsuranceUnits.Commands;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.InsuranceUnits.Entities;
using Master.Data.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands;
using Zamin.Infra.Data.Sql.Commands.Extensions;

namespace Master.Data.Infra.Data.Sql.Commands.InsuranceUnits;

public sealed class InsuranceUnitCommandRepository : BaseCommandRepository<InsuranceUnit, MasterDataCommandDbContext, long>,
    IInsuranceUnitCommandRepository
{
    public InsuranceUnitCommandRepository(MasterDataCommandDbContext dbContext)
    : base(dbContext)
    {
    }

    public async Task<List<InsuranceUnit>> GetByIds(List<long> insuranceUnitIds)
        => await _dbContext.InsuranceUnits.Where(c => insuranceUnitIds.Contains(c.Id)).ToListAsync();

    public async Task<long> GetNextPriority()
    {
        var maxPriority = await _dbContext.InsuranceUnits.IgnoreQueryFilters().MaxAsync(c => c.Priority);
        return maxPriority is not null ? maxPriority.Value + 1 : 1;
    }

    public async Task<List<InsuranceUnit>> GetSubordinateInsuranceUnits(Priority current, Priority @new)
        => await _dbContext.InsuranceUnits.Where(c => c.Priority > current && c.Priority <= @new).ToListAsync();


    public async Task<List<InsuranceUnit>> GetSuperiorInsuranceUnits(Priority current, Priority @new)
        => await _dbContext.InsuranceUnits.Where(c => c.Priority < current && c.Priority >= @new).ToListAsync();

    public async Task<List<InsuranceUnit>> GetSubordinateInsuranceUnits(Priority current)
        => await _dbContext.InsuranceUnits.Where(c => c.Priority > current).ToListAsync();

    public async Task<InsuranceUnit?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId)
        => await _dbContext.InsuranceUnits.FirstOrDefaultAsync(c => c.CoreId == coreId);

    public bool IsCreatedByCore(InsuranceUnit insuranceUnit)
    {
        var createdByUserIdObject = _dbContext.GetShadowPropertyValue(insuranceUnit, AuditableShadowProperties.CreatedByUserId);
        var canParse = long.TryParse((string?)createdByUserIdObject, out long createdByUserId);
        return createdByUserIdObject is null || !canParse || createdByUserId < 1;
    }

    public async Task<List<InsuranceUnit>> GetAllAsync()
        => await _dbContext.InsuranceUnits.ToListAsync();

    public async Task<List<InsuranceUnit>> GetByTenantId(long tenantId)
        => await _dbContext.InsuranceUnits
            .IgnoreQueryFilters()
            .Where(c => c.TenantId == tenantId)
            .ToListAsync();
}