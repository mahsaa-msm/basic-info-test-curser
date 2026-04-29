using Vehicle.Insurance.Core.Domain.ServiceFeatures.Entities;
using Vehicle.Insurance.Core.Resources;
using Zamin.Core.Contracts.Data.Commands;

namespace Vehicle.Insurance.Core.Contracts.ServiceFeatures.Commands;

public interface IServiceFeatureCommandRepository : ICommandRepository<ServiceFeature, long>
{
    Task<List<ServiceFeature>> GetByIds(List<long> serviceFeatureIds);
    Task<List<ServiceFeature>> GetByKeyAsync(ServiceFeatureCategory key);
    Task<List<ServiceFeature>> GetAllAsync();
}
