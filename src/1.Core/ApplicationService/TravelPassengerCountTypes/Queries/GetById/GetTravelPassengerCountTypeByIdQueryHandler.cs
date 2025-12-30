using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.CommonResult;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.TravelPassengerCountTypes.Queries.GetById;

public sealed class GetTravelPassengerCountTypeByIdQueryHandler : QueryHandler<GetTravelPassengerCountTypeByIdQuery, TravelPassengerCountTypeQr>
{
    private readonly ITravelPassengerCountTypeQueryRepository _travelPassengerCountTypeQueryRepository;

    public GetTravelPassengerCountTypeByIdQueryHandler(ZaminServices zaminServices,
        ITravelPassengerCountTypeQueryRepository travelPassengerCountTypeQueryRepository) : base(zaminServices)
    {
        _travelPassengerCountTypeQueryRepository = travelPassengerCountTypeQueryRepository;
    }

    public async override Task<QueryResult<TravelPassengerCountTypeQr?>> Handle(GetTravelPassengerCountTypeByIdQuery query) =>
        Result(await _travelPassengerCountTypeQueryRepository.ExecuteAsync(query));

}
