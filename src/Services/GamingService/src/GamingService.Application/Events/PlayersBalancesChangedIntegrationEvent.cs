namespace GamingService.Application.Events;

public record PlayersBalancesChangedIntegrationEvent(IEnumerable<(string playerId, decimal betAmount)> BalanceLambda);
