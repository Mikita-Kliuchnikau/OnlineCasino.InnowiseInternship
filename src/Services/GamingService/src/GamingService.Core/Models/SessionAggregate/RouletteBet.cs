using GamingService.Core.Common;
using GamingService.Core.Models.RoulettePlayerAggregate;
using GamingService.Core.Primitives;

namespace GamingService.Core.Models.SessionAggregate;

public class RouletteBet : Entity
{
    private RouletteBet(
        Guid playerId,
        Money betAmount,
        BetValues betValues,
        RouletteBetType betType) : base(Guid.NewGuid())
    {
        PlayerId = playerId;
        BetAmount = betAmount;
        BetType = betType;
        BetValues = betValues;
    }

    public Guid PlayerId { get; private init; } 
    
    public Money BetAmount { get; private init; }

    public RouletteBetType BetType { get; private init; }

    public BetValues BetValues { get; private init; }

    public BetStatus Status { get; set; } = BetStatus.Pending;

    public static CreateBetResult Create(
        Guid playerId,
        Money betAmount,
        IEnumerable<string> keys,
        RouletteBetType betType)
    {
        var betValues = new BetValues(keys, betType);
        if (betValues.Errors?.Count != 0)
        {
            return new(null, [.. betValues.Errors!]);
        }
        var bet = new RouletteBet(playerId, betAmount, betValues, betType);
        return new(bet, []);
    }
}
