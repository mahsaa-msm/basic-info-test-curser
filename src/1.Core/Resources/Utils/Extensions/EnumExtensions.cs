using System.ComponentModel;

namespace Master.Data.Core.Resources.Utils.Extensions;

public static class EnumExtensions
{
    public static string GetEnumDescription(object? value)
    {
        if (value is null) return string.Empty;
        var field = value.GetType().GetField(value.ToString());
        var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))!;
        return attribute == null ? value.ToString() : attribute.Description;
    }
}
