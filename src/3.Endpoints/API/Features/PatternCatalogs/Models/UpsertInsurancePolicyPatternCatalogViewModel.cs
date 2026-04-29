using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Commands.Upsert;
using Vehicle.Insurance.Endpoints.API.Features.PatternCatalogs.Utils;
using static Vehicle.Insurance.Core.Resources.ProjectConsts;

namespace Vehicle.Insurance.Endpoints.API.Features.PatternCatalogs.Models;

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

