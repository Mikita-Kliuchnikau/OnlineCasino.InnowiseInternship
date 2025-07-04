using GamingService.Core.Models.RouletteConfigurationAggregate;

namespace GamingService.Core.Abstractions;

public interface IRouletteConfiguratonsRepository
{
    public Task<RouletteConfiguration> CreateAsync(RouletteConfiguration configuration, CancellationToken cancellationToken = default);

    public Task<RouletteConfiguration> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    public Task<string> DeleteAsync(string id, CancellationToken cancellationToken = default);
}
