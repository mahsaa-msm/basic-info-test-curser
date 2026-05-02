using Vehicle.Insurance.Core.Contracts.VehicleTips.Queries;
using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Queries.GetAll;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleTips.Queries.GetAll;

public sealed class GetAllVehicleTipsHandler : QueryHandler<GetAllVehicleTipQuery, List<VehicleTipSelectItemQr>>
{
    private readonly IVehicleTipQueryRepository _queryRepository;

    public GetAllVehicleTipsHandler(ZaminServices zaminServices,
        IVehicleTipQueryRepository queryRepository) : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<List<VehicleTipSelectItemQr>>> Handle(GetAllVehicleTipQuery query)
        => Result(await _queryRepository.Execute(query));
}
