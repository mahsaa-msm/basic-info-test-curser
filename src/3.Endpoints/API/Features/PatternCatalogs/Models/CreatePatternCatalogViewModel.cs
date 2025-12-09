using Master.Data.Core.RequestResponse.PatternCatalogs.Commands.Create;
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
        set => _pattern = NormalizeRegex(value);
    }
    public string? EncodedDescription
    {
        get => _description;
        set => _description = NormalizeRegex(value);
    }
    public long Priority { get; set; }

    public CreatePatternCatalogCommand ToCommand() => new CreatePatternCatalogCommand
    {
        Key = Key,
        Pattern = EncodedPattern,
        Description = EncodedDescription,
        Priority = Priority
    };

    private static string? NormalizeRegex(string? pattern)
    {
        if (string.IsNullOrEmpty(pattern))
            return pattern;

        try
        {
            pattern = HttpUtility.UrlDecode(pattern);
            //// ابتدا بررسی می‌کنیم آیا pattern از JSON آمده (دارای escape مضاعف)
            //// بک‌اسلش‌های معتبر regex را حفظ می‌کنیم
            //pattern = pattern
            //    .Replace(@"\\d", @"\d")    // بازگردانی \d
            //    .Replace(@"\\x2d", @"-")   // x2d یعنی خط تیره (-)
            //    .Replace(@"\\+", @"+")     // بازگردانی +
            //    .Replace(@"\\{", @"{")     // بازگردانی {
            //    .Replace(@"\\}", @"}")     // بازگردانی }
            //    .Replace(@"\\[", @"[")     // بازگردانی [
            //    .Replace(@"\\]", @"]")     // بازگردانی ]
            //    .Replace(@"\\(", @"(")     // بازگردانی (
            //    .Replace(@"\\)", @")")     // بازگردانی )
            //    .Replace(@"\\|", @"|")     // بازگردانی |
            //    .Replace(@"\\^", @"^")     // بازگردانی ^
            //    .Replace(@"\\$", @"$")     // بازگردانی $
            //    .Replace(@"\\.", @".")     // بازگردانی .
            //    .Replace(@"\\*", @"*");    // بازگردانی *


            return Regex.Unescape(pattern);
        }
        catch
        {
            return pattern;
        }
    }
}