using Boto.Texts;

namespace Boto.Widgets.Canvas;

/// <summary>
/// The grid label.
/// </summary>
/// <param name="X">The x.</param>
/// <param name="Y">The y.</param>
/// <param name="Spans">The <see cref="Texts.Spans"/>.</param>
public record Label(double X, double Y, Spans Spans);
