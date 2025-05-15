using System.Globalization;
using System.Resources;
using UsersManagementService.Common.Constants;
using UsersManagementService.Common.Enums;

namespace UsersManagementService.Common.Helpers;

public class ResourceHelper<T>(CulturePreference culture)
{
    private readonly ResourceManager _resourceManager = new(typeof(T));

    public string GetValue(string key)
    {
        return _resourceManager.GetString(key, CultureInfo.CreateSpecificCulture(GetCultureString()))!;
    }

    private string GetCultureString()
    {
        return (culture) switch
        {
            CulturePreference.Russian => CulturesInfo.RUSSIAN_CULTURE,
            _ => CulturesInfo.ENGLISH_CULTURE
        };
    }
}
