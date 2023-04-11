using Boto.Styles;

namespace Boto.Widget.Canvas;

public record Points((double, double)[] Coords, Color Color) : IShape
{
    public Points()
        : this(Array.Empty<(double, double)>(), Color.Reset)
    {
    }

    public void Draw(Painter painter)
    {
        foreach (var (x, y) in Coords)
        {
            if (painter.GetPoint(x, y) is {} point)
            {
                painter.Paint(point.Item1, point.Item2, Color);
            }
        }
    }
}
