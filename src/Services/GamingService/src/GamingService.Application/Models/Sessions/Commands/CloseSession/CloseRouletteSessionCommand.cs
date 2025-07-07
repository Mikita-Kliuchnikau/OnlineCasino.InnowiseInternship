using GamingService.Application.Common.Extensions;
using GamingService.Application.Common.Mapping;
using GamingService.Application.Models.Sessions.Queries.GetSessionDetails;
using GamingService.Core.Common;
using GamingService.Core.Models.SessionAggregate;
using MediatR;

namespace GamingService.Application.Models.Sessions.Commands.CloseSession;

public record CloseRouletteSessionCommand(
    string SessionId,
    IEnumerable<(string playerId, decimal betAmount, string Currency, IEnumerable<string> betValues, string betType)> Bets)
    : IRequest<RouletteSessionViewModel>, IMapWith<IEnumerable<RouletteBet>>
{
    public void Mapping(AutoMapper.Profile profile)
    {
        var betValues = Bets.Select(bet =>
        {
            var betType = bet.betType.ParseEnum<BetType>();
            var rouletteBetType = RouletteBetType.FromName(betType.ToString()) ?? RouletteBetType.Default;
            var currency = bet.Currency.ParseEnum<Currency>();
            return (
                bet.playerId,
                new Money(currency, new Amount(bet.betAmount)),
                bet.betValues,
                rouletteBetType
            );
        });

        profile.CreateMap<CloseRouletteSessionCommand, IEnumerable<RouletteBet>>().ConvertUsing(
            command => RouletteBet.Create(betValues)
        );
    }
}
