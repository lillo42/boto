using Boto.Symbols;

namespace Boto.Widgets;

/// <summary>
/// Border type.
/// </summary>
public enum BorderType
{
    /// <summary>
    /// Plain border.
    /// </summary>
    Plain,

    /// <summary>
    /// Rounded border.
    /// </summary>
    Rounded,

    /// <summary>
    /// Double border.
    /// </summary>
    Double,

    /// <summary>
    /// Thick border.
    /// </summary>
    Thick,
}

/// <summary>
/// The <see cref="Borders"/> extensions methods.
/// </summary>
public static class BorderTypeExtensions
{
    /// <summary>
    /// Convert the <see cref="BorderType"/> to <see cref="Line.Set"/>.
    /// </summary>
    /// <param name="type">The <see cref="BorderType"/>.</param>
    /// <returns>The <see cref="Line.Set"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static Line.Set LineSymbol(this BorderType type)
        => type switch
        {
            BorderType.Plain => Line.Normal,
            BorderType.Rounded => Line.Rounded,
            BorderType.Double => Line.Double,
            BorderType.Thick => Line.Thick,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
}
