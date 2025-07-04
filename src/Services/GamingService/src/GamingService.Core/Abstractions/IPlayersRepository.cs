namespace GamingService.Core.Abstractions;

public interface IPlayersRepository
{
    Task<bool> IsExistAsync(string id, CancellationToken cancellationToken = default);

    Task<bool> IsDeductedFormPlayersBalanceAsync(
        string userId,
        decimal betAmount,
        CancellationToken cancellationToken = default);
}
