using GamingService.Core.Constants;
using GamingService.Core.Primitives;

namespace GamingService.Core.Models.SessionAggregate;

public abstract class RouletteBetType(int value, string name)
    : Enumeration<RouletteBetType>(value, name)
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

    public abstract int Winnings { get; }

    public sealed class StraightUpBet : RouletteBetType
    {
        public StraightUpBet() : base(1, BetNames.StraightUp)
        {
        }
        public override int Winnings => 36;
    }

    public sealed class SplitBet : RouletteBetType
    {
        public SplitBet() : base(2, BetNames.Split)
        {
        }
        public override int Winnings => 18;
    }

    public sealed class StreetBet : RouletteBetType
    {
        public StreetBet() : base(3, BetNames.Street)
        {
        }
        public override int Winnings => 12;
    }

    public sealed class CornerBet : RouletteBetType
    {
        public CornerBet() : base(4, BetNames.Corner)
        {
        }
        public override int Winnings => 9;
    }

    public sealed class BasketBet : RouletteBetType
    {
        public BasketBet() : base(13, BetNames.Basket)
        {
        }
        public override int Winnings => 7;
    }

    public sealed class LineBet : RouletteBetType
    {
        public LineBet() : base(5, BetNames.Line)
        {
        }
        public override int Winnings => 6;
    }

    public sealed class ColumnBet : RouletteBetType
    {
        public ColumnBet() : base(6, BetNames.Column)
        {
        }
        public override int Winnings => 3;
    }

    public sealed class DozenBet : RouletteBetType
    {
        public DozenBet() : base(7, BetNames.Dozen)
        {
        }
        public override int Winnings => 3;
    }

    public sealed class RedBet : RouletteBetType
    {
        public RedBet() : base(8, BetNames.Red)
        {
        }
        public override int Winnings => 2;
    }

    public sealed class BlackBet : RouletteBetType
    {
        public BlackBet() : base(8, BetNames.Black)
        {
        }
        public override int Winnings => 2;
    }

    public sealed class EvenBet : RouletteBetType
    {
        public EvenBet() : base(9, BetNames.Even)
        {
        }
        public override int Winnings => 2;
    }

    public sealed class OddBet : RouletteBetType
    {
        public OddBet() : base(10, BetNames.Odd)
        {
        }
        public override int Winnings => 2;
    }

    public sealed class LowBet : RouletteBetType
    {
        public LowBet() : base(11, BetNames.Low)
        {
        }
        public override int Winnings => 2;
    }

    public sealed class HighBet : RouletteBetType
    {
        public HighBet() : base(12, BetNames.High)
        {
        }
        public override int Winnings => 2;
    }
}