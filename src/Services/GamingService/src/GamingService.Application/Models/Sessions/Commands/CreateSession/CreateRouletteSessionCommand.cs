using GamingService.Application.Models.Sessions.Queries.GetSessionDetails;
using MediatR;

namespace GamingService.Application.Models.Sessions.Commands.CreateSession;

public record CreateRouletteSessionCommand(Guid ConfigurationId, string ClientSeed) 
    : IRequest<RouletteSessionSummaryViewModel>;
