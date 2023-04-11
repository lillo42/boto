using Boto.Styles;
using Boto.Symbols;
using Boto.Widget.Canvas;

namespace Boto.Widget;

public static class CanvasExtensions
{
    public static Canvas.Canvas Block(this Canvas.Canvas canvas, Block block)
    {
        canvas.Block = block;
        return canvas;
    }

    public static Canvas.Canvas Paint(this Canvas.Canvas canvas, Action<Context> painter)
    {
        canvas.Painter = painter;
        return canvas;
    }

    public static Canvas.Canvas Background(this Canvas.Canvas canvas, Color color)
    {
        canvas.Background = color;
        return canvas;
    }

    public static Canvas.Canvas Marker(this Canvas.Canvas canvas, Marker marker)
    {
        canvas.Marker = marker;
        return canvas;
    }

    public static Canvas.Canvas XBounds(this Canvas.Canvas canvas, double left, double right)
    {
        canvas.XBounds = new[] { left, right };
        return canvas;
    }

    public static Canvas.Canvas XBounds(this Canvas.Canvas canvas, double[] bounds)
    {
        canvas.XBounds = bounds;
        return canvas;
    }

    public static Canvas.Canvas YBounds(this Canvas.Canvas canvas, double bottom, double top)
    {
        canvas.YBounds = new[] { bottom, top };
        return canvas;
    }

    public static Canvas.Canvas YBounds(this Canvas.Canvas canvas, double[] bounds)
    {
        canvas.YBounds = bounds;
        return canvas;
    }
}
