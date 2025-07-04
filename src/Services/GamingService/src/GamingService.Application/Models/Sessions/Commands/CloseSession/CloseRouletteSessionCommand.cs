using GamingService.Application.Models.Sessions.Queries.GetSessionDetails;
using MediatR;

namespace GamingService.Application.Models.Sessions.Commands.CloseSession;

public record CloseRouletteSessionCommand(
    string SessionId, 
    IEnumerable<(string playerId, decimal betAmount, string Currency, IEnumerable<string> betValues, string betType)> Bets) 
    : IRequest<RouletteSessionVm>;
