using GamingService.Application.Models.Sessions.Commands.CloseSession;
using GamingService.Application.Models.Sessions.Commands.CreateSession;
using GamingService.Application.Models.Sessions.Queries.GetSessionDetails;
using GamingService.Application.Models.Sessions.Queries.GetSessionList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamingService.Presentation.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
public class RouletteSessionsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<RouletteSessionListViewModel> Get(
        [FromQuery] GetRouletteSessionListQuery sessionListQuery,
        CancellationToken cancellationToken = default)
    {
        return await mediator?.Send(sessionListQuery, cancellationToken)!;
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<RouletteSessionResultViewModel> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var sessionDetailsQuery = new GetRouletteSessionDetailsQuery(id);
        return await mediator?.Send(sessionDetailsQuery, cancellationToken)!;
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<RouletteSessionSummaryViewModel> Create([FromBody] CreateRouletteSessionCommand session, CancellationToken cancellationToken = default)
    {
        return await mediator?.Send(session, cancellationToken)!;
    }

    [HttpPost("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<RouletteSessionResultViewModel> Close(Guid id, [FromBody] IEnumerable<BetsPayload> Bets, CancellationToken cancellationToken = default)
    {
        var command = new CloseRouletteSessionCommand
        {
            SessionId = id,
            Bets = Bets
        };
        return await mediator?.Send(command, cancellationToken)!;
    }
}
