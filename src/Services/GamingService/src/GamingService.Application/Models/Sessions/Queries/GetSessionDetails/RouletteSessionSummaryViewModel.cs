using AutoMapper;
using GamingService.Application.Common.Mapping;
using GamingService.Core.Models.SessionAggregate;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionDetails;

public class RouletteSessionSummaryViewModel : IMapWith<RouletteSession>
{
    public Guid Id { get; set; }
    public DateTime StartedAt { get; set; }
    public string ServerSeedHash { get; set; } = string.Empty;
    public string ClientSeed { get; set; } = string.Empty;
    public Guid ConfigurationId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<RouletteSession, RouletteSessionSummaryViewModel>()
            .ForMember(sessionViewModel => sessionViewModel.Id, 
                opt => opt.MapFrom(session => session.Id))
            .ForMember(sessionViewModel => sessionViewModel.StartedAt, 
                opt => opt.MapFrom(session => session.StartedAt))
            .ForMember(sessionViewModel => sessionViewModel.ServerSeedHash, 
                opt => opt.MapFrom(session => session.ServerSeedHash))
            .ForMember(sessionViewModel => sessionViewModel.ClientSeed, 
                opt => opt.MapFrom(session => session.ClientSeed))
            .ForMember(sessionViewModel => sessionViewModel.ConfigurationId, 
                opt => opt.MapFrom(session => session.ConfigurationId));
    }
}
