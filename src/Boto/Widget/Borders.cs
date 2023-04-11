namespace Boto.Widget;

/// <summary>
/// Borders of a block.
/// </summary>
[Flags]
public enum Borders
{
    /// <summary>
    /// None borders.
    /// </summary>
    None = 0b0000_0001,

    /// <summary>
    /// Top border.
    /// </summary>
    Top = 0b0000_0010,

    /// <summary>
    /// Right border.
    /// </summary>
    Right = 0b0000_0100,

    /// <summary>
    /// Bottom border.
    /// </summary>
    Bottom = 0b0000_1000,

    /// <summary>
    /// Left border.
    /// </summary>
    Left = 0b0001_0000,

    /// <summary>
    /// Top, right, bottom and left borders.
    /// </summary>
    All = Top | Right | Bottom | Left
}
