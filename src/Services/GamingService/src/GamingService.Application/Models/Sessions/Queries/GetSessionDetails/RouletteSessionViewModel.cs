using AutoMapper;
using GamingService.Application.Common.Mapping;
using GamingService.Application.Models.Configurations.Queries.GetConfigurationDetails;
using GamingService.Core.Models.SessionAggregate;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionDetails;

public record RouletteSessionViewModel(
    string Id, 
    DateTime StartedAt,
    string ServerSeed,
    string ServerSeedHash,
    string ClientSeed,
    string SessionResult,
    IEnumerable<RouletteBetViewModel> Bets,
    RouletteConfigurationViewModel Configuration) : IMapWith<RouletteSession>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<RouletteSession, RouletteSessionViewModel>()
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
                opt => opt.MapFrom(session => session.Bets));
    }
}
