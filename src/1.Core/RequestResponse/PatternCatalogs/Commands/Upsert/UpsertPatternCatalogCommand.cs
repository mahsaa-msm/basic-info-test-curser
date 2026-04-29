using Vehicle.Insurance.Core.Domain.PatternCatalogs.Parameters;
using Zamin.Core.RequestResponse.Commands;
using Zamin.Core.RequestResponse.Endpoints;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Commands.Upsert;

public sealed class UpsertPatternCatalogCommand : ICommand<long?>, IWebRequest
{
    public string Key { get; set; } = default!;
    public string Pattern { get; set; } = default!;
    public PatternCatalogType Type { get; set; }
    public string? Description { get; set; }

    public CreatePatternCatalogParameter ToCreateParameter(long priority)
        => new CreatePatternCatalogParameter(Key,
                                             Pattern,
                                             Type,
                                             priority,
                                             Description);

    public UpdatePatternCatalogParameter ToUpdateParameter(long priority)
        => new UpdatePatternCatalogParameter(Pattern,
                                             Type,
                                             priority,
                                             Description);

    public string Path => "/Api/PatternCatalog/UpsertPatternCatalog";
}

