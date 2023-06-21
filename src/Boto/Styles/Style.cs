namespace Boto.Styles;

/// <summary>
/// Style let you control the main characteristics of the displayed elements.
/// </summary>
/// <param name="Foreground">The foreground <see cref="Color"/>.</param>
/// <param name="Background">The background <see cref="Color"/>.</param>
/// <param name="Underline">The underline <see cref="Color"/>.</param>
/// <param name="AddModifier">The added <see cref="Modifier"/>.</param>
/// <param name="RemoveModifier">The removed <see cref="Modifier"/>.</param>
public readonly record struct Style(Color? Foreground,
    Color? Background,
    Color? Underline,
    Modifier AddModifier,
    Modifier RemoveModifier)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Style"/> struct.
    /// </summary>
    public Style()
        : this(null, null, null, Modifier.Empty, Modifier.Empty)
    {

    }

    /// <summary>
    /// Results in a combined style that is equivalent to applying the two individual styles to
    /// a style one after the other.
    /// </summary>
    /// <param name="other">The other <see cref="Style"/>.</param>
    /// <returns>New <see cref="Style"/> combine with current and <paramref name="other"/>.</returns>
    public Style Merge(Style other)
        => new(other.Foreground ?? Foreground,
            other.Background ?? Background,
            other.Underline ?? Underline,
            AddModifier | other.AddModifier,
            RemoveModifier | other.RemoveModifier);

    /// <summary>
    /// The Style with no color and no modifier.
    /// </summary>
    public static Style Reset { get; } = new(Color.Reset, Color.Reset, Color.Reset, Modifier.Empty, Modifier.All);
}
