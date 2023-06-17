namespace Boto.Symbols;

/// <summary>
/// The line symbols.
/// </summary>
public static class Line
{
    /// <summary>
    /// The vertical line symbol.
    /// </summary>
    public const string Vertical = "│";

    /// <summary>
    /// The double vertical line symbol.
    /// </summary>
    public const string DoubleVertical = "║";

    /// <summary>
    /// The thick vertical line symbol.
    /// </summary>
    public const string ThickVertical = "┃";


    /// <summary>
    /// The horizontal line symbol.
    /// </summary>
    public const string Horizontal = "─";

    /// <summary>
    /// The double horizontal line symbol.
    /// </summary>
    public const string DoubleHorizontal = "═";

    /// <summary>
    /// The thick horizontal line symbol.
    /// </summary>
    public const string ThickHorizontal = "━";


    /// <summary>
    /// The top right corner symbol.
    /// </summary>
    public const string TopRight = "┐";

    /// <summary>
    /// The rounded top right corner symbol.
    /// </summary>
    public const string RoundedTopRight = "╮";

    /// <summary>
    /// The double top right corner symbol.
    /// </summary>
    public const string DoubleTopRight = "╗";

    /// <summary>
    /// The thick top right corner symbol.
    /// </summary>
    public const string ThickTopRight = "┓";


    /// <summary>
    /// The top left corner symbol.
    /// </summary>
    public const string TopLeft = "┌";

    /// <summary>
    /// The rounded top left corner symbol.
    /// </summary>
    public const string RoundedTopLeft = "╭";

    /// <summary>
    /// The double top left corner symbol.
    /// </summary>
    public const string DoubleTopLeft = "╔";

    /// <summary>
    /// The thick top left corner symbol.
    /// </summary>
    public const string ThickTopLeft = "┏";


    /// <summary>
    /// The bottom right corner symbol.
    /// </summary>
    public const string BottomRight = "┘";

    /// <summary>
    /// The rounded bottom right corner symbol.
    /// </summary>
    public const string RoundedBottomRight = "╯";

    /// <summary>
    /// The double bottom right corner symbol.
    /// </summary>
    public const string DoubleBottomRight = "╝";

    /// <summary>
    /// The thick bottom right corner symbol.
    /// </summary>
    public const string ThickBottomRight = "┛";


    /// <summary>
    /// The bottom left corner symbol.
    /// </summary>
    public const string BottomLeft = "└";

    /// <summary>
    /// The rounded bottom left corner symbol.
    /// </summary>
    public const string RoundedBottomLeft = "╰";

    /// <summary>
    /// The double bottom left corner symbol.
    /// </summary>
    public const string DoubleBottomLeft = "╚";

    /// <summary>
    /// The thick bottom left corner symbol.
    /// </summary>
    public const string ThickBottomLeft = "┗";


    /// <summary>
    /// The vertical left symbol.
    /// </summary>
    public const string VerticalLeft = "┤";

    /// <summary>
    /// The double vertical left symbol.
    /// </summary>
    public const string DoubleVerticalLeft = "╣";

    /// <summary>
    /// The thick vertical left symbol.
    /// </summary>
    public const string ThickVerticalLeft = "┫";


    /// <summary>
    /// The vertical right symbol.
    /// </summary>
    public const string VerticalRight = "├";

    /// <summary>
    /// The double vertical right symbol.
    /// </summary>
    public const string DoubleVerticalRight = "╠";

    /// <summary>
    /// The thick vertical right symbol.
    /// </summary>
    public const string ThickVerticalRight = "┣";


    /// <summary>
    /// The horizontal down symbol.
    /// </summary>
    public const string HorizontalDown = "┬";

    /// <summary>
    /// The double horizontal down symbol.
    /// </summary>
    public const string DoubleHorizontalDown = "╦";

    /// <summary>
    /// The thick horizontal down symbol.
    /// </summary>
    public const string ThickHorizontalDown = "┳";


    /// <summary>
    /// The horizontal up symbol.
    /// </summary>
    public const string HorizontalUp = "┴";

    /// <summary>
    /// The double horizontal up symbol.
    /// </summary>
    public const string DoubleHorizontalUp = "╩";

    /// <summary>
    /// The thick horizontal up symbol.
    /// </summary>
    public const string ThickHorizontalUp = "┻";


    /// <summary>
    /// The cross symbol.
    /// </summary>
    public const string Cross = "┼";

    /// <summary>
    /// The double cross symbol.
    /// </summary>
    public const string DoubleCross = "╬";

    /// <summary>
    /// The thick cross symbol.
    /// </summary>
    public const string ThickCross = "╋";

    /// <summary>
    /// The set of line symbols.
    /// </summary>
    public record Set
    {
        /// <summary>
        /// The vertical line symbol.
        /// </summary>
        public required string Vertical { get; init; }

        /// <summary>
        /// The horizontal line symbol.
        /// </summary>
        public required string Horizontal { get; init; }

        /// <summary>
        /// The top right corner symbol.
        /// </summary>
        public required string TopRight { get; init; }

        /// <summary>
        /// The top left corner symbol.
        /// </summary>
        public required string TopLeft { get; init; }

        /// <summary>
        /// The bottom right corner symbol.
        /// </summary>
        public required string BottomRight { get; init; }

        /// <summary>
        /// The bottom left corner symbol.
        /// </summary>
        public required string BottomLeft { get; init; }

        /// <summary>
        /// The vertical left symbol.
        /// </summary>
        public required string VerticalLeft { get; init; }

        /// <summary>
        /// The vertical right symbol.
        /// </summary>
        public required string VerticalRight { get; init; }

        /// <summary>
        /// The horizontal down symbol.
        /// </summary>
        public required string HorizontalDown { get; init; }

        /// <summary>
        /// The horizontal up symbol.
        /// </summary>
        public required string HorizontalUp { get; init; }

        /// <summary>
        /// The cross symbol.
        /// </summary>
        public required string Cross { get; init; }
    }


    /// <summary>
    /// The normal set of line symbols.
    /// </summary>
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

    /// <summary>
    /// The rounded set of line symbols.
    /// </summary>
    public static Set Rounded { get; } = Normal with
    {
        TopRight = RoundedTopRight,
        TopLeft = RoundedTopLeft,
        BottomRight = RoundedBottomRight,
        BottomLeft = RoundedBottomLeft
    };

    /// <summary>
    /// The double set of line symbols.
    /// </summary>
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

    /// <summary>
    /// The thick set of line symbols.
    /// </summary>
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
