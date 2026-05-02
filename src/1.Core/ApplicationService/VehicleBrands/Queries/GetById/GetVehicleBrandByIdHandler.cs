using Vehicle.Insurance.Core.Contracts.VehicleBrands.Queries;
using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Queries.GetById;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleBrands.Queries.GetById;

public sealed class GetVehicleBrandByIdHandler : QueryHandler<GetVehicleBrandByIdQuery, VehicleBrandQr?>
{
    private readonly IVehicleBrandQueryRepository _queryRepository;

    public GetVehicleBrandByIdHandler(ZaminServices zaminServices,
        IVehicleBrandQueryRepository queryRepository) : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<VehicleBrandQr?>> Handle(GetVehicleBrandByIdQuery query)
        => Result(await _queryRepository.Execute(query));
}
