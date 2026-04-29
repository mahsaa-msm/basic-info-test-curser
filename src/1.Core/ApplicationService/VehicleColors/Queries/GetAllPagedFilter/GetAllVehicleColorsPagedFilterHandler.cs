using Vehicle.Insurance.Core.Contracts.VehicleColors.Queries;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Queries.GetAllPagedFilter;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleColors.Queries.GetAllPagedFilter;

public class GetAllVehicleColorsPagedFilterHandler : QueryHandler<GetAllVehicleColorsPagedFilterQuery, PagedData<VehicleColorListItemQr>>
{
    private readonly IVehicleColorQueryRepository _queryRepository;

    public GetAllVehicleColorsPagedFilterHandler(ZaminServices zaminServices,
                                             IVehicleColorQueryRepository queryRepository)
        : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<PagedData<VehicleColorListItemQr>>> Handle(GetAllVehicleColorsPagedFilterQuery query)
        => Result(await _queryRepository.Execute(query));
}

