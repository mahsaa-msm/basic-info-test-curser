using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleBrands.Queries.GetAllPagedFilter;

public sealed class GetAllVehicleBrandsPagedFilterQuery : PageQuery<PagedData<VehicleBrandListItemQr>>, IWebRequest
{
    public string? CoreId { get; set; }
    public string? Title { get; set; }
    public string? DisplayTitle { get; set; }
    public bool? IsActive { get; set; }
    public long? Priority { get; set; }

    public string Path => "/Api/VehicleBrand/GetAllVehicleBrandsPagedFilter";
}
