using AutoMapper;
using GamingService.Application.Models.Configurations.Queries.GetConfigurationDetails;
using GamingService.Core.Abstractions;
using GamingService.Core.Models.RouletteConfigurationAggregate;
using MediatR;

namespace GamingService.Application.Models.Configurations.Commands.CreateConfiguration;

public class CreateRouletteConfigurationCommandHandler(IRouletteConfiguratonsRepository repository, IMapper mapper) 
    : IRequestHandler<CreateRouletteConfigurationCommand, RouletteConfigurationViewModel>
{
    public async Task<RouletteConfigurationViewModel> Handle(CreateRouletteConfigurationCommand request, CancellationToken cancellationToken)
    {
        var configuration = mapper.Map<RouletteConfiguration>(request);

        configuration = await repository.CreateAsync(configuration, cancellationToken);

        return mapper.Map<RouletteConfigurationViewModel>(configuration);
    }
}
