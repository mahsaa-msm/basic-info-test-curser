using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.Provinces.Queries.GetAllPagedFilter;

public sealed class GetAllProvincesPagedFilterQuery : PageQuery<PagedData<ProvinceListItemQr>>, IWebRequest
{
    public string? CoreId { get; set; }
    public string? Title { get; set; }
    public string? DisplayTitle { get; set; }
    public string? Code { get; set; }
    public bool? IsActive { get; set; }
    public long? Priority { get; set; }
    public string? CountryCoreId { get; set; }

    public string Path => "/Api/Province/GetAllProvincesPagedFilter";
}