using GamingService.Application.Common.Mapping;
using GamingService.Core.Models.SessionAggregate;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionDetails;

public record RouletteBetVm(
        string PlayerId,
        decimal BetAmount,
        string Currency,
        List<string>? Errors,
        string RouletteBetType) : IMapWith<RouletteBet>
{
    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<RouletteBet, RouletteBetVm>()
            .ForMember(betVm => betVm.PlayerId,
                opt => opt.MapFrom(bet => bet.PlayerId))
            .ForMember(betVm => betVm.BetAmount,
                opt => opt.MapFrom(bet => bet.BetAmount.Amount.Value))
            .ForMember(betVm => betVm.Currency,
                opt => opt.MapFrom(bet => bet.BetAmount.Currency.ToString()))
            .ForMember(betVm => betVm.Errors,
                opt => opt.MapFrom(bet => bet.Errors))
            .ForMember(betVm => betVm.RouletteBetType,
                opt => opt.MapFrom(bet => bet.BetType.Name));
    }
}

