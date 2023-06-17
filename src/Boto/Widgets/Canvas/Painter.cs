using Boto.Styles;

namespace Boto.Widgets.Canvas;

/// <summary>
/// The canvas painter.
/// </summary>
/// <param name="Context">The <see cref="Boto.Widgets.Canvas.Context"/>.</param>
/// <param name="Resolution">The data resolution.</param>
public record Painter(Context Context, (double, double) Resolution)
{
    /// <summary>
    /// Initialize a new instance of <see cref="Painter"/>.
    /// </summary>
    /// <param name="context">The <see cref="Boto.Widgets.Canvas.Context"/>.</param>
    public Painter(Context context)
        : this(context, context.Grid.Resolution)
    {
    }

    /// <summary>
    /// Get the point from resolution.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <returns>The console position.</returns>
    public (int x, int y)? GetPoint(double x, double y)
    {
        var left = Context.XBounds[0];
        var right = Context.XBounds[1];
        var top = Context.YBounds[1];
        var bottom = Context.YBounds[0];
        if (x < left || x > right || y < bottom || y > top)
        {
            return null;
        }

        var width = Math.Abs(Context.XBounds[1] - Context.XBounds[0]);
        var height = Math.Abs(Context.YBounds[1] - Context.YBounds[0]);
        if (width == 0 || height == 0)
        {
            return null;
        }

        var newX = (int)((x - left) * Resolution.Item1 / width);
        var newY = (int)((top - y) * Resolution.Item2 / height);

        return (newX, newY);
    }

    /// <summary>
    /// Paint the point.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="color">The <see cref="Color"/>.</param>
    public void Paint(int x, int y, Color color)
        => Context.Grid.Paint(x, y, color);
}
