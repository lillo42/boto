using Boto.Styles;

namespace Boto.Widget.Canvas;

public record Map(MapResolution Resolution, Color Color) : IShape
{
    public Map()
        : this(MapResolution.Low, Color.Reset)
    {}


    public void Draw(Painter painter)
    {
        foreach (var (x, y) in Resolution.Data())
        {
            if(painter.GetPoint((int)x, (int)y) is { } point)
            {
                painter.Paint(point.Item1, point.Item2, Color);
            }
        }
    }
}
