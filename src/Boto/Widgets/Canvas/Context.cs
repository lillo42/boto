using Boto.Symbols;
using Boto.Texts;

namespace Boto.Widgets.Canvas;

/// <summary>
/// The canvas context.
/// </summary>
/// <param name="XBounds">The x bounds.</param>
/// <param name="YBounds">The y bounds.</param>
/// <param name="Grid">The <see cref="IGrid"/>.</param>
/// <param name="IsDirty">Flag indicating if it's dirty.</param>
/// <param name="Layers">The collection of <see cref="Boto.Widgets.Canvas.Layer"/></param>
/// <param name="Labels">The collection of <see cref="Label"/>.</param>
public record Context(double[] XBounds, double[] YBounds, IGrid Grid, bool IsDirty,
    List<Layer> Layers, List<Label> Labels)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Context"/> class.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="xBounds">The x bounds.</param>
    /// <param name="yBounds">The y bounds.</param>
    /// <param name="marker">The <see cref="Marker"/>.</param>
    public Context(int width, int height, double[] xBounds, double[] yBounds, Marker marker)
        : this(xBounds, yBounds,
            marker switch
            {
                Marker.Dot => new CharGrid(width, height, "•"),
                Marker.Block => new CharGrid(width, height, "▄"),
                Marker.Braille => new BrailleGrid(width, height),
                _ => throw new ArgumentOutOfRangeException(nameof(marker), marker, "Invalid marker")
            },
            false, new(), new())
    {
    }

    /// <summary>
    /// Flag indicating if it's dirty.
    /// </summary>
    public bool IsDirty { get; private set; } = IsDirty;

    /// <summary>
    /// Draws the <see cref="IShape"/>.
    /// </summary>
    /// <param name="shape">The <see cref="IShape"/>.</param>
    public void Draw(IShape shape)
    {
        IsDirty = true;
        shape.Draw(new Painter(this));
    }

    /// <summary>
    /// Create a new layer.
    /// </summary>
    public void Layer()
    {
        Layers.Add(Grid.Save());
        Grid.Reset();
        IsDirty = false;
    }

    /// <summary>
    /// Adds a label.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="spans">The <see cref="Spans"/>.</param>
    public void Print(double x, double y, Spans spans)
        => Labels.Add(new Label(x, y, spans));

    internal void Finish()
    {
        if (IsDirty)
        {
            Layer();
        }
    }
}
