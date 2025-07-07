using GamingService.Core.Common;
using static GamingService.Core.Constants.ErrorMessages;

namespace GamingService.Core.Models.SessionAggregate;

public class RouletteBet
{
    private RouletteBet(
        string playerId,
        Money betAmount,
        BetValues betValues,
        RouletteBetType betType)
    {
        PlayerId = playerId;
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

    public string PlayerId { get; private init; } 
    
    public Money BetAmount { get; private init; }

    public Amount BetWinnings { get; private set; } = new(0);

    public RouletteBetType BetType { get; private init; }

    public BetValues BetValues { get; private init; }

    public BetStatus Status { get; private set; } = BetStatus.Pending;

    public IReadOnlyList<string>? Errors => BetValues.Errors;

    public static RouletteBet Create(
        string playerId,
        Money betAmount,
        IEnumerable<string> keys,
        RouletteBetType betType)
    {
        var betValues = new BetValues(keys, betType);
        var rouletteBet = new RouletteBet( playerId, betAmount, betValues, betType);

        if (betAmount.Currency == Currency.InvalidCurrency)
        {
            rouletteBet.AddErrors(BetCurrencyUnsupported);
        }
        if (betAmount.Amount.Value <= 0)
        {
            rouletteBet.AddErrors(BetAmountMustBeGreaterThanZero);
        }
        return rouletteBet;
    }

    public static IEnumerable<RouletteBet> Create(
        IEnumerable<(
            string playerId,
            Money betAmount,
            IEnumerable<string> values,
            RouletteBetType betType
        )> bets)
    {
        foreach (var (playerId, betAmount, values, betType) in bets)
        {
            yield return Create(playerId, betAmount, values, betType);
        }
    }

    public void AddErrors(string? error)
    {
        BetValues.AddErrors(error);
        ChangeStatus(BetStatus.Cancelled);
    }

    public void AddErrors(IEnumerable<string>? errors)
    {
        BetValues.AddErrors(errors);
        ChangeStatus(BetStatus.Cancelled);
    }

    public void ChangeStatus(BetStatus newStatus)
    {
        if (_statusTransitions.TryGetValue(Status, out var allowedStatuses)
            && allowedStatuses.Contains(newStatus))
        {
            Status = newStatus;

            if (Status == BetStatus.Won)
            {
                BetWinnings = new Amount(BetAmount.Amount.Value * BetType.WinningsMultiplier);
            }
        }
        else
        {
            Status = BetStatus.Cancelled;
        }
    }
}
