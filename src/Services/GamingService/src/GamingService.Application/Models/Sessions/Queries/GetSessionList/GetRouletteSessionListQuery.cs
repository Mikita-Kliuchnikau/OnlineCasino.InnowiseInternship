using GamingService.Core.Contracts;
using GamingService.Mapping.Interfaces;
using MediatR;

namespace GamingService.Application.Models.Sessions.Queries.GetSessionList;

public class GetRouletteSessionListQuery : IRequest<RouletteSessionListViewModel>, IMapWith<PagedRouletteSessionsFilter>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<GetRouletteSessionListQuery, PagedRouletteSessionsFilter>()
            .ForMember(filter => filter.PageNumber, opt => opt.MapFrom(query => query.PageNumber))
            .ForMember(filter => filter.PageSize, opt => opt.MapFrom(query => query.PageSize));
    }
}
