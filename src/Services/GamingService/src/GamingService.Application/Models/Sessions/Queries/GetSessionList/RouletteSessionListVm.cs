using AutoMapper;
using GamingService.Application.Common.Mapping;
using GamingService.Application.Models.Sessions.Queries.GetSessionDetails;
using GamingService.Core.Contracts;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionList;

public record RouletteSessionListVm(
    IReadOnlyList<RouletteSessionVm>? Sessions,
    int TotalCount,
    int PageNumber,
    int PageSize) : IMapWith<PagedRouletteSessionsProjection>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PagedRouletteSessionsProjection, RouletteSessionListVm>()
            .ForMember(sessionListVm => sessionListVm.Sessions, 
                opt => opt.MapFrom(projection => projection.Sessions))
            .ForMember(sessionListVm => sessionListVm.TotalCount,
                opt => opt.MapFrom(projection => projection.TotalCount))
            .ForMember(sessionListVm => sessionListVm.PageNumber,
                opt => opt.MapFrom(projection => projection.PageNumber))
            .ForMember(sessionListVm => sessionListVm.PageSize,
                opt => opt.MapFrom(projection => projection.PageSize));
    }
}
