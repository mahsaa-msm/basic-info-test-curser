using Master.Data.Core.Contracts.InsuranceTypes.Commands;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.InsuranceTypes.Entities;
using Master.Data.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands;
using Zamin.Infra.Data.Sql.Commands.Extensions;

namespace Master.Data.Infra.Data.Sql.Commands.InsuranceTypes;

public sealed class InsuranceTypeCommandRepository : BaseCommandRepository<InsuranceType, MasterDataCommandDbContext, long>,
    IInsuranceTypeCommandRepository
{
    public InsuranceTypeCommandRepository(MasterDataCommandDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<InsuranceType>> GetByIds(List<long> contriesIds)
        => await _dbContext.InsuranceTypes.Where(c => contriesIds.Contains(c.Id)).ToListAsync();

    public async Task<long> GetNextPriority()
    {
        var maxPriority = await _dbContext.InsuranceTypes.IgnoreQueryFilters().MaxAsync(c => c.Priority);
        return maxPriority is not null ? maxPriority.Value + 1 : 1;
    }

    public async Task<List<InsuranceType>> GetSubordinateInsuranceTypes(Priority current, Priority @new)
        => await _dbContext.InsuranceTypes.Where(c => c.Priority > current && c.Priority <= @new).ToListAsync();


    public async Task<List<InsuranceType>> GetSuperiorInsuranceTypes(Priority current, Priority @new)
        => await _dbContext.InsuranceTypes.Where(c => c.Priority < current && c.Priority >= @new).ToListAsync();

    public async Task<List<InsuranceType>> GetSubordinateInsuranceTypes(Priority current)
        => await _dbContext.InsuranceTypes.Where(c => c.Priority > current).ToListAsync();

    public async Task<InsuranceType?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId)
        => await _dbContext.InsuranceTypes.FirstOrDefaultAsync(c => c.CoreId == coreId);

    public bool IsCreatedByCore(InsuranceType insuranceType)
    {
        var createdByUserIdObject = _dbContext.GetShadowPropertyValue(insuranceType, AuditableShadowProperties.CreatedByUserId);
        var canParse = long.TryParse((string?)createdByUserIdObject, out long createdByUserId);
        return createdByUserIdObject is null || !canParse || createdByUserId < 1;
    }

    public async Task<List<InsuranceType>> GetAllAsync()
        => await _dbContext.InsuranceTypes.ToListAsync();

    public async Task<List<InsuranceType>> GetByTenantId(long tenantId)
        => await _dbContext.InsuranceTypes
            .IgnoreQueryFilters()
            .Where(c => c.TenantId == tenantId)
            .ToListAsync();
}