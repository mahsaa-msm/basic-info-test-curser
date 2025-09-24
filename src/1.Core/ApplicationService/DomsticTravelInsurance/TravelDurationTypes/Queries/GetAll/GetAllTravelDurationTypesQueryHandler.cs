using Master.Data.Core.Contracts.DomsticTravelInsurance.TravelDurationTypes.Repositories;
using Master.Data.Core.Contracts.TravelDurationTypess.Queries.CommonResult;
using Master.Data.Core.Contracts.TravelDurationTypess.Queries.GetAll;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.CommonResult;
using Master.Data.Core.Contracts.TravelPassengerCountTypes.Queries.GetAll;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.TravelPassengerCountTypes.Queries.GetAll;

public sealed class GetAllTravelDurationTypesQueryHandler : QueryHandler<GetAllTravelDurationTypesQuery, List<TravelDurationTypesItemQr>>
{
    private readonly ITravelDurationTypesQueryRepository _TravelDurationTypesRepository;
    public GetAllTravelDurationTypesQueryHandler(ZaminServices zaminServices,
        ITravelDurationTypesQueryRepository TravelDurationTypesRepository) : base(zaminServices)
    {
        _TravelDurationTypesRepository = TravelDurationTypesRepository;
    }

    public override async Task<QueryResult<List<TravelDurationTypesItemQr>>> Handle(GetAllTravelDurationTypesQuery query)
        => Result(await _TravelDurationTypesRepository.ExecuteAsync(query));

}
