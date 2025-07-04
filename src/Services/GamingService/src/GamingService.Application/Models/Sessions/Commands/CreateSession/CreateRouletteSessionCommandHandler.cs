using AutoMapper;
using GamingService.Application.Models.Sessions.Queries.GetSessionDetails;
using GamingService.Core.Abstractions;
using GamingService.Core.Models.SessionAggregate;
using MediatR;
using static GamingService.Core.Constants.ErrorMessages;

namespace GamingService.Application.Models.Sessions.Commands.CreateSession;

public class CreateRouletteSessionCommandHandler(IRouletteConfiguratonsRepository rouletteConfiguration, ISessionsRepository sessionRepository, IDomainEventPublisher domainEventPublisher, IMapper mapper) 
    : IRequestHandler<CreateRouletteSessionCommand, RouletteSessionViewModel>
{
    public async Task<RouletteSessionViewModel> Handle(CreateRouletteSessionCommand request, CancellationToken cancellationToken)
    {
        var configuration = await rouletteConfiguration.GetByIdAsync(request.ConfigurationId, cancellationToken)
            ?? throw new ArgumentException(string.Format(ConfigurationNotFound, request.ConfigurationId));

        var session = RouletteSession.Create(request.ClientSeed, configuration, domainEventPublisher);
        session = await sessionRepository.CreateAsync(session, cancellationToken);
        return mapper.Map<RouletteSessionViewModel>(session);
    }
}
