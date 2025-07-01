using GamingService.Core.Common;
using GamingService.Core.Models.RoulettePlayerAggregate;
using static GamingService.Core.Constants.ErrorMessages;

namespace GamingService.Core.Models.SessionAggregate;

public class RouletteBet
{
    private RouletteBet(
        RoulettePlayer player,
        Money betAmount,
        BetValues betValues,
        RouletteBetType betType)
    {
        Player = player;
        BetAmount = betAmount;
        BetType = betType;
        BetValues = betValues;
    }

    private static readonly Dictionary<BetStatus, HashSet<BetStatus>> _statusTransitions = new()
    {
        { BetStatus.Pending, new() { BetStatus.Won, BetStatus.Lost, BetStatus.Cancelled } },
        { BetStatus.Won, new() { BetStatus.Cancelled } },
        { BetStatus.Lost, new() { BetStatus.Cancelled } },
        { BetStatus.Cancelled, new() { } }
    };

    public RoulettePlayer Player { get; private init; } 
    
    public Money BetAmount { get; private init; }

    public RouletteBetType BetType { get; private init; }

    public BetValues BetValues { get; private init; }

    public BetStatus Status { get; private set; } = BetStatus.Pending;

    public IReadOnlyList<string>? Errors => BetValues.Errors;

    public static RouletteBet Create(
        RoulettePlayer player,
        Money betAmount,
        IEnumerable<string> keys,
        RouletteBetType betType)
    {
        var betValues = new BetValues(keys, betType);
        return new RouletteBet(player, betAmount, betValues, betType);
    }

    public static IEnumerable<RouletteBet> Create(IEnumerable<(
        RoulettePlayer player, 
        Money betAmount, 
        IEnumerable<string> values, 
        RouletteBetType betType)> bets)
    {
        foreach (var (player, betAmount, values, betType) in bets)
        {
            var betValues = new BetValues(values, betType);
            yield return new RouletteBet(player, betAmount, betValues, betType);
        }
    }

    public void AddErrors(string? error)
    {
        BetValues.AddErrors(error);
    }

    public void AddErrors(IEnumerable<string>? errors)
    {
        BetValues.AddErrors(errors);
    }

    public void ChangeStatus(BetStatus newStatus)
    {
        if (_statusTransitions.TryGetValue(Status, out var allowedStatuses)
            && allowedStatuses.Contains(newStatus))
        {
            Status = newStatus;
        }
        else
        {
            throw new ArgumentException(string.Format(BetStatusCannotBeChanged, Status.ToString(), newStatus.ToString()));
        }
    }
}
