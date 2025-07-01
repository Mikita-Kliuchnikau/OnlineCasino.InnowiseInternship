using GamingService.Core.Contracts;
using GamingService.Core.Models.SessionAggregate;

namespace GamingService.Core.Abstractions;

public interface ISessionsRepository
{
    Task<Guid> CreateAsync(RouletteSession session, CancellationToken cancellationToken = default);
    
    Task<RouletteSession> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<PagedRouletteSessionsProjection> GetPagedAsync(PagedRouletteSessionsFilter filter, CancellationToken cancellationToken = default);

    Task<Guid> UpdateAsync(RouletteSession session, CancellationToken cancellationToken = default);
}
