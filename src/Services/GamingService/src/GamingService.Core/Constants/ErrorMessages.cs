namespace GamingService.Core.Constants;

public static class ErrorMessages
{
    public const string BetValuesCannotBeNullOrEmpty = "Bet values cannot be null or empty";
    public const string BetTypeCannotBeNull = "Bet type cannot be null";
    public const string BetValuesValidRange = "Bet values must be between 0 and 36 or '00'";
    public const string BetValuesMustHaveExactlyOneValue = "{0} must have exactly 1 value";
    public const string BetValuesMustHaveExactlyTwoValues = "{0} must have exactly 2 values.";
    public const string BetValuesMustHaveExactlyThreeValues = "{0} must have exactly 3 values.";
    public const string BetValuesMustHaveExactlyFourValues = "{0} must have exactly 4 values.";
    public const string BetValuesMustHaveExactlyFiveValues = "{0} must have exactly 5 values.";
    public const string BetValuesMustHaveExactlySixValues = "{0} must have exactly 6 values.";
    public const string BetValuesMustHaveExactlyTwelveValues = "{0} bets must have exactly 12 values.";
    public const string BetValuesMustHaveExactlyEighteenValues = "{0} bets must have exactly 18 values.";
}
