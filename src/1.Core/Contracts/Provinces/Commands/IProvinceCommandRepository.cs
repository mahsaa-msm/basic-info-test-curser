using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.Provinces.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace Master.Data.Core.Contracts.Provinces.Commands;

public interface IProvinceCommandRepository : ICommandRepository<Province, long>
{
    Task<Province?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId);
    Task<List<Province>> GetByIds(List<long> provincesIds);
    Task<List<Province>> GetByTenantId(long tenantId);
    Task<List<Province>> GetAllAsync();
    Task<List<Province>> GetSubordinateProvinces(Priority current, Priority @new);
    Task<List<Province>> GetSubordinateProvinces(Priority current);
    Task<List<Province>> GetSuperiorProvinces(Priority current, Priority @new);
    Task<long> GetNextPriority();
    bool IsCreatedByCore(Province province);
}