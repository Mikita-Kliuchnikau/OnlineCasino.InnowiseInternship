using GamingService.Core.Models.RouletteConfigurationAggregate;

namespace GamingService.Core.Abstractions;

public interface IRouletteConfiguratonsRepository
{
    public Task<Guid> CreateAsync(RouletteConfiguration configuration, CancellationToken cancellationToken = default);

    public Task<RouletteConfiguration> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
