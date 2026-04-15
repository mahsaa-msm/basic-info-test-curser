using Master.Data.Core.Domain.ServiceFeatures.Entities;
using Master.Data.Core.Resources;
using Zamin.Core.Contracts.Data.Commands;

namespace Master.Data.Core.Contracts.ServiceFeatures.Commands;

public interface IServiceFeatureCommandRepository : ICommandRepository<ServiceFeature, long>
{
    Task<List<ServiceFeature>> GetByIds(List<long> serviceFeatureIds);
    Task<List<ServiceFeature>> GetByKeyAsync(ServiceFeatureCategory key);
    Task<List<ServiceFeature>> GetAllAsync();
}