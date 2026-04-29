using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.InsuranceUnits.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace Vehicle.Insurance.Core.Contracts.InsuranceUnits.Commands;

public interface IInsuranceUnitCommandRepository : ICommandRepository<InsuranceUnit, long>
{
    Task<InsuranceUnit?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId);
    Task<List<InsuranceUnit>> GetByIds(List<long> insuranceUnitIds);
    Task<List<InsuranceUnit>> GetByTenantId(long tenantId);
    Task<List<InsuranceUnit>> GetAllAsync();
    Task<List<InsuranceUnit>> GetSubordinateInsuranceUnits(Priority current, Priority @new);
    Task<List<InsuranceUnit>> GetSubordinateInsuranceUnits(Priority current);
    Task<List<InsuranceUnit>> GetSuperiorInsuranceUnits(Priority current, Priority @new);
    Task<long> GetNextPriority();
    bool IsCreatedByCore(InsuranceUnit insuranceUnit);
}
