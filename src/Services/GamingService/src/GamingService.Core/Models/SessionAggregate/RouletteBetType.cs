using GamingService.Core.Primitives;

namespace GamingService.Core.Models.SessionAggregate;

public abstract class RouletteBetType(int value, BetType type)
    : Enumeration<RouletteBetType>(value, type.ToString())
{
    public static readonly RouletteBetType Default = new DefaultBet();
    public static readonly RouletteBetType StraightUp = new StraightUpBet();
    public static readonly RouletteBetType Split = new SplitBet();
    public static readonly RouletteBetType Street = new StreetBet();
    public static readonly RouletteBetType Corner = new CornerBet();
    public static readonly RouletteBetType Line = new LineBet();
    public static readonly RouletteBetType Column = new ColumnBet();
    public static readonly RouletteBetType Dozen = new DozenBet();
    public static readonly RouletteBetType Red = new RedBet();
    public static readonly RouletteBetType Black = new BlackBet();
    public static readonly RouletteBetType Even = new EvenBet();
    public static readonly RouletteBetType Odd = new OddBet();
    public static readonly RouletteBetType Low = new LowBet();
    public static readonly RouletteBetType High = new HighBet();
    public static readonly RouletteBetType Basket = new BasketBet();

    public abstract int WinningsMultiplier { get; }

    public abstract int NumberOfValues { get; }

    public sealed class DefaultBet : RouletteBetType
    {
        public DefaultBet() : base((int)BetType.Default, BetType.Default)
        {
        }
        public override int WinningsMultiplier => 0;
        public override int NumberOfValues => 0;
    }

    public sealed class StraightUpBet : RouletteBetType
    {
        public StraightUpBet() : base((int)BetType.StraightUp, BetType.StraightUp)
        {
        }
        public override int WinningsMultiplier => 36;
        public override int NumberOfValues => 1;
    }

    public sealed class SplitBet : RouletteBetType
    {
        public SplitBet() : base((int)BetType.Split, BetType.Split)
        {
        }
        public override int WinningsMultiplier => 18;
        public override int NumberOfValues => 2;
    }

    public sealed class StreetBet : RouletteBetType
    {
        public StreetBet() : base((int)BetType.Street, BetType.Street)
        {
        }
        public override int WinningsMultiplier => 12;
        public override int NumberOfValues => 3;
    }

    public sealed class CornerBet : RouletteBetType
    {
        public CornerBet() : base((int)BetType.Corner, BetType.Corner)
        {
        }
        public override int WinningsMultiplier => 9;
        public override int NumberOfValues => 4;
    }

    public sealed class BasketBet : RouletteBetType
    {
        public BasketBet() : base((int)BetType.Basket, BetType.Basket)
        {
        }
        public override int WinningsMultiplier => 7;
        public override int NumberOfValues => 5;
    }

    public sealed class LineBet : RouletteBetType
    {
        public LineBet() : base((int)BetType.Line, BetType.Line)
        {
        }
        public override int WinningsMultiplier => 6;
        public override int NumberOfValues => 6;
    }

    public sealed class ColumnBet : RouletteBetType
    {
        public ColumnBet() : base((int)BetType.Column, BetType.Column)
        {
        }
        public override int WinningsMultiplier => 3;
        public override int NumberOfValues => 12;
    }

    public sealed class DozenBet : RouletteBetType
    {
        public DozenBet() : base((int)BetType.Dozen, BetType.Dozen)
        {
        }
        public override int WinningsMultiplier => 3;
        public override int NumberOfValues => 12;
    }

    public sealed class RedBet : RouletteBetType
    {
        public RedBet() : base((int)BetType.Red, BetType.Red)
        {
        }
        public override int WinningsMultiplier => 2;
        public override int NumberOfValues => 18;
    }

    public sealed class BlackBet : RouletteBetType
    {
        public BlackBet() : base((int)BetType.Black, BetType.Black)
        {
        }
        public override int WinningsMultiplier => 2;
        public override int NumberOfValues => 18;
    }

    public sealed class EvenBet : RouletteBetType
    {
        public EvenBet() : base((int)BetType.Even, BetType.Even)
        {
        }
        public override int WinningsMultiplier => 2;
        public override int NumberOfValues => 18;
    }

    public sealed class OddBet : RouletteBetType
    {
        public OddBet() : base((int)BetType.Odd, BetType.Odd)
        {
        }
        public override int WinningsMultiplier => 2;
        public override int NumberOfValues => 18;
    }

    public sealed class LowBet : RouletteBetType
    {
        public LowBet() : base((int)BetType.Low, BetType.Low)
        {
        }
        public override int WinningsMultiplier => 2;
        public override int NumberOfValues => 18;
    }

    public sealed class HighBet : RouletteBetType
    {
        public HighBet() : base((int)BetType.High, BetType.High)
        {
        }
        public override int WinningsMultiplier => 2;
        public override int NumberOfValues => 18;
    }
}