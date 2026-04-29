using Vehicle.Insurance.Core.RequestResponse.PatternCatalogs.Commands.Update;
using Vehicle.Insurance.Endpoints.API.Features.PatternCatalogs.Utils;
using System.Text.RegularExpressions;
using System.Web;

namespace Vehicle.Insurance.Endpoints.API.Features.PatternCatalogs.Models;

public sealed class UpdatePatternCatalogViewModel
{
    private string _pattern;
    private string? _description;

    public long PatternCatalogId { get; set; }
    public string EncodedPattern
    {
        get => _pattern;
        set => _pattern = RegExExtensions.NormalizeRegex(value);
    }
    public string? EncodedDescription
    {
        get => _description;
        set => _description = RegExExtensions.NormalizeRegex(value);
    }
    public long Priority { get; set; }

    public UpdatePatternCatalogCommand ToCommand() => new UpdatePatternCatalogCommand
    {
        PatternCatalogId = PatternCatalogId,
        Pattern = EncodedPattern,
        Description = EncodedDescription,
        Priority = Priority
    };
}

