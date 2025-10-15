using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.CommonResult;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.GetPagedFilter;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.TravelPassengerCountTypes.Queries.GetPagedFilter;

public sealed class GetTravelPassengerCountTypePagedFilterQueryHandler :
    QueryHandler<GetTravelPassengerCountTypePagedFilterQuery, PagedData<TravelPassengerCountTypeQr>>
{
    private readonly ITravelPassengerCountTypeQueryRepository _travelPassengerCountTypeQueryRepository;

    public GetTravelPassengerCountTypePagedFilterQueryHandler(ZaminServices zaminServices,
        ITravelPassengerCountTypeQueryRepository travelPassengerCountTypeQueryRepository) : base(zaminServices)
    {
        _travelPassengerCountTypeQueryRepository = travelPassengerCountTypeQueryRepository;
    }

    public override async Task<QueryResult<PagedData<TravelPassengerCountTypeQr>>> Handle(GetTravelPassengerCountTypePagedFilterQuery query)
        => Result(await _travelPassengerCountTypeQueryRepository.ExecuteAsync(query));
}
