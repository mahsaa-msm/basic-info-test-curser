using Vehicle.Insurance.Core.Contracts.VehicleColors.Queries;
using Vehicle.Insurance.Core.RequestResponse.VehicleColors.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleColors.Queries.GetById;

public class GetVehicleColorByIdHandler : QueryHandler<GetVehicleColorByIdQuery, VehicleColorQr>
{
    private readonly IVehicleColorQueryRepository _queryRepository;

    public GetVehicleColorByIdHandler(ZaminServices zaminServices,
                                 IVehicleColorQueryRepository queryRepository)
        : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<VehicleColorQr>> Handle(GetVehicleColorByIdQuery query)
        => Result(await _queryRepository.Execute(query));
}


