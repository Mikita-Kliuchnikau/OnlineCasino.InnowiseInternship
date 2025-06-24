using GamingService.Core.Common;
using GamingService.Core.Primitives;

namespace GamingService.Core.Models.RouletteConfigurationAggregate;

public class RouletteConfiguration : Entity
{
    private RouletteConfiguration(
        string name,
        string description,
        RouletteGameType rouletteGameType,
        Currency currency,
        Amount minBet,
        Amount maxBet) : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
        RouletteGameType = rouletteGameType;
        Currency = currency;
        MinBet = minBet;
        MaxBet = maxBet;
    }

    public static RouletteConfiguration Create(
        string name,
        string description,
        RouletteGameType rouletteGameType,
        Currency currency,
        Amount minBet,
        Amount maxBet)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        ArgumentOutOfRangeException.ThrowIfLessThan(maxBet.Value, minBet.Value);

        return new RouletteConfiguration(name, description, rouletteGameType, currency, minBet, maxBet);
    }
    public string Name { get; }

    public string Description { get; }

    public RouletteGameType RouletteGameType { get; }
    
    public Currency Currency { get; }

    public Amount MinBet { get; private set; }

    public Amount MaxBet { get; private set; }

    public bool IsActive { get; set; } = true;
}
