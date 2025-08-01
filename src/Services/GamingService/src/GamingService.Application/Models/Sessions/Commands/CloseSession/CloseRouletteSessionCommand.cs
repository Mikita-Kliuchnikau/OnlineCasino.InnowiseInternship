using GamingService.Application.Models.Sessions.Queries.GetSessionDetails;
using MediatR;

namespace GamingService.Application.Models.Sessions.Commands.CloseSession;

public class CloseRouletteSessionCommand : IRequest<RouletteSessionDetailsViewModel>
{
    public Guid SessionId { get; set; }
    public IEnumerable<BetsPayload> Bets { get; set; } = [];
}
