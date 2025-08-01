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
    IRouletteConfiguratonsRepository configuratonsRepository,
    IMapper mapper) : IRequestHandler<CloseRouletteSessionCommand, RouletteSessionResultViewModel>
{
    public async Task<RouletteSessionResultViewModel> Handle(CloseRouletteSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await sessionsRepository.GetByIdAsync(request.SessionId, cancellationToken)
            ?? throw new ArgumentException(string.Format(SessionNotFound, request.SessionId));

        var rouletteBets = request.Bets
            .Select(bet => mapper.Map<RouletteBet>(bet))
            .ToList();

        var playersId = rouletteBets
            .Select(bet => bet.PlayerId)
            .Distinct()
            .ToList();

        foreach (var playerId in playersId)
        {
            var existsResult = await playersRepository.ExistsAsync(playerId, cancellationToken);
            if (!existsResult.Value)
            {
                foreach (var bet in rouletteBets.Where(b => b.PlayerId == playerId))
                {
                    var errorMessage = string.IsNullOrWhiteSpace(existsResult.ErrorMessage)
                            ? string.Format(PlayerNotFound, playerId)
                            : existsResult.ErrorMessage;

                    bet.AddErrors(errorMessage);
                }
            }
        }

        session = await session.CloseSession(rouletteBets, playersRepository, configuratonsRepository);

        await sessionsRepository.CloseAsync(session, cancellationToken);

        return mapper.Map<RouletteSessionResultViewModel>(session);
    }
}
