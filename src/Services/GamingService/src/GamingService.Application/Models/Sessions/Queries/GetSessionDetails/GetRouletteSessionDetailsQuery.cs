using MediatR;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionDetails;

public record class GetRouletteSessionDetailsQuery(string SessionId) : IRequest<RouletteSessionViewModel>;
