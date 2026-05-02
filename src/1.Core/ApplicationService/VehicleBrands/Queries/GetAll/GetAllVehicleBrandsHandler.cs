using Vehicle.Insurance.Core.Contracts.VehicleBrands.Queries;
using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Queries.GetAll;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleBrands.Queries.GetAll;

public sealed class GetAllVehicleBrandsHandler : QueryHandler<GetAllVehicleBrandQuery, List<VehicleBrandSelectItemQr>>
{
    private readonly IVehicleBrandQueryRepository _queryRepository;

    public GetAllVehicleBrandsHandler(ZaminServices zaminServices,
        IVehicleBrandQueryRepository queryRepository) : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<List<VehicleBrandSelectItemQr>>> Handle(GetAllVehicleBrandQuery query)
        => Result(await _queryRepository.Execute(query));
}
