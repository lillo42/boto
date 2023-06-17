namespace Boto.Symbols;

/// <summary>
/// The set symbols.
/// </summary>
public record Set
{
    /// <summary>
    /// The full symbol. 
    /// </summary>
    public required string Full { get; init; }
    
    /// <summary>
    /// The seven eighths symbol.
    /// </summary>
    public required string SevenEighths { get; init; }
    
    /// <summary>
    /// The three quarters symbol.
    /// </summary>
    public required string ThreeQuarters { get; init; }
    
    /// <summary>
    /// The five eighths symbol.
    /// </summary>
    public required string FiveEighths { get; init; }
    
    /// <summary>
    /// The half symbol.
    /// </summary>
    public required string Half { get; init; }
    
    /// <summary>
    /// The three eighths symbol.
    /// </summary>
    public required string ThreeEighths { get; init; }
    
    /// <summary>
    /// The one quarter symbol.
    /// </summary>
    public required string OneQuarter { get; init; }
    
    /// <summary>
    /// The one eighth symbol.
    /// </summary>
    public required string OneEighth { get; init; }
    
    /// <summary>
    /// The empty symbol.
    /// </summary>
    public required string Empty { get; init; }
}
