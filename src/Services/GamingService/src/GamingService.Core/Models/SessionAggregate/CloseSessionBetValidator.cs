using GamingService.Core.Models.RouletteConfigurationAggregate;
using static GamingService.Core.Constants.ErrorMessages;

namespace GamingService.Core.Models.SessionAggregate;

public static class CloseSessionBetValidator
{
    public static void Validate(this IEnumerable<RouletteBet> bets, RouletteConfiguration configuration, string sessionResult)
    {
        foreach (var bet in bets)
        {
            ArgumentNullException.ThrowIfNull(bet);

            if (bet.Errors?.Count != 0)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                continue;
            }
            if (bet.Player.Balance.Currency != bet.BetAmount.Currency)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                bet.AddErrors(BetCurrencyMismatch);
                continue;
            }
            bet.Player.Balance.Amount.Value -= bet.BetAmount.Amount.Value;

            if (bet.Player.Balance.Amount.Value < 0)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                bet.AddErrors(PlayerBalanceInsufficient);
                continue;
            }
            if (bet.BetAmount.Amount.Value > configuration.MaxBet.Value)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                bet.AddErrors(BetAmountExceedsMaxBet);
                continue;
            }
            if (bet.BetAmount.Amount.Value < configuration.MinBet.Value)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                bet.AddErrors(BetAmountBelowMinBet);
                continue;
            }
            if (bet.BetType == RouletteBetType.Basket && configuration.RouletteGameType == RouletteGameType.European)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                bet.AddErrors(BasketBetNotAllowedInEuropeanRoulette);
                continue;
            }

            if (bet.BetValues.Keys!.Contains("00") && configuration.RouletteGameType == RouletteGameType.European)
            {
                bet.ChangeStatus(BetStatus.Cancelled);
                bet.AddErrors(BetValuesCannotContain00InEuropeanRoulette);
                continue;
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
