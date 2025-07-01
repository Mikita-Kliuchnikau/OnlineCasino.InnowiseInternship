namespace GamingService.Core.Models.SessionAggregate;

public static class BetValuesValidator
{
    public static bool AreBetValuesOutOfRange(this IEnumerable<string> betValues)
    {
        return betValues.Any(key => int.TryParse(key, out int result)
                 && (result < 0 || result > 36)
                 && key != "00");
    }
    
    public static bool IsBetCountMismatch(this IEnumerable<string> betValues, RouletteBetType betType)
    {
        return betType.NumberOfValues != betValues.Count();
    }
}
