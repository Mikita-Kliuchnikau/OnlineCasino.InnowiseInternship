using GamingService.Application.Common.Mapping;
using GamingService.Application.Models.Configurations.Queries.GetConfigurationDetails;
using GamingService.Core.Contracts;

namespace GamingService.Application.Models.Configurations.Queries.GetConfigurationList;

public class RouletteConfigurationListViewModel : IMapWith<PagedRouletteConfigurationsProjection>
{
    public IEnumerable<RouletteConfigurationViewModel>? Configurations { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<PagedRouletteConfigurationsProjection, RouletteConfigurationListViewModel>()
            .ForMember(viewModel => viewModel.Configurations,
                opt => opt.MapFrom(projection => projection.Configurations))
            .ForMember(viewModel => viewModel.TotalCount,
                opt => opt.MapFrom(projection => projection.TotalCount))
            .ForMember(viewModel => viewModel.PageNumber,
                opt => opt.MapFrom(projection => projection.PageNumber))
            .ForMember(viewModel => viewModel.PageSize,
                opt => opt.MapFrom(projection => projection.PageSize));
    }
}