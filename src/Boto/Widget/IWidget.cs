using Boto.Layouts;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widget;

public interface IWidget
{
    void Render(Rect area, Buffer buffer);
}
