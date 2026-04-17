using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.CommonResults;
using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.GetAll;
using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.GetAllByKey;
using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.GetAllPagedFilter;
using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.GetById;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.Contracts.ServiceFeatures.Queries;

public interface IServiceFeatureQueryRepository : IQueryRepository
{
    Task<ServiceFeatureQr?> Execute(GetServiceFeatureByIdQuery query);
    Task<List<ServiceFeatureQr>> Execute(GetAllServiceFeaturesQuery query);
    Task<PagedData<ServiceFeatureQr?>> Execute(GetAllServiceFeaturesPagedFilterQuery query);
    Task<GetAllServiceFeaturesByKeyQr?> Execute(GetAllServiceFeaturesByKeyQuery query);
}
