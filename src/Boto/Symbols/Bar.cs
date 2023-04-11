namespace Boto.Symbols;

/// <summary>
/// 
/// </summary>
public static class Bar
{
    /// <summary>
    /// 
    /// </summary>
    public const string Full = "█";
    
    /// <summary>
    /// 
    /// </summary>
    public const string SevenEighths = "▇";
    
    /// <summary>
    /// 
    /// </summary>
    public const string ThreeQuarters = "▆";
    
    /// <summary>
    /// 
    /// </summary>
    public const string FiveEighths = "▅";
    
    /// <summary>
    /// 
    /// </summary>
    public const string Half = "▄";
    
    /// <summary>
    /// 
    /// </summary>
    public const string ThreeEighths = "▃";
    
    /// <summary>
    /// 
    /// </summary>
    public const string OneQuarter = "▂";
    
    /// <summary>
    /// 
    /// </summary>
    public const string OneEighth = "▁";

    
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
