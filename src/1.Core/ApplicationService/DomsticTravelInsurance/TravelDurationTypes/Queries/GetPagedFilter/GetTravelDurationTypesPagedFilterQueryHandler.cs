using Master.Data.Core.Contracts.DomsticTravelInsurance.TravelDurationTypes.Repositories;
using Master.Data.Core.Contracts.TravelDurationTypess.Queries.CommonResult;
using Master.Data.Core.Contracts.TravelDurationTypess.Queries.GetPagedFilter;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.CommonResult;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.GetPagedFilter;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.TravelPassengerCountTypes.Queries.GetPagedFilter;

public sealed class GetTravelDurationTypesPagedFilterQueryHandler :
    QueryHandler<GetTravelDurationTypesPagedFilterQuery, PagedData<TravelDurationTypesQr>>
{
    private readonly ITravelDurationTypesQueryRepository _TravelDurationTypesQueryRepository;

    public GetTravelDurationTypesPagedFilterQueryHandler(ZaminServices zaminServices,
        ITravelDurationTypesQueryRepository TravelDurationTypesQueryRepository) : base(zaminServices)
    {
        _TravelDurationTypesQueryRepository = TravelDurationTypesQueryRepository;
    }

    public override async Task<QueryResult<PagedData<TravelDurationTypesQr>>> Handle(GetTravelDurationTypesPagedFilterQuery query)
        => Result(await _TravelDurationTypesQueryRepository.ExecuteAsync(query));
}
