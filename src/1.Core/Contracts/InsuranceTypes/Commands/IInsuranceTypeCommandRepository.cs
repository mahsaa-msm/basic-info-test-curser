using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.InsuranceTypes.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace Vehicle.Insurance.Core.Contracts.InsuranceTypes.Commands;

public interface IInsuranceTypeCommandRepository : ICommandRepository<InsuranceType, long>
{
    Task<InsuranceType?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId);
    Task<List<InsuranceType>> GetByIds(List<long> contriesIds);
    Task<List<InsuranceType>> GetByTenantId(long tenantId);
    Task<List<InsuranceType>> GetAllAsync();
    Task<List<InsuranceType>> GetSubordinateInsuranceTypes(Priority current, Priority @new);
    Task<List<InsuranceType>> GetSubordinateInsuranceTypes(Priority current);
    Task<List<InsuranceType>> GetSuperiorInsuranceTypes(Priority current, Priority @new);
    Task<long> GetNextPriority();
    bool IsCreatedByCore(InsuranceType insuranceType);
}
