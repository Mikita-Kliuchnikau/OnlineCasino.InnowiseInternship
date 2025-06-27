using GamingService.Core.Common;
using GamingService.Core.Primitives;

namespace GamingService.Core.Models.RoulettePlayerAggregate;

public class RoulettePlayer(Id id, Money money) : Entity(id)
{
    public Money Balance { get; set; } = money;
}
