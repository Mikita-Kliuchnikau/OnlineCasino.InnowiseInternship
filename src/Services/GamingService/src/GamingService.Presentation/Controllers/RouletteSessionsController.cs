using GamingService.Application.Models.Sessions.Commands.CloseSession;
using GamingService.Application.Models.Sessions.Commands.CreateSession;
using GamingService.Application.Models.Sessions.Queries.GetSessionDetails;
using GamingService.Application.Models.Sessions.Queries.GetSessionList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GamingService.Presentation.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
public class RouletteSessionsController : ControllerBase
{
    private IMediator? Mediator => HttpContext.RequestServices.GetService<IMediator>();

    [HttpGet]
    public async Task<RouletteSessionListViewModel> Get(
        [FromQuery] int page,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken = default)
    {
        var configurationQuery = new GetRouletteSessionListQuery
        {
            PageNumber = page,
            PageSize = pageSize
        };
        return await Mediator?.Send(configurationQuery, cancellationToken)!;
    }

    [HttpGet("{id}")]
    public async Task<RouletteSessionResultViewModel> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var configurationQuery = new GetRouletteSessionDetailsQuery(id);
        return await Mediator?.Send(configurationQuery, cancellationToken)!;
    }

    [HttpPost]
    public async Task<RouletteSessionSummaryViewModel> Create([FromBody] CreateRouletteSessionCommand session, CancellationToken cancellationToken = default)
    {
        return await Mediator?.Send(session, cancellationToken)!;
    }

    [HttpPost("{id}")]
    public async Task<RouletteSessionResultViewModel> Close(Guid id, [FromBody] IEnumerable<BetsPayload> Bets, CancellationToken cancellationToken = default)
    {
        var command = new CloseRouletteSessionCommand
        {
            SessionId = id,
            Bets = Bets
        };
        return await Mediator?.Send(command, cancellationToken)!;
    }
}
