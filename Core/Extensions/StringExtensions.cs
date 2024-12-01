using System.Collections.Specialized;
using System.Web;

namespace Core.Extensions;

public static class StringExtensions
{
    public static string NullableSubstringForDescription(this string? value, int startIndex, int length)
    {
        return value?.Substring(startIndex, Math.Min(value.Length, length)) ?? "";
    }

    public static string InsertToEnd(this string? value, string valueToInsert)
    {
        return value + valueToInsert;
    }

    public static string? NullableSubstring(this string? value, int startIndex)
    {
        return value?.Substring(startIndex);
    }
    public static string ToQueryString(this string value)
    {
        return HttpUtility.UrlEncode(value);
    }
}
