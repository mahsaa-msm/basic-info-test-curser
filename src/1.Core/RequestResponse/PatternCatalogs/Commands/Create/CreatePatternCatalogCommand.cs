using Vehicle.Insurance.Core.Domain.PatternCatalogs.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Commands.Create;

public sealed class CreatePatternCatalogCommand : ICommand<long>, IWebRequest
{
    public string Key { get; set; } = default!;
    public string Pattern { get; set; } = default!;
    public PatternCatalogType Type { get; set; }
    public string? Description { get; set; }
    public long Priority { get; set; }

    public CreatePatternCatalogParameter ToCreateParameter(long priority)
        => new CreatePatternCatalogParameter(Key,
                                             Pattern,
                                             Type,
                                             priority,
                                             Description);

    public UpdatePatternCatalogParameter ToUpdateParameter()
    => new UpdatePatternCatalogParameter(Pattern,
                                         Type,
                                         Priority,
                                         Description);

    public string Path => "/Api/PatternCatalog/CreatePatternCatalog";
}

