using GamingService.Core.Abstractions;
using GamingService.Core.Models.RouletteConfigurationAggregate;
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
            if (bet.BetAmount.Amount.Value > configuration.MaxBet.Value)
            {
                bet.AddErrors(BetAmountExceedsMaxBet);
            }
            if (bet.BetAmount.Amount.Value < configuration.MinBet.Value)
            {
                bet.AddErrors(BetAmountBelowMinBet);
            }
            if (bet.BetType == RouletteBetType.Basket && configuration.RouletteGameType == RouletteGameType.European)
            {
                bet.AddErrors(BasketBetNotAllowedInEuropeanRoulette);
            }
            if (bet.BetValues.Keys!.Contains("00") && configuration.RouletteGameType == RouletteGameType.European)
            {
                bet.AddErrors(BetValuesCannotContain00InEuropeanRoulette);
            }

            if (bet.Errors?.Count == 0)
            {
                var isPlayerBalanceInsufficient = await playersRepository.DeductedFormPlayersBalanceAsync(
                    bet.PlayerId,
                    bet.BetAmount.Amount.Value,
                    cancellationToken);

                var errorMessage = string.IsNullOrWhiteSpace(isPlayerBalanceInsufficient.ErrorMessage)
                    ? PlayerBalanceInsufficient
                    : isPlayerBalanceInsufficient.ErrorMessage;

                if (!isPlayerBalanceInsufficient.Value)
                {
                    bet.AddErrors(errorMessage);
                }
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
