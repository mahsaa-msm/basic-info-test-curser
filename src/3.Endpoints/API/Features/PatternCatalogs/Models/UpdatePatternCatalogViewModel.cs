using Master.Data.Core.RequestResponse.PatternCatalogs.Commands.Update;
using System.Text.RegularExpressions;
using System.Web;

namespace Master.Data.Endpoints.API.Features.PatternCatalogs.Models;

public sealed class UpdatePatternCatalogViewModel
{
    private string _pattern;
    private string? _description;

    public long PatternCatalogId { get; set; }
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

    public UpdatePatternCatalogCommand ToCommand() => new UpdatePatternCatalogCommand
    {
        PatternCatalogId = PatternCatalogId,
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
