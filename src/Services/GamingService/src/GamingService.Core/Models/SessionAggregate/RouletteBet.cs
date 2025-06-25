using GamingService.Core.Common;
using GamingService.Core.Models.RoulettePlayerAggregate;
using System.ComponentModel;

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

    public RoulettePlayer Player { get; private init; } 
    
    public Money BetAmount { get; private init; }

    public RouletteBetType BetType { get; private init; }

    public BetValues BetValues { get; private init; }

    public BetStatus Status { get; private set; } = BetStatus.Pending;

    public static CreateBetResult Create(
        RoulettePlayer player,
        Money betAmount,
        IEnumerable<string> keys,
        RouletteBetType betType)
    {
        var betValues = new BetValues(keys, betType);
        var bet = new RouletteBet(player, betAmount, betValues, betType);
        return betValues.Errors?.Count != 0 
            ? new(bet, [.. betValues.Errors!]) 
            : new(bet, []);
    }

    public static IEnumerable<CreateBetResult> Create(IEnumerable<(
        RoulettePlayer player, 
        Money betAmount, 
        IEnumerable<string> values, 
        RouletteBetType betType)> bets)
    {
        foreach (var (player, betAmount, values, betType) in bets)
        {
            var betValues = new BetValues(values, betType);
            var bet = new RouletteBet(player, betAmount, betValues, betType);
            yield return betValues.Errors?.Count != 0
                ? new(bet, [.. betValues.Errors!])
                : new(bet, []);
        }
    }

    public void ChangeStatus(BetStatus status)
    {
        Status = status switch
        {
            BetStatus.Pending => Status,
            BetStatus.Won => Status == BetStatus.Pending ? BetStatus.Won : Status,
            BetStatus.Lost => Status == BetStatus.Pending ? BetStatus.Lost : Status,
            BetStatus.Cancelled => BetStatus.Cancelled,
            _ => throw new InvalidEnumArgumentException(
                nameof(status),
                (int)status,
                typeof(BetStatus))
        };
    }
}
