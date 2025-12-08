using Master.Data.Core.Domain.PatternCatalogs.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.PatternCatalogs.Commands.Update;

public sealed class UpdatePatternCatalogCommand : ICommand, IWebRequest
{
    public long PatternCatalogId { get; set; }
    public string Pattern { get; set; } = default!;
    public string? Description { get; set; }
    public long Priority { get; set; }

    public UpdatePatternCatalogParameter ToUpdateParameter()
        => new UpdatePatternCatalogParameter(Pattern,
                                             Priority,
                                             Description);

    public string Path => "/Api/PatternCatalog/UpdatePatternCatalog";
}
