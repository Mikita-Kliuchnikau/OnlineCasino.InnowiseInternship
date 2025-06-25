using GamingService.Core.Primitives;

namespace GamingService.Core.Models.RouletteConfigurationAggregate;

public abstract class RouletteGameType(int value, RouletteType type)
    : Enumeration<RouletteGameType>(value, type.ToString())
{
    public static readonly EuropeanRoulette European = new();
    public static readonly AmericanRoulette American = new();

    public abstract int NumberOfPossibleBets { get; }

    public sealed class EuropeanRoulette : RouletteGameType
    {
        public EuropeanRoulette() : base(1, RouletteType.EuropeanRoulette)
        {
        }
        public override int NumberOfPossibleBets => 37;
    }

    public sealed class AmericanRoulette : RouletteGameType
    {
        public AmericanRoulette() : base(2, RouletteType.AmericanRoulette)
        {
        }
        public override int NumberOfPossibleBets => 38;
    }
}
