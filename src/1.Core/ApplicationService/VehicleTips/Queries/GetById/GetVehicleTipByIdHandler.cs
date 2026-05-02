using Vehicle.Insurance.Core.Contracts.VehicleTips.Queries;
using Vehicle.Insurance.Core.RequestResponse.VehicleTips.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleTips.Queries.GetById;

public sealed class GetVehicleTipByIdHandler : QueryHandler<GetVehicleTipByIdQuery, VehicleTipQr?>
{
    private readonly IVehicleTipQueryRepository _queryRepository;

    public GetVehicleTipByIdHandler(ZaminServices zaminServices,
        IVehicleTipQueryRepository queryRepository) : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<VehicleTipQr?>> Handle(GetVehicleTipByIdQuery query)
        => Result(await _queryRepository.Execute(query));
}
