using Vehicle.Insurance.Core.Contracts.ServiceFeatures.Queries;
using Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Queries.CommonResults;
using Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Queries.GetAllPagedFilter;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.ServiceFeatures.Queries.GetAllPagedFilter;

public sealed class GetAllServiceFeaturesPagedFilterHandler : QueryHandler<GetAllServiceFeaturesPagedFilterQuery, PagedData<ServiceFeatureQr>>
{
    private readonly IServiceFeatureQueryRepository _serviceFeatureQueryRepository;

    public GetAllServiceFeaturesPagedFilterHandler(ZaminServices zaminServices,
                                                   IServiceFeatureQueryRepository serviceFeatureQueryRepository)
        : base(zaminServices)
    {
        _serviceFeatureQueryRepository = serviceFeatureQueryRepository;
    }

    public override async Task<QueryResult<PagedData<ServiceFeatureQr>>> Handle(GetAllServiceFeaturesPagedFilterQuery query)
        => Result(await _serviceFeatureQueryRepository.Execute(query));
}
