using Master.Data.Core.RequestResponse.PatternCatalogs.Commands.Create;
using Master.Data.Endpoints.API.Features.PatternCatalogs.Utils;
using System.Text.RegularExpressions;
using System.Web;

namespace Master.Data.Endpoints.API.Features.PatternCatalogs.Models;

public sealed class CreatePatternCatalogViewModel
{
    private string _pattern;
    private string? _description;
    public string Key { get; set; } = default!;
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

    public CreatePatternCatalogCommand ToCommand() => new CreatePatternCatalogCommand
    {
        Key = Key,
        Pattern = EncodedPattern,
        Description = EncodedDescription,
        Priority = Priority
    };
}