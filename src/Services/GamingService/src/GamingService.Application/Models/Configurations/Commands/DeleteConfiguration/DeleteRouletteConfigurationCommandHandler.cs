using GamingService.Core.Abstractions;
using MediatR;

namespace GamingService.Application.Models.Configurations.Commands.DeleteConfiguration;

public class DeleteRouletteConfigurationCommandHandler(IRouletteConfiguratonsRepository repository) 
    : IRequestHandler<DeleteRouletteConfigurationCommand, Guid>
{
    public async Task<Guid> Handle(DeleteRouletteConfigurationCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.DeleteAsync(
            request.Id,
            cancellationToken);
        return result;
    }
}
