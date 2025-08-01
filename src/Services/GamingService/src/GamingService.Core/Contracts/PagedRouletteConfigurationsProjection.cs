using GamingService.Core.Models.RouletteConfigurationAggregate;

namespace GamingService.Core.Contracts;

public record PagedRouletteConfigurationsProjection(
    IReadOnlyList<RouletteConfiguration>? Configurations,
    int TotalCount,
    int PageNumber,
    int PageSize);
