using System.Security.Cryptography;

namespace GamingService.Core.Extentions;

public static class HashAlgorithmExtentions
{
    public static string GetHashAlgorithmName(this HashAlgorithm hashAlgorithm)
    {
        var baseType = hashAlgorithm.GetType().BaseType;

        return baseType != null
            ? baseType.Name
            : hashAlgorithm.GetType().Name;
    }
}
