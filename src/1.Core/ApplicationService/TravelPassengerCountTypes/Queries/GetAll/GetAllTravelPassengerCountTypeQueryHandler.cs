using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.CommonResult;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.GetAll;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.TravelPassengerCountTypes.Queries.GetAll;

public sealed class GetAllTravelPassengerCountTypeQueryHandler : QueryHandler<GetAllTravelPassengerCountTypeQuery, List<TravelPassengerCountTypeItemQr>>
{
    private readonly ITravelPassengerCountTypeQueryRepository _travelPassengerCountTypeRepository;
    public GetAllTravelPassengerCountTypeQueryHandler(ZaminServices zaminServices,
        ITravelPassengerCountTypeQueryRepository travelPassengerCountTypeRepository) : base(zaminServices)
    {
        _travelPassengerCountTypeRepository = travelPassengerCountTypeRepository;
    }

    public override async Task<QueryResult<List<TravelPassengerCountTypeItemQr>>> Handle(GetAllTravelPassengerCountTypeQuery query)
        => Result(await _travelPassengerCountTypeRepository.ExecuteAsync(query));

}
