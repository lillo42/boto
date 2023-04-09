using Boto.Styles;

namespace Boto.Buffers;

/// <summary>
/// Buffer cell
/// </summary>
public readonly record struct Cell(string Symbol, Color ForegroundColor, Color BackgroundColor, Modifier Modifier)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> struct.
    /// </summary>
    public Cell()
        : this(" ", Color.Reset, Color.Reset, Modifier.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> struct.
    /// </summary>
    /// <param name="other">The other <see cref="Cell"/> to copy some information.</param>
    /// <param name="style">The <see cref="Styles.Style"/>.</param>
    public Cell(Cell other, Style style)
        : this(other.Symbol, 
            style.Foreground ?? other.ForegroundColor, 
            style.Background ?? other.BackgroundColor, 
            style.AddModifier | (other.Modifier & ~style.RemoveModifier))
    {
    }

    public Cell With(Style style)
    {
        var foreground = style.Foreground ?? ForegroundColor;
        var background = style.Background ?? BackgroundColor;

        var modifier = Modifier;
        modifier |= style.AddModifier;
        modifier &= ~style.RemoveModifier;

        return this with { ForegroundColor = foreground, BackgroundColor = background, Modifier = modifier };
    }

    /// <summary>
    /// The <see cref="Boto.Styles.Style"/>.
    /// </summary>
    public Style Style => new(ForegroundColor, BackgroundColor, Modifier, Modifier.Empty);
}
