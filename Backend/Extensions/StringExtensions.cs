namespace Backend.Extensions;

public static class StringExtensions
{
    public static T ParseEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public static string Normalize(string value)
    {
        return value.ToLower();
    }
}