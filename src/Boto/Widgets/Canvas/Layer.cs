using Boto.Styles;

namespace Boto.Widgets.Canvas;

/// <summary>
/// The grid layer.
/// </summary>
/// <param name="Content">The content.</param>
/// <param name="Colors">The <see cref="Styles.Color"/> collection.</param>
public record Layer(string Content, List<Color> Colors);
