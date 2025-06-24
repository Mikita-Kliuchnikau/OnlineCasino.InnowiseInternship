using GamingService.Core.Models.SessionAggregate;

namespace GamingService.Core.Contracts;

public record PagedRouletteSessionsProjection(
    IReadOnlyList<RouletteSession>? Sessions,
    int TotalCount,
    int PageNumber,
    int PageSize);
