using GamingService.Application.Models.Configurations.Commands.CreateConfiguration;
using GamingService.Application.Models.Configurations.Commands.DeleteConfiguration;
using GamingService.Application.Models.Configurations.Queries.GetConfigurationDetails;
using GamingService.Application.Models.Configurations.Queries.GetConfigurationList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GamingService.Presentation.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
public class RouletteConfigurationsController : ControllerBase
{
    private IMediator? Mediator => HttpContext.RequestServices.GetService<IMediator>();

    [HttpGet]
    public async Task<ActionResult<RouletteConfigurationListViewModel>> Get(
        [FromQuery] GetRouletteConfigurationListQuery configurationListQuery,
        CancellationToken cancellationToken = default)
    {
        return await Mediator?.Send(configurationListQuery, cancellationToken)!;
    }

    [HttpGet("{id}")]
    public async Task<RouletteConfigurationViewModel> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var configurationQuery = new GetRouletteConfigurationDetailsQuery(id);
        return await Mediator?.Send(configurationQuery, cancellationToken)!;
    }

    [HttpPost]
    public async Task<RouletteConfigurationViewModel> Create([FromBody] CreateRouletteConfigurationCommand configuration, CancellationToken cancellationToken = default)
    {
        return await Mediator?.Send(configuration, cancellationToken)!;
    }

    [HttpDelete("{id}")]
    public async Task<Guid> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var deleteConfigurationCommand = new DeleteRouletteConfigurationCommand(id);
        return await Mediator?.Send(deleteConfigurationCommand, cancellationToken)!;
    }
}
