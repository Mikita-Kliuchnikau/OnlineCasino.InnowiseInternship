namespace GamingService.Core.Common;

public record Money(Currency Currency, Amount Amount);

public sealed class Amount(decimal value)
{
    public decimal Value { get; set; } = Math.Max(0, value);
}

public enum Currency
{
    InvalidCurrency = 0,
    USD = 1,
    EUR = 2
}