using System.Collections.Immutable;

namespace Boto.Styles;

/// <summary>
/// Colors
/// </summary>
/// <param name="Name">The color name.</param>
/// <param name="Values">The value bytes value.</param>
public readonly record struct Color(string Name, ImmutableArray<byte> Values)
{
    /// <summary>
    /// The reset color.
    /// </summary>
    public static Color Reset { get; } = new("Reset", ImmutableArray<byte>.Empty);

    /// <summary>
    /// Black color.
    /// </summary>
    public static Color Black { get; } = new("Black", ImmutableArray<byte>.Empty);

    /// <summary>
    /// Red color.
    /// </summary>
    public static Color Red { get; } = new("Red", ImmutableArray<byte>.Empty);

    /// <summary>
    /// Green color.
    /// </summary>
    public static Color Green { get; } = new("Green", ImmutableArray<byte>.Empty);

    /// <summary>
    /// Yellow color.
    /// </summary>
    public static Color Yellow { get; } = new("Yellow", ImmutableArray<byte>.Empty);

    /// <summary>
    /// Blue color.
    /// </summary>
    public static Color Blue { get; } = new("Blue", ImmutableArray<byte>.Empty);

    /// <summary>
    /// Magenta color.
    /// </summary>
    public static Color Magenta { get; } = new("Magenta", ImmutableArray<byte>.Empty);

    /// <summary>
    /// Cyan color.
    /// </summary>
    public static Color Cyan { get; } = new("Cyan", ImmutableArray<byte>.Empty);

    /// <summary>
    /// Gray color.
    /// </summary>
    public static Color Gray { get; } = new("Gray", ImmutableArray<byte>.Empty);

    /// <summary>
    /// Dark gray color.
    /// </summary>
    public static Color DarkGray { get; } = new("DarkGray", ImmutableArray<byte>.Empty);

    /// <summary>
    /// Light red color.
    /// </summary>
    public static Color LightRed { get; } = new("LightRed", ImmutableArray<byte>.Empty);

    /// <summary>
    /// Light green color.
    /// </summary>
    public static Color LightGreen { get; } = new("LightGreen", ImmutableArray<byte>.Empty);

    /// <summary>
    /// Light yellow color.
    /// </summary>
    public static Color LightYellow { get; } = new("LightYellow", ImmutableArray<byte>.Empty);

    /// <summary>
    /// Light blue color.
    /// </summary>
    public static Color LightBlue { get; } = new("LightBlue", ImmutableArray<byte>.Empty);

    /// <summary>
    /// Light magenta color.
    /// </summary>
    public static Color LightMagenta { get; } = new("LightMagenta", ImmutableArray<byte>.Empty);

    /// <summary>
    /// Light cyan color.
    /// </summary>
    public static Color LightCyan { get; } = new("LightCyan", ImmutableArray<byte>.Empty);

    /// <summary>
    /// White color.
    /// </summary>
    public static Color White { get; } = new("White", ImmutableArray<byte>.Empty);

    /// <summary>
    /// The RGB color.
    /// </summary>
    /// <param name="r">The red value.</param>
    /// <param name="g">The green value.</param>
    /// <param name="b">The blue value.</param>
    /// <returns>The RGB <see cref="Color"/>.</returns>
    public static Color Rgb(byte r, byte g, byte b) => new("Rgb", ImmutableArray<byte>.Empty.AddRange(r, g, b));

    /// <summary>
    /// The indexed color.
    /// </summary>
    /// <param name="index">The index value.</param>
    /// <returns>The Indexed <see cref="Color"/></returns>
    public static Color Indexed(byte index) => new("Indexed", ImmutableArray<byte>.Empty.Add(index));
}
