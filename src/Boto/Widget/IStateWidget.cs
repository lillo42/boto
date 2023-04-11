using Boto.Layouts;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widget;

public interface IStateWidget<in T>
    where T : notnull
{
    void Render(Rect area, Buffer buffer, T state);
}
