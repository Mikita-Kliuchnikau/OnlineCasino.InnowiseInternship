using AutoMapper;
using GamingService.Application.Common.Mapping;
using GamingService.Core.Contracts;
using MediatR;

namespace GamingService.Application.Models.Configurations.Queries.GetConfigurationList;

public class GetRouletteConfigurationListQuery
    : IRequest<RouletteConfigurationListViewModel>, IMapWith<PagedRouletteConfigurationsFilter>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GetRouletteConfigurationListQuery, PagedRouletteConfigurationsFilter>()
            .ForMember(filter => filter.PageNumber, opt => opt.MapFrom(query => query.PageNumber))
            .ForMember(filter => filter.PageSize, opt => opt.MapFrom(query => query.PageSize));
    }
}
