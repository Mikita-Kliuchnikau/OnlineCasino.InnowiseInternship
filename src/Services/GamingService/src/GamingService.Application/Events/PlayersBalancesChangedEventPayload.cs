namespace GamingService.Application.Events;

public record PlayersBalancesChangedEventPayload(string PlayerId, decimal BetAmount);
