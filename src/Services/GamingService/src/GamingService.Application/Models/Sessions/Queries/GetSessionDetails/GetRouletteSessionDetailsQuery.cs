using MediatR;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionDetails;

public record GetRouletteSessionDetailsQuery(Guid SessionId) : IRequest<RouletteSessionResultViewModel>;
