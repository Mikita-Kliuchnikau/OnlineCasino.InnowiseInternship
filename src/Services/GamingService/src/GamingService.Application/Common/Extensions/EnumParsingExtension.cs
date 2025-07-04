namespace GamingService.Application.Common.Extensions;

public static class EnumParsingExtension
{
    public static TEnum ParseEnum<TEnum>(this string value, TEnum defaultValue = default) where TEnum : struct, Enum
    {
        return Enum.TryParse(value, true, out TEnum result) ? result : defaultValue;
    }
}
