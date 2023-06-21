using Boto.Styles;

namespace Boto.Buffers;

/// <summary>
/// Buffer cell
/// </summary>
public readonly record struct Cell(string Symbol, Color Foreground, Color BackgroundColor, Color UnderlineColor, Modifier Modifier)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> struct.
    /// </summary>
    public Cell()
        : this(" ", Color.Reset, Color.Reset, Color.Reset, Modifier.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> struct.
    /// </summary>
    /// <param name="other">The other <see cref="Cell"/> to copy some information.</param>
    /// <param name="style">The <see cref="Styles.Style"/>.</param>
    public Cell(Cell other, Style style)
        : this(other.Symbol, 
            style.Foreground ?? other.Foreground, 
            style.Background ?? other.BackgroundColor, 
            style.Underline ?? other.UnderlineColor,
            style.AddModifier | (other.Modifier & ~style.RemoveModifier))
    {
    }

    /// <summary>
    /// Create a new <see cref="Cell"/> with the <paramref name="style"/>.
    /// </summary>
    /// <param name="style">The other <see cref="Styles.Style"/></param>
    /// <returns>A new <see cref="Cell"/> base on current and the <paramref name="style"/>.</returns>
    public Cell With(Style style)
    {
        var foreground = style.Foreground ?? Foreground;
        var background = style.Background ?? BackgroundColor;

        var modifier = Modifier;
        modifier |= style.AddModifier;
        modifier &= ~style.RemoveModifier;

        return this with { Foreground = foreground, BackgroundColor = background, Modifier = modifier };
    }

    /// <summary>
    /// The <see cref="Boto.Styles.Style"/>.
    /// </summary>
    public Style Style => new(Foreground, BackgroundColor, UnderlineColor, Modifier, Modifier.Empty);
}
