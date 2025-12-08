using Master.Data.Core.RequestResponse.PatternCatalogs.Queries.CommonResults;
using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.PatternCatalogs.Queries.GetAllPagedFilter;

public sealed class GetAllPatternCatalogsPagedFilterQuery : PageQuery<PagedData<PatternCatalogQr>>, IWebRequest
{
    public string? Key { get; set; }
    public bool? IsActive { get; set; }
    public long? Priority { get; set; }

    public string Path => "/Api/PatternCatalog/GetAllPatternCatalogsPagedFilter";
}