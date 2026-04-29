using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.CommonResults;
using Zamin.Core.RequestResponse.Endpoints;
using Zamin.Core.RequestResponse.Queries;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Queries.GetAll;

public sealed class GetAllPatternCatalogsQuery : IQuery<List<PatternCatalogQr>>, IWebRequest
{
    public bool? IsActive { get; set; }
    public PatternCatalogType? Type { get; set; }

    public string Path => "/Api/PatternCatalog/GetAllPatternCatalogs";
}
