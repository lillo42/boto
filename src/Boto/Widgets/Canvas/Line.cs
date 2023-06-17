using Boto.Extensions;
using Boto.Styles;

namespace Boto.Widgets.Canvas;

/// <summary>
/// The canvas line.
/// </summary>
/// <param name="X1">Start at x1.</param>
/// <param name="Y1">Start at y1.</param>
/// <param name="X2">End at x2.</param>
/// <param name="Y2">End at y2.</param>
/// <param name="Color">The <see cref="Styles.Color"/>.</param>
public record Line(double X1, double Y1, double X2, double Y2, Color Color) : IShape
{
    /// <inheritdoc cref="IShape.Draw"/>
    public void Draw(Painter painter)
    {
        if (painter.GetPoint(X1, Y1) is not { } point1)
        {
            return;
        }

        if (painter.GetPoint(X2, Y2) is not { } point2)
        {
            return;
        }
        
        var (x1, y1) = point1;
        var (x2, y2) = point2;
        
        var (dx, xRange) = x2 >= x1 ? (x2 - x1, (x1, x2)) : (x1 - x2, (x2, x1));
        var (dy, yRange) = y2 >= y1 ? (y2 - y1, (y1, y2)) : (y1 - y2, (y2, y1));

        if (dx == 0)
        {
            for (var y = yRange.Item1; y <= yRange.Item2; y++)
            {
                painter.Paint(x1, y, Color);
            }
        }
        else if (dy == 0)
        {
            for (var x = xRange.Item1; x <= xRange.Item2; x++)
            {
                painter.Paint(x, y1, Color);
            }
        }
        else if (dy < dx)
        {
            if (x1 > x2)
            {
                DrawLineLow(painter, x2, y2, x1, y2, Color);
            }
            else
            {
                DrawLineLow(painter, x1, y1, x2, y2, Color);
            }
        }
        else if (y1 > y2)
        {
            DrawLineHigh(painter, x2, y2, x1, y2, Color);
        }
        else
        {
            DrawLineHigh(painter, x1, y1, x2, y2, Color);
        }
    }

    private static void DrawLineLow(Painter painter, int x1, int y1, int x2, int y2, Color color)
    {
        var dx = x2 - x1;
        var dy = Math.Abs(y2 - y1);
        var d = 2 * dy - dx;
        var y = y1;

        for (var x = x1; x <= x2; x++)
        {
            painter.Paint(x, y, color);
            if (d > 0)
            {
                y = y1 > y2 ? y.SaturatingSub(1) : y + 1;
                d -= 2 * dx;
            }
            
            d += 2 * dy;
        }
    }
    
    private static void DrawLineHigh(Painter painter, int x1, int y1, int x2, int y2, Color color)
    {
        var dx = Math.Abs(x2 - x1);
        var dy = y2 - y1;
        var d = 2 * dx - dy;
        var x = x1;
        
        for(var y = y1; y <= y2; y++)
        {
            painter.Paint(x, y, color);
            if (d > 0)
            {
                x = x1 > x2 ? x.SaturatingSub(1) : x + 1;
                d -= 2 * dy;
            }
            
            d += 2 * dx;
        }
    }
}
