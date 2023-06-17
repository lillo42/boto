using Boto.Styles;

namespace Boto.Widgets.Canvas;

/// <summary>
/// The canvas rectangle.
/// </summary>
public class Rectangle : IShape
{
    /// <summary>
    /// Initialize a new instance of <see cref="Rectangle"/>.
    /// </summary>
    public Rectangle()
    {
    }

    /// <summary>
    /// Initialize a new instance of <see cref="Rectangle"/>.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="color">The <see cref="Styles.Color"/>.</param>
    public Rectangle(double x, double y, double width, double height, Color color)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
        Color = color;
    }

    /// <inheritdoc cref="IShape.Draw"/>
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

    /// <summary>
    /// The x.
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// The y.
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// The width.
    /// </summary>
    public double Width { get; set; }

    /// <summary>
    /// The height.
    /// </summary>
    public double Height { get; set; }

    /// <summary>
    /// The <see cref="Styles.Color"/>.
    /// </summary>
    public Color Color { get; set; }
}
