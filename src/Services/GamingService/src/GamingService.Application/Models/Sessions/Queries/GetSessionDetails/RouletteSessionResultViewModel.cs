using AutoMapper;
using GamingService.Application.Common.Mapping;
using GamingService.Core.Models.SessionAggregate;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionDetails;

public class RouletteSessionResultViewModel : IMapWith<RouletteSession>
{
    public Guid Id { get; set; }
    public DateTime StartedAt { get; set; }
    public string ServerSeed { get; set; } = string.Empty;
    public string ServerSeedHash { get; set; } = string.Empty;
    public string ClientSeed { get; set; } = string.Empty;
    public string SessionResult { get; set; } = string.Empty;
    public IEnumerable<RouletteBetViewModel> Bets { get; set; } = [];
    public Guid ConfigurationId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<RouletteSession, RouletteSessionResultViewModel>()
            .ForMember(sessionViewModel => sessionViewModel.Id, 
                opt => opt.MapFrom(session => session.Id))
            .ForMember(sessionViewModel => sessionViewModel.StartedAt, 
                opt => opt.MapFrom(session => session.StartedAt))
            .ForMember(sessionViewModel => sessionViewModel.ServerSeed, 
                opt => opt.MapFrom(session => session.ServerSeed))
            .ForMember(sessionViewModel => sessionViewModel.ServerSeedHash, 
                opt => opt.MapFrom(session => session.ServerSeedHash))
            .ForMember(sessionViewModel => sessionViewModel.ClientSeed, 
                opt => opt.MapFrom(session => session.ClientSeed))
            .ForMember(sessionViewModel => sessionViewModel.SessionResult, 
                opt => opt.MapFrom(session => session.SessionResult.Result))
            .ForMember(sessionViewModel => sessionViewModel.Bets,
                opt => opt.MapFrom(session => session.Bets))
            .ForMember(sessionViewModel => sessionViewModel.ConfigurationId, 
                opt => opt.MapFrom(session => session.ConfigurationId));
    }
}
