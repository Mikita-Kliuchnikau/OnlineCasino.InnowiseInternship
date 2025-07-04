using AutoMapper;
using GamingService.Application.Models.Sessions.Queries.GetSessionDetails;
using GamingService.Core.Abstractions;
using GamingService.Core.Models.SessionAggregate;
using MediatR;
using static GamingService.Core.Constants.ErrorMessages;

namespace GamingService.Application.Models.Sessions.Commands.CloseSession;

public class CloseRouletteSessionCommandHandler(
    ISessionsRepository sessionsRepository,
    IPlayersRepository playersRepository,
    IMapper mapper)
    : IRequestHandler<CloseRouletteSessionCommand, RouletteSessionViewModel>
{
    public async Task<RouletteSessionViewModel> Handle(CloseRouletteSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await sessionsRepository.GetByIdAsync(request.SessionId, cancellationToken)
            ?? throw new ArgumentException(string.Format(SessionNotFound, request.SessionId));

        var rouletteBets = mapper.Map<IEnumerable<RouletteBet>>(request.Bets)
            .ToList();

        var playersId = rouletteBets
            .Select(bet => bet.PlayerId)
            .Distinct()
            .ToList();

        foreach (var playerId in playersId)
        {
            if (!await playersRepository.IsExistAsync(playerId, cancellationToken))
            {
                foreach (var bet in rouletteBets.Where(b => b.PlayerId == playerId))
                {
                    bet.AddErrors(PlayerNotFound);
                }
            }
        }

        session = await session.CloseSession(rouletteBets, playersRepository);

        await sessionsRepository.UpdateAsync(session, cancellationToken);

        return mapper.Map<RouletteSessionViewModel>(session);
    }
}
