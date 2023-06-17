namespace Boto.Symbols;

/// <summary>
/// The block symbols.
/// </summary>
public static class Block
{
    /// <summary>
    /// The full block symbol.
    /// </summary>
    public const string Full = "\u2588";

    /// <summary>
    /// The seven eighths block symbol.
    /// </summary>
    public const string SevenEighths = "\u2589";

    /// <summary>
    /// The three quarters block symbol.
    /// </summary>
    public const string ThreeQuarters = "\u258A";

    /// <summary>
    /// The five eighths block symbol.
    /// </summary>
    public const string FiveEighths = "\u258B";

    /// <summary>
    /// The half block symbol.
    /// </summary>
    public const string Half = "\u258C";

    /// <summary>
    /// The three eighths block symbol.
    /// </summary>
    public const string ThreeEighths = "\u258D";

    /// <summary>
    /// The one quarter block symbol.
    /// </summary>
    public const string OneQuarter = "\u258E";

    /// <summary>
    /// The one eighth block symbol.
    /// </summary>
    public const string OneEighth = "\u258F";

    /// <summary>
    /// The three levels block symbol set.
    /// </summary>
    public static Set ThreeLevels { get; } = new()
    {
        Full = Full,
        SevenEighths = Full,
        ThreeQuarters = Half,
        FiveEighths = Half,
        Half = Half,
        ThreeEighths = Half,
        OneQuarter = Half,
        OneEighth = " ",
        Empty = " "
    };

    /// <summary>
    /// The nine levels block symbol set.
    /// </summary>
    public static Set NineLevels { get; } = new()
    {
        Full = Full,
        SevenEighths = SevenEighths,
        ThreeQuarters = ThreeQuarters,
        FiveEighths = FiveEighths,
        Half = Half,
        ThreeEighths = ThreeEighths,
        OneQuarter = OneQuarter,
        OneEighth = OneEighth,
        Empty = " "
    };
}
