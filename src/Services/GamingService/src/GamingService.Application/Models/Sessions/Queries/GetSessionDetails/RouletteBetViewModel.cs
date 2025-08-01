using GamingService.Application.Common.Mapping;
using GamingService.Core.Common;
using GamingService.Core.Models.SessionAggregate;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionDetails;

public class RouletteBetViewModel : IMapWith<RouletteBet>
{
    public Guid PlayerId { get; set; }
    public required Money BetAmount { get; set; }
    public IEnumerable<string>? Errors { get; set; }
    public string RouletteBetType { get; set; } = string.Empty;

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<RouletteBet, RouletteBetViewModel>()
            .ForMember(betViewModel => betViewModel.PlayerId,
                opt => opt.MapFrom(bet => bet.PlayerId))
            .ForMember(betViewModel => betViewModel.BetAmount,
                opt => opt.MapFrom(bet => bet.BetAmount))
            .ForMember(betViewModel => betViewModel.Errors,
                opt => opt.MapFrom(bet => bet.Errors))
            .ForMember(betViewModel => betViewModel.RouletteBetType,
                opt => opt.MapFrom(bet => bet.BetType.Name));
    }
}

