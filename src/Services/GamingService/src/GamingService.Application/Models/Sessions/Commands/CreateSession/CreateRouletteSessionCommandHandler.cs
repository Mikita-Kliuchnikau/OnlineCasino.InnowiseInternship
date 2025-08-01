using AutoMapper;
using GamingService.Application.Models.Sessions.Queries.GetSessionDetails;
using GamingService.Core.Abstractions;
using GamingService.Core.Models.SessionAggregate;
using MediatR;
using static GamingService.Core.Constants.ErrorMessages;

namespace GamingService.Application.Models.Sessions.Commands.CreateSession;

public class CreateRouletteSessionCommandHandler(
    IRouletteConfiguratonsRepository configurationRepository, 
    ISessionsRepository sessionRepository, 
    IMapper mapper) 
    : IRequestHandler<CreateRouletteSessionCommand, RouletteSessionSummaryViewModel>
{
    public async Task<RouletteSessionSummaryViewModel> Handle(CreateRouletteSessionCommand request, CancellationToken cancellationToken)
    {
        var isExists = !await configurationRepository.ExistsAsync(request.ConfigurationId, cancellationToken);
        if (!isExists)
        {
            throw new ArgumentException(string.Format(ConfigurationNotFound, request.ConfigurationId));
        }

        var session = await RouletteSession.Create(request.ClientSeed, request.ConfigurationId, configurationRepository);
        session = await sessionRepository.CreateAsync(session, cancellationToken);
        return mapper.Map<RouletteSessionSummaryViewModel>(session);
    }
}
