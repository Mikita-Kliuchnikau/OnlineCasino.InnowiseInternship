using GamingService.Core.Abstractions;
using GamingService.Core.Models.RouletteConfigurationAggregate;
using System.Threading;
using static GamingService.Core.Constants.ErrorMessages;

namespace GamingService.Core.Models.SessionAggregate;

public static class CloseSessionBetValidator
{
    public static async Task Validate(this IEnumerable<RouletteBet> bets, 
        RouletteConfiguration configuration, 
        string sessionResult, 
        IPlayersRepository playersRepository, 
        CancellationToken cancellationToken = default)
    {
        foreach (var bet in bets)
        {
            if (bet.Errors?.Count != 0)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                continue;
            }
            if (bet.BetAmount.Amount.Value > configuration.MaxBet.Value)
            {
                bet.AddErrors(BetAmountExceedsMaxBet);
                continue;
            }
            if (bet.BetAmount.Amount.Value < configuration.MinBet.Value)
            {
                bet.AddErrors(BetAmountBelowMinBet);
                continue;
            }
            if (bet.BetType == RouletteBetType.Basket && configuration.RouletteGameType == RouletteGameType.European)
            {
                bet.AddErrors(BasketBetNotAllowedInEuropeanRoulette);
                continue;
            }
            if (bet.BetValues.Keys!.Contains("00") && configuration.RouletteGameType == RouletteGameType.European)
            {
                bet.AddErrors(BetValuesCannotContain00InEuropeanRoulette);
                continue;
            }

            if (bet.Errors?.Count > 0 && !await playersRepository.IsDeductedFormPlayersBalanceAsync(
                    bet.PlayerId,
                    bet.BetAmount.Amount.Value,
                    cancellationToken))
            {
                bet.AddErrors(PlayerBalanceInsufficient);
            }

            if (bet.Status == BetStatus.Pending)
            {
                var betStatus = bet.BetValues.Keys!.Contains(sessionResult.ToString())
                    ? BetStatus.Won
                    : BetStatus.Lost;
                bet.ChangeStatus(betStatus);
            }
        }
    }
}
