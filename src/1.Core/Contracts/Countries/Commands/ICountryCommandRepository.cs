using Master.Data.Core.Domain.Common.ValueObjects;
using Master.Data.Core.Domain.Countries.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace Master.Data.Core.Contracts.Countries.Commands;
public interface ICountryCommandRepository : ICommandRepository<Country, long>
{
    Task<Country?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId);
    Task<List<Country>> GetByIds(List<long> contriesIds);
    Task<List<Country>> GetAllIgnoreQueryFiltersAsync();
    Task<List<Country>> GetSubordinateCountries(Priority current, Priority @new);
    Task<List<Country>> GetSubordinateCountries(Priority current);
    Task<List<Country>> GetSuperiorCountries(Priority current, Priority @new);
    Task<long> GetNextPriority();
    bool IsCreatedByCore(Country country);
}