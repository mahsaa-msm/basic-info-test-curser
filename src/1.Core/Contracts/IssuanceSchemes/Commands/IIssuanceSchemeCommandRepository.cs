using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.IssuanceSchemes.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace Master.Data.Core.Contracts.IssuanceSchemes.Commands;
public interface IIssuanceSchemeCommandRepository : ICommandRepository<IssuanceScheme, long>
{
    Task<IssuanceScheme?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId);
    Task<List<IssuanceScheme>> GetByIds(List<long> contriesIds);
    Task<List<IssuanceScheme>> GetByTenantId(long tenantId);
    Task<List<IssuanceScheme>> GetAllAsync();
    Task<List<IssuanceScheme>> GetSubordinateIssuanceSchemes(Priority current, Priority @new);
    Task<List<IssuanceScheme>> GetSubordinateIssuanceSchemes(Priority current);
    Task<List<IssuanceScheme>> GetSuperiorIssuanceSchemes(Priority current, Priority @new);
    Task<long> GetNextPriority();
    bool IsCreatedByCore(IssuanceScheme issuanceScheme);
}