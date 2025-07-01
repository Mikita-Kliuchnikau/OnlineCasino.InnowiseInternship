using GamingService.Core.Models.RoulettePlayerAggregate;

namespace GamingService.Core.Abstractions;

public interface IUsersRepository
{
    Task<RoulettePlayer> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
