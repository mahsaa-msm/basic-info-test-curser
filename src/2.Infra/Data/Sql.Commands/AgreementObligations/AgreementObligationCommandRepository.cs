using Master.Data.Core.Contracts.AgreementObligations.Commands;
using Master.Data.Core.Domain.AgreementObligations.Entities;
using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands;
using Zamin.Infra.Data.Sql.Commands.Extensions;

namespace Master.Data.Infra.Data.Sql.Commands.AgreementObligations;

public sealed class AgreementObligationCommandRepository : BaseCommandRepository<AgreementObligation, MasterDataCommandDbContext, long>,
    IAgreementObligationCommandRepository
{
    public AgreementObligationCommandRepository(MasterDataCommandDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<AgreementObligation>> GetAllAsync()
        => await _dbContext.AgreementObligations.ToListAsync();

    public async Task<AgreementObligation?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId)
        => await _dbContext.AgreementObligations.FirstOrDefaultAsync(c => c.CoreId == coreId);

    public async Task<List<AgreementObligation>> GetByIds(List<long> agreementObligationIds)
        => await _dbContext.AgreementObligations.Where(c => agreementObligationIds.Contains(c.Id)).ToListAsync();

    public async Task<List<AgreementObligation>> GetByTenantId(long tenantId)
        => await _dbContext.AgreementObligations
            .IgnoreQueryFilters()
            .Where(c => c.TenantId == tenantId)
            .ToListAsync();

    public async Task<long> GetNextPriority()
    {
        var maxPriority = await _dbContext.AgreementObligations.IgnoreQueryFilters().MaxAsync(c => c.Priority);
        return maxPriority is not null ? maxPriority.Value + 1 : 1;
    }

    public async Task<List<AgreementObligation>> GetSubordinateAgreementObligations(Priority current, Priority @new)
        => await _dbContext.AgreementObligations.Where(c => c.Priority > current && c.Priority <= @new).ToListAsync();

    public async Task<List<AgreementObligation>> GetSubordinateAgreementObligations(Priority current)
        => await _dbContext.AgreementObligations.Where(c => c.Priority > current).ToListAsync();

    public async Task<List<AgreementObligation>> GetSuperiorAgreementObligations(Priority current, Priority @new)
        => await _dbContext.AgreementObligations.Where(c => c.Priority < current && c.Priority >= @new).ToListAsync();

    public bool IsCreatedByCore(AgreementObligation agreementObligation)
    {
        var createdByUserIdObject = _dbContext.GetShadowPropertyValue(agreementObligation, AuditableShadowProperties.CreatedByUserId);
        var canParse = long.TryParse((string?)createdByUserIdObject, out long createdByUserId);
        return createdByUserIdObject is null || !canParse || createdByUserId < 1;
    }
}
