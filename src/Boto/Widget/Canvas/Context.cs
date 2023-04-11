using Boto.Symbols;
using Boto.Texts;

namespace Boto.Widget.Canvas;

public record Context(double[] XBounds, double[] YBounds, IGrid Grid, bool IsDirty,
    List<Layer> Layers, List<Label> Labels)
{
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

    public bool IsDirty { get; private set; } = IsDirty;

    public void Draw(IShape shape)
    {
        IsDirty = true;
        shape.Draw(new Painter(this));
    }

    public void Layer()
    {
        Layers.Add(Grid.Save());
        Grid.Reset();
        IsDirty = false;
    }

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
