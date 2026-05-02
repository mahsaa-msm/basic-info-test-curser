using Vehicle.Insurance.Core.Domain.Common.ValueObjects;
using Vehicle.Insurance.Core.Domain.LicensePlateTypes.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace Vehicle.Insurance.Core.Contracts.LicensePlateTypes.Commands;

public interface ILicensePlateTypeCommandRepository : ICommandRepository<LicensePlateType, long>
{
    Task<LicensePlateType?> GetByCoreIdIgnoreQueryFiltersAsync(CoreId coreId);
    Task<List<LicensePlateType>> GetByIds(List<long> ids);
    Task<long> GetNextPriority();
    Task<List<LicensePlateType>> GetSubordinateLicensePlateTypes(Priority current, Priority @new);
    Task<List<LicensePlateType>> GetSubordinateLicensePlateTypes(Priority current);
    Task<List<LicensePlateType>> GetSuperiorLicensePlateTypes(Priority current, Priority @new);
    bool IsCreatedByCore(LicensePlateType entity);
}
