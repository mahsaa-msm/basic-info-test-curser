using Vehicle.Insurance.Core.Contracts.ServiceFeatures.Queries;
using Vehicle.Insurance.Core.RequestResponse.ServiceFeatures.Queries.GetAllByKey;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.ServiceFeatures.Queries.GetAllByKey;

public sealed class GetAllServiceFeaturesByKeyHandler : QueryHandler<GetAllServiceFeaturesByKeyQuery, GetAllServiceFeaturesByKeyQr?>
{
    private readonly IServiceFeatureQueryRepository _serviceFeatureQueryRepository;

    public GetAllServiceFeaturesByKeyHandler(ZaminServices zaminServices,
                                             IServiceFeatureQueryRepository serviceFeatureQueryRepository)
        : base(zaminServices)
    {
        _serviceFeatureQueryRepository = serviceFeatureQueryRepository;
    }

    public override async Task<QueryResult<GetAllServiceFeaturesByKeyQr?>> Handle(GetAllServiceFeaturesByKeyQuery query)
        => Result(await _serviceFeatureQueryRepository.Execute(query));
}
