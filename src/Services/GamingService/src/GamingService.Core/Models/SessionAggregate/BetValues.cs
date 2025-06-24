using static GamingService.Core.Constants.ErrorMessages;

namespace GamingService.Core.Models.SessionAggregate;

public record BetValues
{
    private readonly List<string>? _keys = [];
    private readonly List<string>? _errors = [];

    public BetValues(IEnumerable<string> betValues, RouletteBetType betType)
    {
        if (betValues == null || !betValues.Any())
        {
            _errors.Add(BetValuesCannotBeNullOrEmpty);
        }
        else
        {
            if (betType == null)
            {
                _errors.Add(BetTypeCannotBeNull);
            }
            if (betValues.Any(key => int.TryParse(key, out int result) && (result < 0 || result > 36) && key != "00"))
            {
                _errors.Add(BetValuesValidRange);
            }
            if (betType is RouletteBetType.StraightUpBet && betValues.Count() != 1)
            {
                _errors.Add(string.Format(BetValuesMustHaveExactlyOneValue, betType.Name));
            }
            if (betType is RouletteBetType.SplitBet && betValues.Count() != 2)
            {
                _errors.Add(string.Format(BetValuesMustHaveExactlyTwoValues, betType.Name));
            }
            if (betType is RouletteBetType.StreetBet && betValues.Count() != 3)
            {
                _errors.Add(string.Format(BetValuesMustHaveExactlyThreeValues, betType.Name));
            }
            if (betType is RouletteBetType.CornerBet && betValues.Count() != 4)
            {
                _errors.Add(string.Format(BetValuesMustHaveExactlyFourValues, betType.Name));
            }
            if (betType is RouletteBetType.BasketBet && betValues.Count() != 5)
            {
                _errors.Add(string.Format(BetValuesMustHaveExactlyFiveValues, betType.Name));
            }
            if (betType is RouletteBetType.LineBet && betValues.Count() != 6)
            {
                _errors.Add(string.Format(BetValuesMustHaveExactlySixValues, betType.Name));
            }
            if (betType is RouletteBetType.ColumnBet ||
                betType is RouletteBetType.DozenBet &&
                betValues.Count() != 12)
            {
                _errors.Add(string.Format(BetValuesMustHaveExactlyTwelveValues, betType.Name));
            }
            if (betType is RouletteBetType.RedBet ||
                betType is RouletteBetType.BlackBet ||
                betType is RouletteBetType.EvenBet ||
                betType is RouletteBetType.OddBet ||
                betType is RouletteBetType.LowBet ||
                betType is RouletteBetType.HighBet &&
                betValues.Count() != 18)
            {
                _errors.Add(string.Format(BetValuesMustHaveExactlyEighteenValues, betType.Name));
            }
            _keys.AddRange(betValues);
        }
    }
    public IReadOnlyList<string>? Keys => _keys;
    public IReadOnlyList<string>? Errors => _errors;
}
