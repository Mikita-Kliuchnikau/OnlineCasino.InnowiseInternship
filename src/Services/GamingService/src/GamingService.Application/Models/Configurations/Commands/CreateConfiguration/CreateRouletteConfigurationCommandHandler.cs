using AutoMapper;
using GamingService.Application.Models.Configurations.Queries.GetConfigurationDetails;
using GamingService.Core.Abstractions;
using GamingService.Core.Common;
using GamingService.Core.Models.RouletteConfigurationAggregate;
using MediatR;
using static GamingService.Core.Constants.ErrorMessages;

namespace GamingService.Application.Models.Configurations.Commands.CreateConfiguration;

public class CreateRouletteConfigurationCommandHandler(IRouletteConfiguratonsRepository repository, IMapper mapper) 
    : IRequestHandler<CreateRouletteConfigurationCommand, RouletteConfigurationVm>
{
    public async Task<RouletteConfigurationVm> Handle(CreateRouletteConfigurationCommand request, CancellationToken cancellationToken)
    {
        var configuration = RouletteConfiguration.Create(
            RouletteGameType.FromName(request.RouletteType) ?? throw new ArgumentException(string.Format(ConfigurationNotFound, request.RouletteType)),
            Enum.Parse<Currency>(request.Currency),
            new Amount(request.MinBet),
            new Amount(request.MaxBet),
            request.Engine);

        return mapper.Map<RouletteConfigurationVm>(await repository.CreateAsync(configuration, cancellationToken));
    }
}
