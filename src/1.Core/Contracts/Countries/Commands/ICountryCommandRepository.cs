using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.Countries.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace Vehicle.Insurance.Core.Contracts.Countries.Commands;

public interface ICountryCommandRepository : ICommandRepository<Country, long>
{
    Task<Country?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId);
    Task<List<Country>> GetByIds(List<long> contriesIds);
    Task<List<Country>> GetByTenantId(long tenantId);
    Task<List<Country>> GetAllAsync();
    Task<List<Country>> GetSubordinateCountries(Priority current, Priority @new);
    Task<List<Country>> GetSubordinateCountries(Priority current);
    Task<List<Country>> GetSuperiorCountries(Priority current, Priority @new);
    Task<long> GetNextPriority();
    bool IsCreatedByCore(Country country);
}
