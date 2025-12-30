using Master.Data.Core.Contracts.DomsticTravelInsurance.TravelDurationTypes.Repositories;
using Master.Data.Core.Contracts.TravelDurationTypess.Queries;
using Master.Data.Core.Contracts.TravelDurationTypess.Queries.CommonResult;
using Master.Data.Core.Contracts.TravelDurationTypess.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Master.Data.Core.ApplicationService.TravelDurationTypess.Queries.GetById;

public sealed class GetTravelDurationTypesByIdQueryHandler : QueryHandler<GetTravelDurationTypesByIdQuery, TravelDurationTypesQr>
{
    private readonly ITravelDurationTypesQueryRepository _TravelDurationTypesQueryRepository;

    public GetTravelDurationTypesByIdQueryHandler(ZaminServices zaminServices,
        ITravelDurationTypesQueryRepository TravelDurationTypesQueryRepository) : base(zaminServices)
    {
        _TravelDurationTypesQueryRepository = TravelDurationTypesQueryRepository;
    }

    public async override Task<QueryResult<TravelDurationTypesQr?>> Handle(GetTravelDurationTypesByIdQuery query) =>
        Result(await _TravelDurationTypesQueryRepository.ExecuteAsync(query));

}
