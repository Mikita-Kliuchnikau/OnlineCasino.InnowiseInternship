using AutoMapper;
using GamingService.Application.Common.Mapping;
using GamingService.Application.Models.Sessions.Queries.GetSessionDetails;
using GamingService.Core.Contracts;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionList;

public record GetRouletteSessionListViewModel(
    IReadOnlyList<RouletteSessionViewModel>? Sessions,
    int TotalCount,
    int PageNumber,
    int PageSize) : IMapWith<PagedRouletteSessionsProjection>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PagedRouletteSessionsProjection, GetRouletteSessionListViewModel>()
            .ForMember(sessionListViewModel => sessionListViewModel.Sessions, 
                opt => opt.MapFrom(projection => projection.Sessions))
            .ForMember(sessionListViewModel => sessionListViewModel.TotalCount,
                opt => opt.MapFrom(projection => projection.TotalCount))
            .ForMember(sessionListViewModel => sessionListViewModel.PageNumber,
                opt => opt.MapFrom(projection => projection.PageNumber))
            .ForMember(sessionListViewModel => sessionListViewModel.PageSize,
                opt => opt.MapFrom(projection => projection.PageSize));
    }
}
