using Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Queries.CommonResults;
using Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Queries.GetAll;
using Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Queries.GetAllByKey;
using Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Queries.GetAllPagedFilter;
using Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Queries.GetById;
using Zamin.Core.Contracts.Data.Queries;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.Contracts.ServiceFeatures.Queries;

public interface IServiceFeatureQueryRepository : IQueryRepository
{
    Task<ServiceFeatureQr?> Execute(GetServiceFeatureByIdQuery query);
    Task<List<ServiceFeatureQr>> Execute(GetAllServiceFeaturesQuery query);
    Task<PagedData<ServiceFeatureQr?>> Execute(GetAllServiceFeaturesPagedFilterQuery query);
    Task<GetAllServiceFeaturesByKeyQr?> Execute(GetAllServiceFeaturesByKeyQuery query);
}

