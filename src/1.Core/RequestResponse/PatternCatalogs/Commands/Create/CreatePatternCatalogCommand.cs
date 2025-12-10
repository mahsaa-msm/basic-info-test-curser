using Master.Data.Core.Domain.PatternCatalogs.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;

namespace Master.Data.Core.RequestResponse.PatternCatalogs.Commands.Create;

public sealed class CreatePatternCatalogCommand : ICommand<long>, IWebRequest
{
    public string Key { get; set; } = default!;
    public string Pattern { get; set; } = default!;
    public string? Description { get; set; }
    public long Priority { get; set; }

    public CreatePatternCatalogParameter ToCreateParameter(long priority)
        => new CreatePatternCatalogParameter(Key,
                                             Pattern,
                                             priority,
                                             Description);

    public string Path => "/Api/PatternCatalog/CreatePatternCatalog";
}
