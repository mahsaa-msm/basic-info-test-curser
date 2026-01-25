using Master.Data.Core.RequestResponse.PatternCatalogs.Queries.CommonResults;
using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.PatternCatalogs.Queries.GetAll;

public sealed class GetAllPatternCatalogsQuery : IQuery<List<PatternCatalogQr>>, IWebRequest
{
    public bool? IsActive { get; set; }

    public string Path => "/Api/PatternCatalog/GetAllPatternCatalogs";
}