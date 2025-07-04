using AutoMapper;
using GamingService.Application.Models.Sessions.Queries.GetSessionDetails;
using GamingService.Core.Abstractions;
using GamingService.Core.Common;
using GamingService.Core.Models.SessionAggregate;
using MediatR;
using static GamingService.Core.Constants.ErrorMessages;

namespace GamingService.Application.Models.Sessions.Commands.CloseSession;

public class CloseRouletteSessionCommandHandler(
    ISessionsRepository sessionsRepository,
    IPlayersRepository playersRepository,
    IMapper mapper)
    : IRequestHandler<CloseRouletteSessionCommand, RouletteSessionVm>
{
    public async Task<RouletteSessionVm> Handle(CloseRouletteSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await sessionsRepository.GetByIdAsync(request.SessionId, cancellationToken)
            ?? throw new ArgumentException(string.Format(SessionNotFound, request.SessionId));

        var betValues = request.Bets.Select(bet =>
        {
            var rouletteBetType = Enum.TryParse(bet.betType, out BetType betType)
                ? RouletteBetType.FromName(betType.ToString()) ?? RouletteBetType.Default
                : RouletteBetType.Default;

            return (
                bet.playerId,
                new Money(
                    Enum.TryParse(bet.Currency, out Currency currency) ? currency : Currency.InvalidCurrency,
                    new Amount(bet.betAmount)),
                bet.betValues,
                rouletteBetType
            );
        });

        var rouletteBets = RouletteBet.Create(betValues);

        foreach (var playerId in rouletteBets
            .Select(bet => bet.PlayerId)
            .Distinct())
        {
            if (!await playersRepository.IsExistAsync(playerId, cancellationToken))
            {
                foreach (var bet in rouletteBets.Where(b => b.PlayerId == playerId))
                {
                    bet.AddErrors(PlayerNotFound);
                }
            }
        }

        session = await session.CloseSession(rouletteBets.Select(bet => bet), playersRepository);

        await sessionsRepository.UpdateAsync(session, cancellationToken);

        return mapper.Map<RouletteSessionVm>(session);
    }
}
