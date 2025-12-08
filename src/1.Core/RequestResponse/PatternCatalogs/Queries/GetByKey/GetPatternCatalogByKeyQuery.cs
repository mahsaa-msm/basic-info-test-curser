using Master.Data.Core.RequestResponse.PatternCatalogs.Queries.CommonResults;
using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Master.Data.Core.RequestResponse.PatternCatalogs.Queries.GetByKey;

public sealed class GetPatternCatalogByKeyQuery : IQuery<PatternCatalogQr?>, IWebRequest
{
    public string PatternCatalogKey { get; set; }

    public string Path => "/Api/PatternCatalog/GetPatternCatalogByKey";
}