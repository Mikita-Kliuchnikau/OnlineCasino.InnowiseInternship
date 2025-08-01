using GamingService.Core.Abstractions;
using MediatR;

namespace GamingService.Application.Models.Configurations.Commands.DeleteConfiguration;

public class DeleteRouletteConfigurationCommandHandler(IRouletteConfiguratonsRepository repository) 
    : IRequestHandler<DeleteRouletteConfigurationCommand, string>
{
    public async Task<string> Handle(DeleteRouletteConfigurationCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.DeleteAsync(
            Guid.Parse(request.Id),
            cancellationToken);
        return result.ToString();
    }
}
