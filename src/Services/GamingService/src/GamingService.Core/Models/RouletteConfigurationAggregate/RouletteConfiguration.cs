using GamingService.Core.Common;
using GamingService.Core.Primitives;
using System.Security.Cryptography;

namespace GamingService.Core.Models.RouletteConfigurationAggregate;

public class RouletteConfiguration : Entity
{
    private RouletteConfiguration(
        RouletteGameType rouletteGameType,
        Currency currency,
        Amount minBet,
        Amount maxBet,
        HashAlgorithm hashAlgorithm,
        Guid? id) : base(id)
    {
        RouletteGameType = rouletteGameType;
        Currency = currency;
        MinBet = minBet;
        MaxBet = maxBet;
        HashAlgorithm = hashAlgorithm;
    }

    public static RouletteConfiguration Create(
        RouletteGameType rouletteGameType,
        Currency currency,
        Amount minBet,
        Amount maxBet,
        string hashAlgorithmName,
        Guid? id = null)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(maxBet.Value, minBet.Value);

        var hashAlgorithm = HashAlgorithm.Create(hashAlgorithmName) ?? SHA256.Create();

        return new RouletteConfiguration(rouletteGameType, currency, minBet, maxBet, hashAlgorithm, id);
    }

    public RouletteGameType RouletteGameType { get; }
    
    public Currency Currency { get; }

    public Amount MinBet { get;}

    public Amount MaxBet { get; }

    public HashAlgorithm HashAlgorithm { get; } 

    public bool IsActive { get; private set; } = true;
}
