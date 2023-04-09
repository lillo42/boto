namespace Boto.Symbols;

/// <summary>
/// Marker to use when plotting data points
/// </summary>
public enum Marker
{
    /// <summary>
    /// One point per cell in shape of dot
    /// </summary>
    Dot,

    /// <summary>
    /// One point per cell in shape of a block
    /// </summary>
    Block,

    /// <summary>
    /// Up to 8 points per cell
    /// </summary>
    Braille,
}
