using GamingService.Core.Contracts;
using GamingService.Core.Models.RouletteConfigurationAggregate;

namespace GamingService.Core.Abstractions;

public interface IRouletteConfiguratonsRepository
{
    public Task<RouletteConfiguration> CreateAsync(RouletteConfiguration configuration, CancellationToken cancellationToken = default);

    public Task<RouletteConfiguration> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<PagedRouletteConfigurationsProjection> GetPagedAsync(PagedRouletteConfigurationsFilter filter, CancellationToken cancellationToken = default);

    public Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    public Task<bool> ExistsAsync (Guid id, CancellationToken cancellationToken = default);
}
