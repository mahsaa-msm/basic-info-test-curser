using Vehicle.Insurance.Core.Domain.PatternCatalogs.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Commands.Update;

public sealed class UpdatePatternCatalogCommand : ICommand, IWebRequest
{
    public long PatternCatalogId { get; set; }
    public string Pattern { get; set; } = default!;
    public PatternCatalogType Type { get; set; }
    public string? Description { get; set; }
    public long Priority { get; set; }

    public UpdatePatternCatalogParameter ToUpdateParameter()
        => new UpdatePatternCatalogParameter(Pattern,
                                             Type,
                                             Priority,
                                             Description);

    public string Path => "/Api/PatternCatalog/UpdatePatternCatalog";
}

