using GamingService.Application.Models.Sessions.Queries.GetSessionDetails;
using MediatR;

namespace GamingService.Application.Models.Sessions.Commands.CreateSession;

public record CreateRouletteSessionCommand(string ConfigurationId, string ClientSeed) 
    : IRequest<RouletteSessionVm>;
