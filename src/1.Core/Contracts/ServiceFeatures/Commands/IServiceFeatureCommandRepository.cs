using Master.Data.Core.Domain.ServiceFeatures.Entities;
using Zamin.Core.Contracts.Data.Commands;

namespace Master.Data.Core.Contracts.ServiceFeatures.Commands;

public interface IServiceFeatureCommandRepository : ICommandRepository<ServiceFeature, long>
{
    Task<List<ServiceFeature>> GetByIds(List<long> serviceFeatureIds);
    Task<List<ServiceFeature>> GetAllAsync();
}