using Boto.Styles;

namespace Boto.Widget.Canvas;

public record Painter(Context Context, (double, double) Resolution)
{
    public Painter(Context context)
        : this(context, context.Grid.Resolution)
    {
    }

    public (int, int)? GetPoint(double x, double y)
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

    public void Paint(int x, int y, Color color)
        => Context.Grid.Paint(x, y, color);
}
