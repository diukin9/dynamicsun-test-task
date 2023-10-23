using System.ComponentModel;

namespace DynamicSunTestTask.Infrastructure.Common.Helpers;

public static class Enum
{
    public static T GetValueFromDescriptionOrDefault<T>(string description) where T : notnull, System.Enum
    {
        foreach (var field in typeof(T).GetFields())
        {
            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (attribute.Description == description) return (T)field.GetValue(null)!;
            }
            else
            {
                if (field.Name == description) return (T)field.GetValue(null)!;
            }
        }

        return default!;
    }

    public static T GetValueFromStringOrDefault<T>(string str, bool ignoreCase = true) where T : struct, System.Enum
    {
        if (string.IsNullOrEmpty(str)) return default!;
        var isSuccess = System.Enum.TryParse<T>(str.Trim(), ignoreCase, out var result);
        return isSuccess ? result : default!;
    }

    public static T[] GetValuesFromStringOrDefault<T>(string str, string separator, bool ignoreCase = true) where T : struct, System.Enum
    {
        if (string.IsNullOrEmpty(str)) return default!;
        return str.Trim().Split(separator).Select(x => GetValueFromStringOrDefault<T>(str, ignoreCase)).ToArray();
    }
}