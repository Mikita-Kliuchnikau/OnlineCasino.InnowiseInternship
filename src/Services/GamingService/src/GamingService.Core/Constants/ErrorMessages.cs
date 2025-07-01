namespace GamingService.Core.Constants;

public static class ErrorMessages
{
    public const string BetValuesCannotBeNullOrEmpty = "Bet values cannot be null or empty";
    public const string BetTypeCannotBeNull = "Bet type cannot be null";
    public const string BetValuesValidRange = "Bet values must be between 0 and 36 or '00'";
    public const string BetValuesDoesNotMatchTheType = "{0} must have exactly {1} value(s)";
    public const string BetCurrencyMismatch = "Bet currency does not match player's balance currency";
    public const string PlayerBalanceInsufficient = "Player's balance is insufficient to place the bet";
    public const string BetAmountExceedsMaxBet = "Bet amount exceeds the maximum allowed bet";
    public const string BetAmountBelowMinBet = "Bet amount is below the minimum allowed bet";
    public const string BasketBetNotAllowedInEuropeanRoulette = "Basket bet is not allowed in European roulette";
    public const string BetValuesCannotContain00InEuropeanRoulette = "Bet values cannot contain '00' in European roulette";
    public const string BetStatusCannotBeChanged = "Bet status cannot be changed from {0} to {1}";
}
