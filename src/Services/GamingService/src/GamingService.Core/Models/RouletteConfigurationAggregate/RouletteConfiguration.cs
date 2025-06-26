using GamingService.Core.Common;
using GamingService.Core.Primitives;
using System.Security.Cryptography;

namespace GamingService.Core.Models.RouletteConfigurationAggregate;

public class RouletteConfiguration : Entity
{
    private RouletteConfiguration(
        Id id,
        RouletteGameType rouletteGameType,
        Currency currency,
        Amount minBet,
        Amount maxBet,
        HashAlgorithm? engine) : base(id)
    {
        RouletteGameType = rouletteGameType;
        Currency = currency;
        MinBet = minBet;
        MaxBet = maxBet;
        Engine = engine ?? SHA256.Create();
    }

    public static RouletteConfiguration Create(
        Id id,
        RouletteGameType rouletteGameType,
        Currency currency,
        Amount minBet,
        Amount maxBet,
        HashAlgorithm? engine)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(maxBet.Value, minBet.Value);

        return new RouletteConfiguration(id, rouletteGameType, currency, minBet, maxBet, engine);
    }

    public RouletteGameType RouletteGameType { get; }
    
    public Currency Currency { get; }

    public Amount MinBet { get;}

    public Amount MaxBet { get; }

    public HashAlgorithm Engine { get; } 

    public bool IsActive { get; set; } = true;
}
