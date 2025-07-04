using AutoMapper;
using GamingService.Application.Common.Mapping;
using GamingService.Application.Models.Configurations.Queries.GetConfigurationDetails;
using GamingService.Core.Models.RouletteConfigurationAggregate;
using GamingService.Core.Models.SessionAggregate;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionDetails;

public record RouletteSessionVm(
    string Id, 
    DateTime StartedAt,
    string ServerSeed,
    string ServerSeedHash,
    string ClientSeed,
    string SessionResult,
    IEnumerable<RouletteBetVm> Bets,
    RouletteConfigurationVm Configuration) : IMapWith<RouletteSession>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<RouletteSession, RouletteSessionVm>()
            .ForMember(sessionVm => sessionVm.Id, 
                opt => opt.MapFrom(session => session.Id))
            .ForMember(sessionVm => sessionVm.StartedAt, 
                opt => opt.MapFrom(session => session.StartedAt))
            .ForMember(sessionVm => sessionVm.ServerSeed, 
                opt => opt.MapFrom(session => session.ServerSeed))
            .ForMember(sessionVm => sessionVm.ServerSeedHash, 
                opt => opt.MapFrom(session => session.ServerSeedHash))
            .ForMember(sessionVm => sessionVm.ClientSeed, 
                opt => opt.MapFrom(session => session.ClientSeed))
            .ForMember(sessionVm => sessionVm.SessionResult, 
                opt => opt.MapFrom(session => session.SessionResult.Result))
            .ForMember(sessionVm => sessionVm.Bets,
                opt => opt.MapFrom(session => session.Bets))
            .ForMember(sessionVm => sessionVm.Configuration, 
                opt => opt.MapFrom(session => session.Configuration));
    }
}
