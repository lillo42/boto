using Boto.Styles;

namespace Boto.Widgets.Canvas;

/// <summary>
/// The canvas grid.
/// </summary>
public interface IGrid
{
    /// <summary>
    /// The width.
    /// </summary>
    int Width { get; }
    
    /// <summary>
    /// The height.
    /// </summary>
    int Height { get; }
    
    /// <summary>
    /// The resolution.
    /// </summary>
    (double, double) Resolution { get; }

    /// <summary>
    /// Paint the grid.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="color">The <see cref="Color"/>.</param>
    void Paint(int x, int y, Color color);
    
    /// <summary>
    /// Save the grid by create a layer.
    /// </summary>
    /// <returns>The saved layer.</returns>
    Layer Save();
    
    /// <summary>
    /// Reset the grid.
    /// </summary>
    void Reset();
}
