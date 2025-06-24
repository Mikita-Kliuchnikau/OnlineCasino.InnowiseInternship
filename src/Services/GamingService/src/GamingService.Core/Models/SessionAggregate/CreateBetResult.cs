namespace GamingService.Core.Models.SessionAggregate;

public record CreateBetResult(RouletteBet? Bet, List<string>? Errors);
