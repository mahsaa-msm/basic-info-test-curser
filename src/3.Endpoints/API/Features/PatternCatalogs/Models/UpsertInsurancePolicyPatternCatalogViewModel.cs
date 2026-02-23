using Master.Data.Core.RequestResponse.PatternCatalogs.Commands.Upsert;
using Master.Data.Endpoints.API.Features.PatternCatalogs.Utils;
using static Master.Data.Core.Resources.ProjectConsts;

namespace Master.Data.Endpoints.API.Features.PatternCatalogs.Models;

public sealed class UpsertInsurancePolicyPatternCatalogViewModel
{
    private string _pattern;
    public string Key { get; set; } = default!;
    public string EncodedPattern
    {
        get => _pattern;
        set => _pattern = RegExExtensions.NormalizeRegex(value);
    }

    public UpsertPatternCatalogCommand ToCommand() => new UpsertPatternCatalogCommand
    {
        Key = Key,
        Pattern = EncodedPattern,
        Type = PatternCatalogType.InsurancePolicyNumber,
    };


}
