namespace Boto.Symbols;

/// <summary>
/// The bar symbols. 
/// </summary>
public static class Bar
{
    /// <summary>
    /// The full bar symbol.
    /// </summary>
    public const string Full = "█";
    
    /// <summary>
    /// The seven eighths bar symbol.
    /// </summary>
    public const string SevenEighths = "▇";
    
    /// <summary>
    /// The three quarters bar symbol.
    /// </summary>
    public const string ThreeQuarters = "▆";
    
    /// <summary>
    /// The five eighths bar symbol.
    /// </summary>
    public const string FiveEighths = "▅";
    
    /// <summary>
    /// The half bar symbol. 
    /// </summary>
    public const string Half = "▄";
    
    /// <summary>
    /// The three eighths bar symbol.
    /// </summary>
    public const string ThreeEighths = "▃";
    
    /// <summary>
    /// The one quarter bar symbol.
    /// </summary>
    public const string OneQuarter = "▂";
    
    /// <summary>
    /// The one eighth bar symbol.
    /// </summary>
    public const string OneEighth = "▁";

    
    /// <summary>
    /// The three levels bar symbol set.
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
    /// The nine levels bar symbol set.
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
