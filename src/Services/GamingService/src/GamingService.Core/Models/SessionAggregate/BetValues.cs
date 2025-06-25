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
        if (betType == null)
        {
            _errors.Add(BetTypeCannotBeNull); 
        }
        if (_errors.Count == 0)
        {
            if (betValues!.Any(key => int.TryParse(key, out int result) && (result < 0 || result > 36) && key != "00"))
            {
                _errors.Add(BetValuesValidRange);
            }
            if (betType!.NumberOfValues != betValues!.Count())
            {
                _errors.Add(string.Format(BetValuesDoesNotMatchTheType, betType.Name, betType.NumberOfValues));
            }
            _keys.AddRange(betValues!);
        }
    }
    public IReadOnlyList<string>? Keys => _keys;
    public IReadOnlyList<string>? Errors => _errors;
}
