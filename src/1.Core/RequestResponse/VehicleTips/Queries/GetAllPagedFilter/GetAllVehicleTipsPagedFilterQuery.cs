using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleTips.Queries.GetAllPagedFilter;

public sealed class GetAllVehicleTipsPagedFilterQuery : PageQuery<PagedData<VehicleTipListItemQr>>, IWebRequest
{
    public string? CoreId { get; set; }
    public string? Title { get; set; }
    public string? DisplayTitle { get; set; }
    public string? BrandCoreId { get; set; }
    public string? VehicleTypeCoreId { get; set; }
    public string? VehicleSystemCoreId { get; set; }
    public bool? IsActive { get; set; }
    public long? Priority { get; set; }

    public string Path => "/Api/VehicleTip/GetAllVehicleTipsPagedFilter";
}
