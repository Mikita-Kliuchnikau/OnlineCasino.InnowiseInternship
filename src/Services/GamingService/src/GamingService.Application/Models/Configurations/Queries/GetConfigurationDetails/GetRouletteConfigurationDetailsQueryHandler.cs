using AutoMapper;
using GamingService.Core.Abstractions;
using MediatR;

namespace GamingService.Application.Models.Configurations.Queries.GetConfigurationDetails;

public class GetRouletteConfigurationDetailsQueryHandler(IRouletteConfiguratonsRepository repository, IMapper mapper)
    : IRequestHandler<GetRouletteConfigurationDetailsQuery, RouletteConfigurationVm>
{
    public async Task<RouletteConfigurationVm> Handle(GetRouletteConfigurationDetailsQuery request, CancellationToken cancellationToken)
    {
        return mapper.Map<RouletteConfigurationVm>(await repository.GetByIdAsync(
            request.Id,
            cancellationToken));
    }
}
