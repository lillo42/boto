using Boto.Layouts;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widgets;

/// <summary>
/// The widget.
/// </summary>
public interface IWidget
{
    /// <summary>
    /// Renders the widget.
    /// </summary>
    /// <param name="area">The <see cref="Rect"/> when the component will be render.</param>
    /// <param name="buffer">The <see cref="Boto.Buffers.Buffer"/>.</param>
    void Render(Rect area, Buffer buffer);
}
