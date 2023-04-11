using Boto.Layouts;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widget;

public record Clear : IWidget
{
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
