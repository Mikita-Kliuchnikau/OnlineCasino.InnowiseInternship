namespace GamingService.Core.Common;

public record Money(Currency Currency, Amount Amount);

public sealed class Amount(decimal value)
{
    public decimal Value { get; set; } = value < 0 ? throw new ArgumentOutOfRangeException(nameof(value)) : value;
}

public enum Currency
{
    USD = 0,
    EUR = 1
}
