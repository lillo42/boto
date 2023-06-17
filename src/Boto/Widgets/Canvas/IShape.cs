namespace Boto.Widgets.Canvas;

/// <summary>
/// The canvas shape.
/// </summary>
public interface IShape
{
    /// <summary>
    /// Paint the shape.
    /// </summary>
    /// <param name="painter">The <see cref="Painter"/>.</param>
    void Draw(Painter painter);
}
