using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.LicensePlateTypes.Queries.GetAllPagedFilter;
public sealed class GetAllLicensePlateTypesPagedFilterQuery : PageQuery<PagedData<LicensePlateTypeListItemQr>>, IWebRequest
{
    public string? CoreId { get; set; }
    public string? Title { get; set; }
    public string? DisplayTitle { get; set; }
    public bool? IsActive { get; set; }
    public long? Priority { get; set; }

    public string Path => "/Api/LicensePlateType/GetAllLicensePlateTypesPagedFilter";
}
