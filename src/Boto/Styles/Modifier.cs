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
    Bold = 1,

    /// <summary>
    /// Dim modifier.
    /// </summary>
    Dim = 2,

    /// <summary>
    /// Italic modifier.
    /// </summary>
    Italic = 4,

    /// <summary>
    /// Underlined modifier.
    /// </summary>
    Underlined = 8,

    /// <summary>
    /// Slow blink modifier.
    /// </summary>
    SlowBlink = 16,

    /// <summary>
    /// Rapid blink modifier.
    /// </summary>
    RapidBlink = 32,

    /// <summary>
    /// Reversed modifier.
    /// </summary>
    Reversed = 64,

    /// <summary>
    /// Hidden modifier.
    /// </summary>
    Hidden = 128,

    /// <summary>
    /// Crossed out modifier.
    /// </summary>
    CrossedOut = 256,

    /// <summary>
    /// The double underlined modifier.
    /// </summary>
    DoubleUnderlined = 512,

    /// <summary>
    /// The curly underlined modifier.
    /// </summary>
    UnderCurled = 1024,

    /// <summary>
    /// The dotted underlined modifier.
    /// </summary>
    UnderDotted = 2048,

    /// <summary>
    /// The dashed underlined modifier.
    /// </summary>
    UnderDashed = 4096,

    /// <summary>
    /// The framed modifier.
    /// </summary>
    Frame = 8192,

    /// <summary>
    /// The encircled modifier.
    /// </summary>
    Encircle = 16384,
    
    /// <summary>
    /// The over lined modifier.
    /// </summary>
    OverLined = 32768,

    /// <summary>
    /// All modifiers.
    /// </summary>
    All = Bold 
          | Dim 
          | Italic
          | Underlined
          | SlowBlink
          | RapidBlink
          | Reversed
          | Hidden
          | CrossedOut
          | DoubleUnderlined
          | UnderCurled
          | UnderDotted
          | UnderDashed
          | Frame
          | Encircle
          | OverLined
}
