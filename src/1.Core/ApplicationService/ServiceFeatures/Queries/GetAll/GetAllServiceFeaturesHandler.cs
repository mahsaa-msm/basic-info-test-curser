using Vehicle.Insurance.Core.Contracts.ServiceFeatures.Queries;
using Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Queries.CommonResults;
using Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Queries.GetAll;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.ServiceFeatures.Queries.GetAll;

public sealed class GetAllServiceFeaturesHandler : QueryHandler<GetAllServiceFeaturesQuery, List<ServiceFeatureQr>>
{
    private readonly IServiceFeatureQueryRepository _serviceFeatureQueryRepository;

    public GetAllServiceFeaturesHandler(ZaminServices zaminServices,
                                        IServiceFeatureQueryRepository serviceFeatureQueryRepository)
        : base(zaminServices)
    {
        _serviceFeatureQueryRepository = serviceFeatureQueryRepository;
    }

    public override async Task<QueryResult<List<ServiceFeatureQr>>> Handle(GetAllServiceFeaturesQuery query)
        => Result(await _serviceFeatureQueryRepository.Execute(query));
}
