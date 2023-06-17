using Boto.Styles;
using Boto.Symbols;
using Boto.Widgets.Canvas;
using CanvasW = Boto.Widgets.Canvas.Canvas;

namespace Boto.Widgets.Extensions;

/// <summary>
/// The <see cref="CanvasW"/> extensions method.
/// </summary>
public static class CanvasExtensions
{
    /// <summary>
    /// Change <see cref="CanvasW.Block"/>.
    /// </summary>
    /// <param name="canvas">The <see cref="CanvasW"/>.</param>
    /// <param name="block">The <see cref="Block"/>.</param>
    /// <returns>The <paramref name="canvas"/> with <see cref="CanvasW.Block"/> as <paramref name="block"/>.</returns>
    public static CanvasW SetBlock(this CanvasW canvas, Block block)
    {
        canvas.Block = block;
        return canvas;
    }

    /// <summary>
    /// Change <see cref="CanvasW.Painter"/>.
    /// </summary>
    /// <param name="canvas">The <see cref="CanvasW"/>.</param>
    /// <param name="painter">The <see cref="Action{T}"/>.</param>
    /// <returns>The <paramref name="canvas"/> with <see cref="CanvasW.Painter"/> as <paramref name="painter"/>.</returns>
    public static CanvasW SetPaint(this CanvasW canvas, Action<Context> painter)
    {
        canvas.Painter = painter;
        return canvas;
    }

    /// <summary>
    /// Change <see cref="CanvasW.Background"/>.
    /// </summary>
    /// <param name="canvas">The <see cref="CanvasW"/>.</param>
    /// <param name="color">The <see cref="Color"/>.</param>
    /// <returns>The <paramref name="canvas"/> with <see cref="CanvasW.Background"/> as <paramref name="color"/>.</returns>
    public static CanvasW SetBackground(this CanvasW canvas, Color color)
    {
        canvas.Background = color;
        return canvas;
    }

    /// <summary>
    /// Change <see cref="CanvasW.Marker"/>.
    /// </summary>
    /// <param name="canvas">The <see cref="CanvasW"/>.</param>
    /// <param name="marker">The <see cref="Marker"/>.</param>
    /// <returns>The <paramref name="canvas"/> with <see cref="CanvasW.Marker"/> as <paramref name="marker"/>.</returns>
    public static CanvasW SetMarker(this CanvasW canvas, Marker marker)
    {
        canvas.Marker = marker;
        return canvas;
    }

    /// <summary>
    /// Change <see cref="CanvasW.XBounds"/>.
    /// </summary>
    /// <param name="canvas">The <see cref="CanvasW"/>.</param>
    /// <param name="left">The start at x position.</param>
    /// <param name="right">The end at x position.</param>
    /// <returns>The <paramref name="canvas"/> with <see cref="CanvasW.XBounds"/> as (<paramref name="left"/> and <paramref name="right"/>).</returns>
    public static CanvasW SetXBounds(this CanvasW canvas, double left, double right)
    {
        canvas.XBounds = new[] { left, right };
        return canvas;
    }

    /// <summary>
    /// Change <see cref="CanvasW.XBounds"/>.
    /// </summary>
    /// <param name="canvas">The <see cref="CanvasW"/>.</param>
    /// <param name="bounds">The x bounds.</param>
    /// <returns>The <paramref name="canvas"/> with <see cref="CanvasW.XBounds"/> as <paramref name="bounds"/>.</returns>
    public static CanvasW SetXBounds(this CanvasW canvas, double[] bounds)
    {
        canvas.XBounds = bounds;
        return canvas;
    }

    /// <summary>
    /// Change <see cref="CanvasW.YBounds"/>.
    /// </summary>
    /// <param name="canvas">The <see cref="CanvasW"/>.</param>
    /// <param name="bottom">The start at y position.</param>
    /// <param name="top">The end at y position.</param>
    /// <returns>The <paramref name="canvas"/> with <see cref="CanvasW.YBounds"/> as (<paramref name="bottom"/> and <paramref name="top"/>).</returns>
    public static CanvasW SetYBounds(this CanvasW canvas, double bottom, double top)
    {
        canvas.YBounds = new[] { bottom, top };
        return canvas;
    }

    /// <summary>
    /// Change <see cref="CanvasW.YBounds"/>.
    /// </summary>
    /// <param name="canvas">The <see cref="CanvasW"/>.</param>
    /// <param name="bounds">The y bounds.</param>
    /// <returns>The <paramref name="canvas"/> with <see cref="CanvasW.YBounds"/> as <paramref name="bounds"/>.</returns>
    public static CanvasW SetYBounds(this CanvasW canvas, double[] bounds)
    {
        canvas.YBounds = bounds;
        return canvas;
    }
}
