using MediatR;

namespace GamingService.Application.Models.Configurations.Queries.GetConfigurationDetails;

public record GetRouletteConfigurationDetailsQuery(string Id) : IRequest<RouletteConfigurationVm>;
