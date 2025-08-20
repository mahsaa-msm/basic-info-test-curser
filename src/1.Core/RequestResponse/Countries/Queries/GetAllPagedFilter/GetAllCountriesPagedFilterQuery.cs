using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.Countries.Queries.GetAllPagedFilter;

public sealed class GetAllCountriesPagedFilterQuery : PageQuery<PagedData<CountryListItemQr>>, IWebRequest
{
    public string? CoreId { get; set; }
    public string? Title { get; set; }
    public string? DisplayTitle { get; set; }
    public string? Code { get; set; }
    public bool? IsActive { get; set; }
    public int? Priority { get; set; }

    public string Path => "/Api/Country/GetAllCountriesPagedFilter";
}