using MediatR;

namespace GamingService.Application.Models.Configurations.Commands.DeleteConfiguration;

public record DeleteRouletteConfigurationCommand(Guid Id) : IRequest<Guid>;
