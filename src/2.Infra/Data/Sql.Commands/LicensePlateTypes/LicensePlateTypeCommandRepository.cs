using Vehicle.Insurance.Core.Contracts.LicensePlateTypes.Commands;
using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.LicensePlateTypes.Entities;
using Vehicle.Insurance.Infra.Data.Sql.Commands.Common;
using Microsoft.EntityFrameworkCore;
using Zamin.Infra.Data.Sql.Commands;
using Zamin.Infra.Data.Sql.Commands.Extensions;

namespace Vehicle.Insurance.Infra.Data.Sql.Commands.LicensePlateTypes;

public sealed class LicensePlateTypeCommandRepository :
    BaseCommandRepository<LicensePlateType, VehicleInsuranceCommandDbContext, long>,
    ILicensePlateTypeCommandRepository
{
    public LicensePlateTypeCommandRepository(VehicleInsuranceCommandDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<LicensePlateType>> GetByIds(List<long> ids)
        => await _dbContext.LicensePlateTypes.Where(c => ids.Contains(c.Id)).ToListAsync();

    public async Task<long> GetNextPriority()
    {
        var maxPriority = await _dbContext.LicensePlateTypes.MaxAsync(c => c.Priority);
        return maxPriority is not null ? maxPriority.Value + 1 : 1;
    }

    public async Task<List<LicensePlateType>> GetSubordinateLicensePlateTypes(Priority current, Priority @new)
        => await _dbContext.LicensePlateTypes
            .Where(c => c.Priority > current && c.Priority <= @new).ToListAsync();

    public async Task<List<LicensePlateType>> GetSuperiorLicensePlateTypes(Priority current, Priority @new)
        => await _dbContext.LicensePlateTypes
            .Where(c => c.Priority < current && c.Priority >= @new).ToListAsync();

    public async Task<List<LicensePlateType>> GetSubordinateLicensePlateTypes(Priority current)
        => await _dbContext.LicensePlateTypes.Where(c => c.Priority > current).ToListAsync();

    public async Task<LicensePlateType?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId)
        => await _dbContext.LicensePlateTypes.FirstOrDefaultAsync(c => c.CoreId == coreId);

    public bool IsCreatedByCore(LicensePlateType entity)
    {
        var createdByUserIdObject = _dbContext.GetShadowPropertyValue(entity, AuditableShadowProperties.CreatedByUserId);
        var canParse = long.TryParse((string?)createdByUserIdObject, out long createdByUserId);
        return createdByUserIdObject is null || !canParse || createdByUserId < 1;
    }
}
