using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.CommonResults;
using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;

namespace Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.GetById;

public sealed class GetPatternCatalogByIdQuery : IQuery<PatternCatalogQr?>, IWebRequest
{
    public long PatternCatalogId { get; set; }

    public string Path => "/Api/PatternCatalog/GetPatternCatalogById";
}
