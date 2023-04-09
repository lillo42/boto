using Boto.Layouts;

namespace Boto.Terminals;

/// <summary>
/// The Viewport represents the area of the terminal that is available for rendering.
/// </summary>
/// <param name="Area">The <see cref="Rect"/>.</param>
/// <param name="ResizeBehavior">The <see cref="Terminals.ResizeBehavior"/></param>
public record Viewport(Rect Area, ResizeBehavior ResizeBehavior)
{
    /// <summary>
    /// Creates a new <see cref="Viewport"/> with <see cref="ResizeBehavior"/> set to <see cref="Terminals.ResizeBehavior.Auto"/>. 
    /// </summary>
    /// <param name="area">The <see cref="Rect"/>.</param>
    /// <returns>New <see cref="Viewport"/> instance with <see cref="ResizeBehavior"/> as <see cref="Terminals.ResizeBehavior.Auto"/>.</returns>
    public static Viewport Fixed(Rect area) => new(area, ResizeBehavior.Fixed);
}
