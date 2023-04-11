using Boto.Styles;

namespace Boto.Widget.Canvas;

public class Rectangle : IShape
{
    public Rectangle()
    {
    }

    public Rectangle(double x, double y, double width, double height, Color color)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
        Color = color;
    }

    public void Draw(Painter painter)
    {
        var lines = new[]
        {
            new Line(X, Y, X, Y + Height, Color), new Line(X, Y + Height, X + Width, Y + Height, Color),
            new Line(X + Width, Y, X + Width, Y + Height, Color), new Line(X, Y, X + Width, Y, Color)
        };

        foreach (var line in lines)
        {
            line.Draw(painter);
        }
    }

    public double X { get; set; }
    public double Y { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public Color Color { get; set; }
}
