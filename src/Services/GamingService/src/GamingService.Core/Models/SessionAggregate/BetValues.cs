using static GamingService.Core.Constants.ErrorMessages;

namespace GamingService.Core.Models.SessionAggregate;

public record BetValues
{
    private readonly List<string> _keys = [];
    private readonly List<string> _errors = [];

    public BetValues(IEnumerable<string> betValues, RouletteBetType betType)
    {
        if (betValues is null || !betValues.Any())
        {
            _errors.Add(BetValuesCannotBeNullOrEmpty);
        }
        else
        {
            if (betType == RouletteBetType.Default)
            {
                _errors.Add(BetTypeNotFound);
            }
            if (betValues.AreBetValuesOutOfRange())
            {
                _errors.Add(BetValuesValidRange);
            }
            if (betValues.IsBetCountMismatch(betType))
            {
                _errors.Add(string.Format(BetValuesDoesNotMatchTheType, betType.Name, betType.NumberOfValues));
            }
            _keys.AddRange(betValues!);
        }
    }

    public IReadOnlyList<string>? Keys => _keys;
    public IReadOnlyList<string>? Errors => _errors;

    public void AddErrors(string? error)
    {
        if (string.IsNullOrWhiteSpace(error))
        {
            return;
        }
        _errors.Add(error);
    }

    public void AddErrors(IEnumerable<string>? errors)
    {
        if (errors is null || !errors.Any())
        {
            return;
        }
        _errors.AddRange(errors);
    }
}
