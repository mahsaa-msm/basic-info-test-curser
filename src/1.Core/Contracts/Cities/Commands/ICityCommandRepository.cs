using Master.Data.Core.Domain.Cities.Entities;
using Master.Data.Core.Domain.Common.ValueObjects;
using Zamin.Core.Contracts.Data.Commands;

namespace Master.Data.Core.Contracts.Cities.Commands;
public interface ICityCommandRepository : ICommandRepository<City, long>
{
    Task<City?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId);
    Task<List<City>> GetByIds(List<long> citiesIds);
    Task<List<City>> GetByTenantId(long tenantId);
    Task<List<City>> GetAllAsync();
    Task<List<City>> GetSubordinateCities(Priority current, Priority @new);
    Task<List<City>> GetSubordinateCities(Priority current);
    Task<List<City>> GetSuperiorCities(Priority current, Priority @new);
    Task<long> GetNextPriority();
    bool IsCreatedByCore(City city);
}