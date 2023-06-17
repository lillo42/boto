using Boto.Layouts;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widgets;

/// <summary>
/// The clear widget.
/// </summary>
public record Clear : IWidget
{
    /// <inheritdoc cref="IWidget.Render"/>
    public void Render(Rect area, Buffer buffer)
    {
        for (var x = area.Left; x < area.Right; x++)
        {
            for (var y = area.Top; y < area.Bottom; y++)
            {
                buffer[x, y] = new();
            }
        }
    }
}
