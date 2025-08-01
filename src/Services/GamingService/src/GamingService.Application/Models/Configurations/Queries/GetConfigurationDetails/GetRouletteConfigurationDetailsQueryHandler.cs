using AutoMapper;
using GamingService.Core.Abstractions;
using MediatR;

namespace GamingService.Application.Models.Configurations.Queries.GetConfigurationDetails;

public class GetRouletteConfigurationDetailsQueryHandler(IRouletteConfiguratonsRepository configurationsRepository, IMapper mapper)
    : IRequestHandler<GetRouletteConfigurationDetailsQuery, RouletteConfigurationViewModel>
{
    public async Task<RouletteConfigurationViewModel> Handle(GetRouletteConfigurationDetailsQuery request, CancellationToken cancellationToken)
    {
        var configuration = await configurationsRepository.GetByIdAsync(Guid.Parse(request.Id), cancellationToken);
        return mapper.Map<RouletteConfigurationViewModel>(configuration);
    }
}
