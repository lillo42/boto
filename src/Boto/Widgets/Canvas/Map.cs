using Boto.Styles;

namespace Boto.Widgets.Canvas;

/// <summary>
/// The canvas map.
/// </summary>
/// <param name="Resolution">The <see cref="MapResolution"/>.</param>
/// <param name="Color">The <see cref="Styles.Color"/>.</param>
public record Map(MapResolution Resolution, Color Color) : IShape
{
    /// <summary>
    /// Initialize a new instance of <see cref="Map"/>.
    /// </summary>
    public Map()
        : this(MapResolution.Low, Color.Reset)
    {}


    /// <inheritdoc cref="IShape.Draw"/>
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
