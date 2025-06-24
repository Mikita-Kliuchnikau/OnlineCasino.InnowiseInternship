using System.Reflection;

namespace GamingService.Core.Primitives;

public abstract class Enumeration<TEnum>(int value, string name) : IEquatable<TEnum>
    where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<int, TEnum> _enumerations = CreateEnumerations();

    public string Name { get; protected init; } = name;
    public int Value { get; protected init; } = value;

    public static TEnum? FromValue(int value)
    {
        return _enumerations.TryGetValue(value, out var enumeration) ? enumeration : default;
    }

    public static TEnum? FromName(string name)
    {
        return _enumerations.Values.FirstOrDefault(e => e.Name == name);
    }

    public bool Equals(TEnum? other)
    {
        if (other is null)
        {
            return false;
        }
        return GetType() == other.GetType() &&
            Value.Equals(other.Value);
    }
    public override bool Equals(object? obj) => obj is Enumeration<TEnum> other && Equals(other);
    
    public override string ToString() => Name;

    public override int GetHashCode() => Value.GetHashCode(); 

    private static Dictionary<int, TEnum> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);

        var fieldsForType = enumerationType
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly)
            .Where(fieldInfo =>
                enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (TEnum)fieldInfo.GetValue(null)!);

        return fieldsForType
            .ToDictionary(enumeration => enumeration.Value);
    }
}
