using Vehicle.Insurance.Core.Resources;
using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.VehicleColors.Queries.GetAllPagedFilter;

public sealed class GetAllVehicleColorsPagedFilterQuery : PageQuery<PagedData<VehicleColorListItemQr>>, IWebRequest
{
    public string? CoreId { get; set; }
    public string? Title { get; set; }
    public string? DisplayTitle { get; set; }
    public string? ColorHash { get; set; }
    public bool? IsActive { get; set; }
    public long? Priority { get; set; }

    public string Path => "/Api/VehicleColor/GetAllVehicleColorsPagedFilter";
}



