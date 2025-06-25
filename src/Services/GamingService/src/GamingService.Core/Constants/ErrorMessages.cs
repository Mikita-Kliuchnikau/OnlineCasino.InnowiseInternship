namespace GamingService.Core.Constants;

public static class ErrorMessages
{
    public const string BetValuesCannotBeNullOrEmpty = "Bet values cannot be null or empty";
    public const string BetTypeCannotBeNull = "Bet type cannot be null";
    public const string BetValuesValidRange = "Bet values must be between 0 and 36 or '00'";
    public const string BetValuesDoesNotMatchTheType = "{0} must have exactly {1} value(s)";
}
