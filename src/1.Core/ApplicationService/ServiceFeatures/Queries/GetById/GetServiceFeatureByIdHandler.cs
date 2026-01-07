using Master.Data.Core.Contracts.ServiceFeatures.Queries;
using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.CommonResults;
using Master.Data.Core.RequestResponse.ServiceFeatures.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.ServiceFeatures.Queries.GetById;

public sealed class GetServiceFeatureByIdHandler : QueryHandler<GetServiceFeatureByIdQuery, ServiceFeatureQr?>
{
    private readonly IServiceFeatureQueryRepository _serviceFeatureQueryRepository;

    public GetServiceFeatureByIdHandler(ZaminServices zaminServices,
                                        IServiceFeatureQueryRepository serviceFeatureQueryRepository)
        : base(zaminServices)
    {
        _serviceFeatureQueryRepository = serviceFeatureQueryRepository;
    }

    public override async Task<QueryResult<ServiceFeatureQr?>> Handle(GetServiceFeatureByIdQuery query)
        => Result(await _serviceFeatureQueryRepository.Execute(query));
}