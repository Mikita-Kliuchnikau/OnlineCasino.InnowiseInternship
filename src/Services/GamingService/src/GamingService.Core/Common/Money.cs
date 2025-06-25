namespace GamingService.Core.Common;

public record Money(Currency Currency, Amount Amount);

public sealed class Amount(decimal Value)
{
    public decimal Value { get; set; } = Value < 0 ? throw new ArgumentOutOfRangeException(nameof(Value)) : Value;
}

public enum Currency
{
    USD = 0,
    EUR = 1
}
