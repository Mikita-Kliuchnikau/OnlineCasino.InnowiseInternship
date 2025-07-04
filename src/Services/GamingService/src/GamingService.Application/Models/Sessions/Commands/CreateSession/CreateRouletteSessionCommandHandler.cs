using AutoMapper;
using GamingService.Application.Models.Sessions.Queries.GetSessionDetails;
using GamingService.Core.Abstractions;
using GamingService.Core.Models.SessionAggregate;
using MediatR;
using static GamingService.Core.Constants.ErrorMessages;

namespace GamingService.Application.Models.Sessions.Commands.CreateSession;

public class CreateRouletteSessionCommandHandler(IRouletteConfiguratonsRepository rouletteConfiguration, ISessionsRepository sessionRepository, IMediator mediator, IMapper mapper) 
    : IRequestHandler<CreateRouletteSessionCommand, RouletteSessionVm>
{
    public async Task<RouletteSessionVm> Handle(CreateRouletteSessionCommand request, CancellationToken cancellationToken)
    {
        var configuration = await rouletteConfiguration.GetByIdAsync(request.ConfigurationId, cancellationToken)
            ?? throw new ArgumentException(string.Format(ConfigurationNotFound, request.ConfigurationId));

        var session = RouletteSession.Create(request.ClientSeed, configuration, mediator);
        return mapper.Map<RouletteSessionVm>(await sessionRepository.CreateAsync(session, cancellationToken));
    }
}
