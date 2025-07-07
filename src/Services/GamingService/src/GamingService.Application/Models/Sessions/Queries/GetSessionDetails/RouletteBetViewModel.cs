using GamingService.Application.Common.Mapping;
using GamingService.Core.Models.SessionAggregate;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionDetails;

public record RouletteBetViewModel(
        string PlayerId,
        decimal BetAmount,
        string Currency,
        List<string>? Errors,
        string RouletteBetType) : IMapWith<RouletteBet>
{
    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<RouletteBet, RouletteBetViewModel>()
            .ForMember(betViewModel => betViewModel.PlayerId,
                opt => opt.MapFrom(bet => bet.PlayerId))
            .ForMember(betViewModel => betViewModel.BetAmount,
                opt => opt.MapFrom(bet => bet.BetAmount.Amount.Value))
            .ForMember(betViewModel => betViewModel.Currency,
                opt => opt.MapFrom(bet => bet.BetAmount.Currency.ToString()))
            .ForMember(betViewModel => betViewModel.Errors,
                opt => opt.MapFrom(bet => bet.Errors))
            .ForMember(betViewModel => betViewModel.RouletteBetType,
                opt => opt.MapFrom(bet => bet.BetType.Name));
    }
}

