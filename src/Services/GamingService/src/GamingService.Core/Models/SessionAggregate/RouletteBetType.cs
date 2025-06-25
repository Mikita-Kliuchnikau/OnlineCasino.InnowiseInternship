using GamingService.Core.Primitives;

namespace GamingService.Core.Models.SessionAggregate;

public abstract class RouletteBetType(int value, BetType type)
    : Enumeration<RouletteBetType>(value, type.ToString())
{
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

    public sealed class StraightUpBet : RouletteBetType
    {
        public StraightUpBet() : base(1, BetType.StraightUp)
        {
        }
        public override int WinningsMultiplier => 36;
        public override int NumberOfValues => 1;
    }

    public sealed class SplitBet : RouletteBetType
    {
        public SplitBet() : base(2, BetType.Split)
        {
        }
        public override int WinningsMultiplier => 18;
        public override int NumberOfValues => 2;
    }

    public sealed class StreetBet : RouletteBetType
    {
        public StreetBet() : base(3, BetType.Street)
        {
        }
        public override int WinningsMultiplier => 12;
        public override int NumberOfValues => 3;
    }

    public sealed class CornerBet : RouletteBetType
    {
        public CornerBet() : base(4, BetType.Corner)
        {
        }
        public override int WinningsMultiplier => 9;
        public override int NumberOfValues => 4;
    }

    public sealed class BasketBet : RouletteBetType
    {
        public BasketBet() : base(13, BetType.Basket)
        {
        }
        public override int WinningsMultiplier => 7;
        public override int NumberOfValues => 5;
    }

    public sealed class LineBet : RouletteBetType
    {
        public LineBet() : base(5, BetType.Line)
        {
        }
        public override int WinningsMultiplier => 6;
        public override int NumberOfValues => 6;
    }

    public sealed class ColumnBet : RouletteBetType
    {
        public ColumnBet() : base(6, BetType.Column)
        {
        }
        public override int WinningsMultiplier => 3;
        public override int NumberOfValues => 12;
    }

    public sealed class DozenBet : RouletteBetType
    {
        public DozenBet() : base(7, BetType.Dozen)
        {
        }
        public override int WinningsMultiplier => 3;
        public override int NumberOfValues => 12;
    }

    public sealed class RedBet : RouletteBetType
    {
        public RedBet() : base(8, BetType.Red)
        {
        }
        public override int WinningsMultiplier => 2;
        public override int NumberOfValues => 18;
    }

    public sealed class BlackBet : RouletteBetType
    {
        public BlackBet() : base(8, BetType.Black)
        {
        }
        public override int WinningsMultiplier => 2;
        public override int NumberOfValues => 18;
    }

    public sealed class EvenBet : RouletteBetType
    {
        public EvenBet() : base(9, BetType.Even)
        {
        }
        public override int WinningsMultiplier => 2;
        public override int NumberOfValues => 18;
    }

    public sealed class OddBet : RouletteBetType
    {
        public OddBet() : base(10, BetType.Odd)
        {
        }
        public override int WinningsMultiplier => 2;
        public override int NumberOfValues => 18;
    }

    public sealed class LowBet : RouletteBetType
    {
        public LowBet() : base(11, BetType.Low)
        {
        }
        public override int WinningsMultiplier => 2;
        public override int NumberOfValues => 18;
    }

    public sealed class HighBet : RouletteBetType
    {
        public HighBet() : base(12, BetType.High)
        {
        }
        public override int WinningsMultiplier => 2;
        public override int NumberOfValues => 18;
    }
}