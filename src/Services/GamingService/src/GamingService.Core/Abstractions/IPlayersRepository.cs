using GamingService.Core.Contracts;

namespace GamingService.Core.Abstractions;

public interface IPlayersRepository
{
    Task<PlayersRepositoryResponse> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PlayersRepositoryResponse> DeductedFormPlayersBalanceAsync(
        Guid userId,
        decimal betAmount,
        CancellationToken cancellationToken = default);
}
