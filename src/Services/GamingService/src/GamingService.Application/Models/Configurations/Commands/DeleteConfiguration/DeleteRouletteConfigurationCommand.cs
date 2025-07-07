using MediatR;

namespace GamingService.Application.Models.Configurations.Commands.DeleteConfiguration;

public record DeleteRouletteConfigurationCommand(string Id) : IRequest<string>;
