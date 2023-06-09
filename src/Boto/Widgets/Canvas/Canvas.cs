﻿using Boto.Extensions;
using Boto.Layouts;
using Boto.Styles;
using Boto.Symbols;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widgets.Canvas;

/// <summary>
/// The canvas widget.
/// </summary>
public class Canvas : IWidget
{
    /// <summary>
    /// The <see cref="Widgets.Block"/>.
    /// </summary>
    public Block? Block { get; set; }

    /// <summary>
    /// The x bounds.
    /// </summary>
    public double[] XBounds { get; set; } = { 0, 0 };

    /// <summary>
    /// The y bounds.
    /// </summary>
    public double[] YBounds { get; set; } = { 0, 0 };

    /// <summary>
    /// The painter.
    /// </summary>
    public Action<Context>? Painter { get; set; }

    /// <summary>
    /// The background <see cref="Color"/>.
    /// </summary>
    public Color Background { get; set; } = Color.Reset;

    /// <summary>
    /// The <see cref="Symbols.Marker"/>.
    /// </summary>
    public Marker Marker { get; set; } = Marker.Braille;

    /// <inheritdoc cref="IWidget.Render"/>
    public void Render(Rect area, Buffer buffer)
    {
        var canvasArea = area;
        if (Block != null)
        {
            var inner = Block.Inner(area);
            Block.Render(area, buffer);
            canvasArea = inner;
        }

        buffer.SetStyle(canvasArea, new() { Background = Background });

        if (Painter == null)
        {
            return;
        }

        // Create a blank context that match the size of the canvas
        var context = new Context(canvasArea.Width, canvasArea.Height, XBounds, YBounds, Marker);

        // Paint to this context
        Painter(context);
        context.Finish();

        var width = canvasArea.Width;
        // Retreive painted points for each layer
        foreach (var layer in context.Layers)
        {
            foreach (var (i, (ch, color)) in layer.Content.ToCharArray()
                         .Zip(layer.Colors).WithIndex())
            {
                if (ch != ' ' && ch != '\x2800')
                {
                    var (x, y) = ((i % width) + canvasArea.Left, (i / width) + canvasArea.Top);
                    buffer[x, y] = buffer[x, y] with { Foreground = color, Symbol = ch.ToString() };
                }
            }
        }

        // Finally draw the labels
        var left = XBounds[0];
        var right = XBounds[1];
        var top = YBounds[1];
        var bottom = YBounds[0];

        var otherWidth = Math.Abs(XBounds[1] - XBounds[0]);
        var otherHeight = Math.Abs(YBounds[1] - YBounds[0]);
        var resolution = (canvasArea.Width - 1, canvasArea.Height - 1);

        foreach (var label in context.Labels
                     .Where(label => label.X >= left && label.X <= right && label.Y <= top && label.Y >= bottom))
        {
            var x = (int)((label.X - left) * resolution.Item1 / otherWidth) + canvasArea.Left;
            var y = (int)((top - label.Y) * resolution.Item2 / otherHeight) + canvasArea.Top;
            buffer.SetSpan(x, y, label.Spans, canvasArea.Right - x);
        }
    }
}
