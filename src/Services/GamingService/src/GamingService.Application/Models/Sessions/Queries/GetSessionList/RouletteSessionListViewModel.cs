using AutoMapper;
using GamingService.Application.Models.Sessions.Queries.GetSessionDetails;
using GamingService.Core.Contracts;
using GamingService.Mapping.Interfaces;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionList;

public class RouletteSessionListViewModel : IMapWith<PagedRouletteSessionsProjection>
{
    public IReadOnlyList<RouletteSessionResultViewModel>? Sessions { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PagedRouletteSessionsProjection, RouletteSessionListViewModel>()
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
