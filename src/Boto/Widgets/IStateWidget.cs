using Boto.Layouts;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widgets;

/// <summary>
/// The state widget.
/// </summary>
/// <typeparam name="T">The state.</typeparam>
public interface IStateWidget<in T>
    where T : notnull
{
    /// <summary>
    /// Renders the widget.
    /// </summary>
    /// <param name="area">The <see cref="Rect"/> when the component will be render.</param>
    /// <param name="buffer">The <see cref="Boto.Buffers.Buffer"/>.</param>
    /// <param name="state">The widget state <typeparamref name="T"/>.</param>
    void Render(Rect area, Buffer buffer, T state);
}
