using AutoMapper;
using GamingService.Core.Abstractions;
using GamingService.Core.Contracts;
using MediatR;

namespace GamingService.Application.Models.Configurations.Queries.GetConfigurationList;

public class GetRouletteConfigurationListQueryHandler(IRouletteConfiguratonsRepository configurationsRepository, IMapper mapper)
    : IRequestHandler<GetRouletteConfigurationListQuery, RouletteConfigurationListViewModel>
{
    public async Task<RouletteConfigurationListViewModel> Handle(GetRouletteConfigurationListQuery request, CancellationToken cancellationToken)
    {
        var configurationsFilter = mapper.Map<PagedRouletteConfigurationsFilter>(request);
        var configurationsProjection = await configurationsRepository.GetPagedAsync(configurationsFilter, cancellationToken);
        return mapper.Map<RouletteConfigurationListViewModel>(configurationsProjection);
    }
}
