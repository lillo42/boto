namespace Boto.Symbols;

public static class Line
{
    public const string Vertical = "│";
    public const string DoubleVertical = "║";
    public const string ThickVertical = "┃";

    public const string Horizontal = "─";
    public const string DoubleHorizontal = "═";
    public const string ThickHorizontal = "━";

    public const string TopRight = "┐";
    public const string RoundedTopRight = "╮";
    public const string DoubleTopRight = "╗";
    public const string ThickTopRight = "┓";

    public const string TopLeft = "┌";
    public const string RoundedTopLeft = "╭";
    public const string DoubleTopLeft = "╔";
    public const string ThickTopLeft = "┏";

    public const string BottomRight = "┘";
    public const string RoundedBottomRight = "╯";
    public const string DoubleBottomRight = "╝";
    public const string ThickBottomRight = "┛";

    public const string BottomLeft = "└";
    public const string RoundedBottomLeft = "╰";
    public const string DoubleBottomLeft = "╚";
    public const string ThickBottomLeft = "┗";

    public const string VerticalLeft = "┤";
    public const string DoubleVerticalLeft = "╣";
    public const string ThickVerticalLeft = "┫";

    public const string VerticalRight = "├";
    public const string DoubleVerticalRight = "╠";
    public const string ThickVerticalRight = "┣";

    public const string HorizontalDown = "┬";
    public const string DoubleHorizontalDown = "╦";
    public const string ThickHorizontalDown = "┳";

    public const string HorizontalUp = "┴";
    public const string DoubleHorizontalUp = "╩";
    public const string ThickHorizontalUp = "┻";

    public const string Cross = "┼";
    public const string DoubleCross = "╬";
    public const string ThickCross = "╋";

    public record Set
    {
        public required string Vertical { get; init; }
        public required string Horizontal { get; init; }
        public required string TopRight { get; init; }
        public required string TopLeft { get; init; }
        public required string BottomRight { get; init; }
        public required string BottomLeft { get;init; }
        public required string VerticalLeft { get; init; }
        public required string VerticalRight { get; init; }
        public required string HorizontalDown { get; init; }
        public required string HorizontalUp { get; init; }
        public required string Cross { get; init; }
    }


    public static Set Normal { get; } = new()
    {
        Vertical = Vertical,
        Horizontal = Horizontal,
        TopRight = TopRight,
        TopLeft = TopLeft,
        BottomRight = BottomRight,
        BottomLeft = BottomLeft,
        VerticalLeft = VerticalLeft,
        VerticalRight = VerticalRight,
        HorizontalDown = HorizontalDown,
        HorizontalUp = HorizontalUp,
        Cross = Cross
    };

    public static Set Rounded { get; } = Normal with
    {
        TopRight = RoundedTopRight,
        TopLeft = RoundedTopLeft,
        BottomRight = RoundedBottomRight,
        BottomLeft = RoundedBottomLeft
    };
    
    public static Set Double { get; } = Normal with
    {
        Vertical = DoubleVertical,
        Horizontal = DoubleHorizontal,
        TopRight = DoubleTopRight,
        TopLeft = DoubleTopLeft,
        BottomRight = DoubleBottomRight,
        BottomLeft = DoubleBottomLeft,
        VerticalLeft = DoubleVerticalLeft,
        VerticalRight = DoubleVerticalRight,
        HorizontalDown = DoubleHorizontalDown,
        HorizontalUp = DoubleHorizontalUp,
        Cross = DoubleCross
    };
    
    public static Set Thick { get; } = Normal with
    {
        Vertical = ThickVertical,
        Horizontal = ThickHorizontal,
        TopRight = ThickTopRight,
        TopLeft = ThickTopLeft,
        BottomRight = ThickBottomRight,
        BottomLeft = ThickBottomLeft,
        VerticalLeft = ThickVerticalLeft,
        VerticalRight = ThickVerticalRight,
        HorizontalDown = ThickHorizontalDown,
        HorizontalUp = ThickHorizontalUp,
        Cross = ThickCross
    };
}
