using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.PatternCatalogs.Entities;
using Vehicle.Insurance.Core.Domain.PatternCatalogs.ValueObjects;
using Zamin.Core.Contracts.Data.Commands;

namespace Vehicle.Insurance.Core.Contracts.PatternCatalogs.Commands;

public interface IPatternCatalogCommandRepository : ICommandRepository<PatternCatalog, long>
{
    Task<List<PatternCatalog>> GetByIds(List<long> patternCatalogsIds);
    Task<List<PatternCatalog>> GetByTenantId(long tenantId);
    Task<List<PatternCatalog>> GetAllAsync();
    Task<List<PatternCatalog>> GetSubordinatePatternCatalogs(Priority current, Priority @new);
    Task<List<PatternCatalog>> GetSubordinatePatternCatalogs(Priority current);
    Task<List<PatternCatalog>> GetSuperiorPatternCatalogs(Priority current, Priority @new);
    Task<long> GetNextPriority();
    Task<PatternCatalog?> GetByKeyAsync(PatternKey key);
}
