using GamingService.Core.Common;

namespace GamingService.Core.Models.RoulettePlayerAggregate;

public class RoulettePlayer(Guid id, Money money)
{
    public Guid Id { get; } = id;
    public Money Balance { get; set; } = money;
}
