using GamingService.Core.Common;
using GamingService.Core.Models.SessionAggregate;
using AutoMapper;
using GamingService.Mapping.Interfaces;

namespace GamingService.Application.Models.Sessions.Commands.CloseSession;

public class BetsPayload : IMapWith<RouletteBet>
{
    public Guid PlayerId { get; set; }
    public required Money BetAmount { get; set; }
    public IEnumerable<string> BetValues { get; set; } = [];
    public BetType BetType { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BetsPayload, RouletteBet>()
            .ConvertUsing(src =>
                RouletteBet.Create(
                    src.PlayerId,
                    src.BetAmount,
                    src.BetValues,
                    RouletteBetType.FromName(src.BetType.ToString()) ?? RouletteBetType.Default));
    }
}