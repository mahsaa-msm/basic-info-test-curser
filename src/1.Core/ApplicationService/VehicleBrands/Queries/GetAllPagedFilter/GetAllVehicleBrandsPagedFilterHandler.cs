using Vehicle.Insurance.Core.Contracts.VehicleBrands.Queries;
using Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Queries.GetAllPagedFilter;
using Zamin.Core.ApplicationServices.Queries;
using Zamin.Core.RequestResponse.Queries;
using Zamin.Utilities;

namespace Vehicle.Insurance.Core.ApplicationService.VehicleBrands.Queries.GetAllPagedFilter;

public sealed class GetAllVehicleBrandsPagedFilterHandler
    : QueryHandler<GetAllVehicleBrandsPagedFilterQuery, PagedData<VehicleBrandListItemQr>>
{
    private readonly IVehicleBrandQueryRepository _queryRepository;

    public GetAllVehicleBrandsPagedFilterHandler(ZaminServices zaminServices,
        IVehicleBrandQueryRepository queryRepository) : base(zaminServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<PagedData<VehicleBrandListItemQr>>> Handle(
        GetAllVehicleBrandsPagedFilterQuery query)
        => Result(await _queryRepository.Execute(query));
}
