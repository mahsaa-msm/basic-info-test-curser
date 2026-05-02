using Vehicle.Insurance.Core.Contracts.VehicleTips.Queries;
using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Queries.GetAllPagedFilter;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleTips.Queries.GetAllPagedFilter;

public sealed class GetAllVehicleTipsPagedFilterHandler
    : QueryHandler<GetAllVehicleTipsPagedFilterQuery, PagedData<VehicleTipListItemQr>>
{
    private readonly IVehicleTipQueryRepository _queryRepository;

    public GetAllVehicleTipsPagedFilterHandler(ZaminServices zaminServices,
        IVehicleTipQueryRepository queryRepository) : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<PagedData<VehicleTipListItemQr>>> Handle(GetAllVehicleTipsPagedFilterQuery query)
        => Result(await _queryRepository.Execute(query));
}
