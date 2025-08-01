using MediatR;

namespace GamingService.Application.Models.Configurations.Queries.GetConfigurationDetails;

public record GetRouletteConfigurationDetailsQuery(Guid Id) : IRequest<RouletteConfigurationViewModel>;
