namespace Boto.Symbols;

/// <summary>
/// 
/// </summary>
public static class Block
{
    /// <summary>
    /// 
    /// </summary>
    public const string Full = "\u2588";

    /// <summary>
    /// 
    /// </summary>
    public const string SevenEighths = "\u2589";

    /// <summary>
    /// 
    /// </summary>
    public const string ThreeQuarters = "\u258A";

    /// <summary>
    /// 
    /// </summary>
    public const string FiveEighths = "\u258B";

    /// <summary>
    /// 
    /// </summary>
    public const string Half = "\u258C";

    /// <summary>
    /// 
    /// </summary>
    public const string ThreeEighths = "\u258D";

    /// <summary>
    /// 
    /// </summary>
    public const string OneQuarter = "\u258E";

    /// <summary>
    /// 
    /// </summary>
    public const string OneEighth = "\u258F";

    /// <summary>
    /// 
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
    /// 
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
