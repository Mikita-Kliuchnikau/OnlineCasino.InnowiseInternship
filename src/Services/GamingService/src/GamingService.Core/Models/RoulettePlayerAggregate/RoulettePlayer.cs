using GamingService.Core.Common;
using GamingService.Core.Primitives;

namespace GamingService.Core.Models.RoulettePlayerAggregate;

public class RoulettePlayer(string id, Money money) : Entity(id)
{
    public Money Balance { get; set; } = money;
}
