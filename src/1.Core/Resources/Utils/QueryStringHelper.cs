using System.Web;

namespace Master.Data.Core.Resources.Utils;

public static class QueryStringHelper
{
    public static string GetQueryString<T>(T obj)
    {
        var properties = from p in typeof(T).GetProperties()
                         where p.GetValue(obj, null) != null
                         select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

        return string.Join("&", properties.ToArray());
    }
}
