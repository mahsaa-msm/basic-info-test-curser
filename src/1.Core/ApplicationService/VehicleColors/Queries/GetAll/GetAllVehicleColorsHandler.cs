using Vehicle.Insurance.Core.Contracts.VehicleColors.Queries;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Queries.GetAll;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleColors.Queries.GetAll;

public class GetAllVehicleColorsHandler : QueryHandler<GetAllVehicleColorQuery, List<VehicleColorSelectItemQr>>
{
    private readonly IVehicleColorQueryRepository _vehicleColorQueryRepository;

    public GetAllVehicleColorsHandler(ZaminServices zaminServices,
                                  IVehicleColorQueryRepository vehicleColorQueryRepository)
        : base(zaminServices)
    {
        _vehicleColorQueryRepository = vehicleColorQueryRepository;
    }

    public override async Task<QueryResult<List<VehicleColorSelectItemQr>>> Handle(GetAllVehicleColorQuery query)
        => Result(await _vehicleColorQueryRepository.Execute(query));
}


