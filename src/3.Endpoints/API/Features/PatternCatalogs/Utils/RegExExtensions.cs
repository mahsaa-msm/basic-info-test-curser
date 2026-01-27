using System.Text.RegularExpressions;
using System.Web;

namespace Master.Data.Endpoints.API.Features.PatternCatalogs.Utils;

public static class RegExExtensions
{
    public static string? NormalizeRegex(string? pattern)
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
