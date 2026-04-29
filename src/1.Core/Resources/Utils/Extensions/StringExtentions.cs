using System.Reflection;
using System.Text.RegularExpressions;

namespace Vehicle.Insurance.Core.Resources.Utils.Extensions;

public static class StringExtentions
{
    private static Regex _regex = new Regex("[ ]{2,}", RegexOptions.None);

    public static bool IsContainThisField<T>(this string fieldName)
    {
        T val = Activator.CreateInstance<T>();
        return val != null && val.GetType().GetProperties().Any((PropertyInfo p) => p.Name.ToLower() == fieldName.ToLower());
    }

    public static string RemoveExcessWhiteSpace(this string str)
    {
        return _regex.Replace(str, " ").Trim();
    }
}
