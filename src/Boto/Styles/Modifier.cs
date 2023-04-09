namespace Boto.Styles;

/// <summary>
/// Modifier changes the way of piece of text is displayed
/// </summary>
/// <remarks>
/// They are flags, so they can easily be composed.
/// </remarks>
/// <example>
/// <code>
/// var m = Modifier.BOLD | Modifier.ITALIC;
/// </code>
/// </example>
[Flags]
public enum Modifier
{
    /// <summary>
    /// No modifier.
    /// </summary>
    Empty = 0,

    /// <summary>
    /// Bold modifier.
    /// </summary>
    Bold = 0b0000_0000_0001,

    /// <summary>
    /// Dim modifier.
    /// </summary>
    Dim = 0b0000_0000_0010,

    /// <summary>
    /// Italic modifier.
    /// </summary>
    Italic = 0b0000_0000_0100,

    /// <summary>
    /// Underlined modifier.
    /// </summary>
    Underlined = 0b0000_0000_1000,

    /// <summary>
    /// Slow blink modifier.
    /// </summary>
    SlowBlink = 0b0000_0001_0000,
    
    /// <summary>
    /// Rapid blink modifier.
    /// </summary>
    RapidBlink = 0b0000_0010_0000,
    
    /// <summary>
    /// Reversed modifier.
    /// </summary>
    Reversed = 0b0000_0100_0000,
    
    /// <summary>
    /// Hidden modifier.
    /// </summary>
    Hidden = 0b0000_1000_0000,
    
    /// <summary>
    /// Crossed out modifier.
    /// </summary>
    CrossedOut = 0b0001_0000_0000,
    
    /// <summary>
    /// All modifiers.
    /// </summary>
    All = Bold | Dim | Italic | Underlined | SlowBlink | RapidBlink | Reversed | Hidden | CrossedOut,
}
