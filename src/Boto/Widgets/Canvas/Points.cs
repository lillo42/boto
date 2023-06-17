using Boto.Styles;

namespace Boto.Widgets.Canvas;

/// <summary>
/// The grid points.
/// </summary>
/// <param name="Coordinate">The coordinates points.</param>
/// <param name="Color">The <see cref="Styles.Color"/>.</param>
public record Points((double, double)[] Coordinate, Color Color) : IShape
{
    /// <summary>
    /// Initialize a new instance of <see cref="Points"/>.
    /// </summary>
    public Points()
        : this(Array.Empty<(double, double)>(), Color.Reset)
    {
    }

    /// <inheritdoc cref="IShape.Draw"/>
    public void Draw(Painter painter)
    {
        foreach (var (x, y) in Coordinate)
        {
            if (painter.GetPoint(x, y) is {} point)
            {
                painter.Paint(point.Item1, point.Item2, Color);
            }
        }
    }
}
