using GamingService.Core.Contracts;
using GamingService.Core.Models.SessionAggregate;

namespace GamingService.Core.Abstractions;

public interface ISessionsRepository
{
    Task<RouletteSession> CreateAsync(RouletteSession session, CancellationToken cancellationToken = default);
    
    Task<RouletteSession> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    Task<PagedRouletteSessionsProjection> GetPagedAsync(PagedRouletteSessionsFilter filter, CancellationToken cancellationToken = default);

    Task<RouletteSession> UpdateAsync(RouletteSession session, CancellationToken cancellationToken = default);
}
