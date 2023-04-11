using System.Globalization;
using Boto.Extensions;
using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;

namespace Boto.Buffers;

/// <summary>
/// A buffer that maps to the desired content of the terminal after the draw call.
/// </summary>
/// <remarks>
/// No widget in the library interacts directly with the terminal. Instead each of them is required
/// to draw their state to an intermediate buffer. It is basically a grid where each cell contains
/// a grapheme, a foreground color and a background color. This grid will then be used to output
/// the appropriate escape sequences and characters to draw the UI as the user has defined it.
/// </remarks>
/// <example>
/// <code>
/// var buffer = new Buffer(new Rect(0, 0, 10, 10));
/// buffer[0, 2] = buffer[0, 2] with { Symbol = "x" };
/// Debug.Assert(buffer[0, 2].Symbol == "x");
/// 
/// buffer.String(3, 0, "string", new() { Foreground = Color.Red, Background = Color.White });
/// Debug.Assert(buffer[5, 0].Symbol == new Cell("t", Color.Red, Color.White, Modifier.None)));
/// 
/// buffer[5, 0] = buffer[5, 0] with { Symbol = "x" };
/// Debug.Assert(buffer[5, 0].Symbol == "x");
/// </code>
/// </example>
public record Buffer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Buffer"/> class.
    /// </summary>
    /// <param name="area">The buffer area.</param>
    public Buffer(Rect area)
        : this(area, new Cell())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Buffer"/> class.
    /// </summary>
    /// <param name="area">The buffer area.</param>
    /// <param name="cell">The default buffer content.</param>
    public Buffer(Rect area, Cell cell)
    {
        var size = area.Area;
        var content = new List<Cell>(size);

        for (var i = 0; i < size; i++)
        {
            content.Add(cell);
        }

        Area = area;
        Content = content;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Buffer"/> class.
    /// </summary>
    /// <param name="lines">The default line that the buffer should have.</param>
    public Buffer(IReadOnlyList<string> lines)
        : this(new Rect(0, 0, lines.Max(l => l.Width()), lines.Count))
    {
        for (var index = 0; index < lines.Count; index++)
        {
            var line = lines[index];
            SetString(0, index, line, new());
        }
    }

    /// <summary>
    /// The area represented by this buffer.
    /// </summary>
    public Rect Area { get; set; }

    /// <summary> The content of the buffer.
    /// The <see cref="List{T}.Count"/> of this <see cref="List{T}"/> should always be equal to
    /// <see cref="Rect.Width"/> * <see cref="Rect.Height"/>
    /// </summary>
    public List<Cell> Content { get; init; }


    /// <summary>
    /// Cell at the given coordinates. 
    /// </summary>
    /// <param name="x">The X position.</param>
    /// <param name="y">The Y position.</param>
    public Cell this[int x, int y]
    {
        get => Content[IndexOf(x, y)];
        set => Content[IndexOf(x, y)] = value;
    }

    /// <summary>
    /// Returns the index in the <see cref="List{T}"/> of <see cref="Cell"/> for the given global (<paramref name="x"/>, <paramref name="y"/>) coordinates.
    /// </summary>
    /// <param name="x">The X position.</param>
    /// <param name="y">The Y position.</param>
    /// <returns>
    /// Returns the index in the <see cref="List{T}"/> of <see cref="Cell"/> for the given global (<paramref name="x"/>, <paramref name="y"/>) coordinates.
    /// </returns>
    /// <example>
    /// <code>
    /// var rect = new Rect(200, 100, 10, 10);
    /// var buffer = new Buffer(rect);
    /// 
    /// // Global coordinates to the top corner of this buffer's area
    /// Debug.Assert(buffer.IndexOf(200, 100) == 0);
    /// </code>
    /// </example>
    public int IndexOf(int x, int y)
        => (y - Area.Y) * Area.Width + (x - Area.X);

    /// <summary>
    /// Returns the (global) coordinates of a cell given its index.
    /// </summary>
    /// <param name="index">The <see cref="Content"/> index.</param>
    /// <returns>Returns the (global) coordinates of a cell given its index.</returns>
    /// <remarks>
    /// Global coordinates are offset by the Buffer's area offset (`x`/`y`).
    /// </remarks>
    public Coordinate PosOf(int index)
        => new(Area.X + index % Area.Width, Area.Y + index / Area.Width);


    /// <summary>
    /// Set the <paramref name="text"/>, starting at coordinates (<paramref name="x"/>, <paramref name="y"/>) with given style.
    /// </summary>
    /// <param name="x">The X position.</param>
    /// <param name="y">The Y position.</param>
    /// <param name="text">The text.</param>
    /// <param name="style">The <see cref="Styles.Style"/>.</param>
    public void SetString(int x, int y, string text, Style style) => SetString(x, y, text, ushort.MaxValue, style);

    /// <summary>
    /// Set the <paramref name="text"/>, starting at coordinates (<paramref name="x"/>, <paramref name="y"/>) with given style.
    /// By setting at most first n characters of the string if enough space is available until the end of the line.
    /// </summary>
    /// <param name="x">The X position.</param>
    /// <param name="y">The Y position.</param>
    /// <param name="text">The text.</param>
    /// <param name="width">The width of space.</param>
    /// <param name="style">The <see cref="Styles.Style"/>.</param>
    /// <returns>The <see cref="Coordinate"/> end.</returns>
    public Coordinate SetString(int x, int y, string text, int width, Style style)
    {
        var indexOf = IndexOf(x, y);
        var xOffset = x;
        var maxOffset = Math.Min(Area.Right, width + x);
        var graphemes = StringInfo.GetTextElementEnumerator(text);
        while (graphemes.MoveNext())
        {
            var symbol = graphemes.GetTextElement();
            var length = symbol.Width();
            if (length == 0)
            {
                continue;
            }

            // `x_offset + width > max_offset` could be integer overflow on 32-bit machines if we
            // change dimenstions to usize or u32 and someone resizes the terminal to 1x2^32.
            if (length > maxOffset.SaturatingSub(xOffset))
            {
                break;
            }

            Content[indexOf] = new(Content[indexOf], style) { Symbol = symbol };

            // Reset following cells if multi-width (they would be hidden by the grapheme)
            for (var i = indexOf + 1; i < indexOf + length; i++)
            {
                Content[i] = new Cell();
            }

            indexOf += length;
            xOffset += length;
        }

        return new(xOffset, y);
    }

    /// <summary>
    /// Set the <paramref name="span"/>, starting at coordinates (<paramref name="x"/>, <paramref name="y"/>) with given style.
    /// By setting at most first n characters of the string if enough space is available until the end of the line.
    /// </summary>
    /// <param name="x">The X position.</param>
    /// <param name="y">The Y position.</param>
    /// <param name="span">The <see cref="Span"/>.</param>
    /// <param name="width">The width of space.</param>
    /// <returns></returns>
    public Coordinate SetSpan(int x, int y, Span span, int width)
        => SetString(x, y, span.Content, width, span.Style);

    /// <summary>
    /// Set the <paramref name="spans"/>, starting at coordinates (<paramref name="x"/>, <paramref name="y"/>) with given style.
    /// By setting at most first n characters of the string if enough space is available until the end of the line.
    /// </summary>
    /// <param name="x">The X position.</param>
    /// <param name="y">The Y position.</param>
    /// <param name="spans">The <see cref="Spans"/>.</param>
    /// <param name="width">The width of space.</param>
    /// <returns></returns>
    public Coordinate SetSpan(int x, int y, Spans spans, int width)
    {
        var remainingWidth = width;
        foreach (var span in spans.Value)
        {
            if (remainingWidth == 0)
            {
                break;
            }

            var pos = SetString(x, y, span.Content, remainingWidth, span.Style);
            var w = pos.X.SaturatingSub(x);
            x = pos.X;
            remainingWidth = remainingWidth.SaturatingSub(w);
        }

        return new(x, y);
    }

    /// <summary>
    /// Set the <see cref="Style"/> to all cells in the given <paramref name="area"/>.
    /// </summary>
    /// <param name="area">The <see cref="Rect"/>.</param>
    /// <param name="style">The new <see cref="Style"/>.</param>
    public void SetStyle(Rect area, Style style)
    {
        for (var y = area.Top; y < area.Bottom; y++)
        {
            for (var x = area.Left; x < area.Right; x++)
            {
                this[x, y] = new Cell(this[x, y], style);
            }
        }
    }

    /// <summary>
    /// Resize the buffer so that the mapped area matches the given area and that the buffer
    /// length is equal to area.width * area.height
    /// </summary>
    /// <param name="area">The new area.</param>
    public void Resize(Rect area)
    {
        var length = area.Area;
        if (Content.Count > length)
        {
            Content.RemoveRange(length, Content.Count - length);
        }
        else
        {
            Content.AddRange(Enumerable.Repeat(new Cell(), length - Content.Count));
        }

        Area = area;
    }

    /// <summary>
    /// Reset all cells in the buffer
    /// </summary>
    public void Reset()
    {
        for (var i = 0; i < Content.Count; i++)
        {
            Content[i] = new();
        }
    }

    /// <summary>
    /// Merge an other buffer into this one
    /// </summary>
    /// <param name="other">The other <see cref="Buffer"/>.</param>
    public void Merge(Buffer other)
    {
        var area = Area.Union(other.Area);
        var cell = new Cell();
        Content.AddRange(Enumerable.Repeat(cell, area.Area - Content.Count));

        // Move original content to the appropriate space
        var size = Area.Area;

        for (var i = size - 1; i >= 0; i--)
        {
            var (x, y) = PosOf(i);

            // New index in content
            var k = (y - area.Y) * area.Width + x - area.X;
            if (k != i)
            {
                (Content[k], Content[i]) = (Content[i], cell);
            }
        }

        // Push content of the other buffer into this one (may erase previous
        // data)
        size = other.Area.Area;
        for (var i = 0; i < size; i++)
        {
            var (x, y) = other.PosOf(i);

            // New index in content
            var k = (y - area.Y) * area.Width + x - area.X;
            Content[k] = other.Content[i];
        }

        Area = area;
    }

    /// <summary>
    /// Builds a minimal sequence of coordinates and Cells necessary to update the UI from
    /// self to other.
    /// </summary>
    /// <param name="other">The <see cref="Buffer"/> to be compared.</param>
    /// <returns>A <see cref="List{T}"/> of <see cref="BufferDiff"/> with all difference between the buffers.</returns>
    /// <remarks>
    /// <para>
    /// We're assuming that buffers are well-formed, that is no double-width cell is followed by
    /// a non-blank cell.
    /// </para>
    ///
    /// <para>
    /// <b>Multi-width characters handling:</b>
    /// </para>
    /// <para>
    /// (Index:) `01`
    /// Prev:    `コ`
    /// Next:    `aa`
    /// Updates: `0: a, 1: a'
    /// </para>
    ///
    /// <para>
    /// (Index:) `01`
    /// Prev:    `a `
    /// Next:    `コ`
    /// Updates: `0: コ` (double width symbol at index 0 - skip index 1)
    /// </para>
    ///
    /// <para>
    /// (Index:) `012`
    /// Prev:    `aaa`
    /// Next:    `aコ`
    /// Updates: `0: a, 1: コ` (double width symbol at index 1 - skip index 2)
    /// </para>
    /// </remarks>
    public List<BufferDiff> Diff(Buffer other)
    {
        var previousBuffer = Content;
        var nextBuffer = other.Content;
        var width = Area.Width;

        var updates = new List<BufferDiff>();

        // Cells invalidated by drawing/replacing preceeding multi-width characters:
        var invalidated = 0;

        // Cells from the current buffer to skip due to preceeding multi-width characters taking their
        // place (the skipped cells should be blank anyway):
        var toSkip = 0;

        foreach (var (i, (current, previous)) in nextBuffer.Zip(previousBuffer).WithIndex())
        {
            if ((current != previous || invalidated > 0) && toSkip == 0)
            {
                var x = i % width;
                var y = i / width;
                updates.Add(new(y, x, nextBuffer[i]));
            }

            var currentWidth = current.Symbol.Width();
            toSkip = currentWidth.SaturatingSub(1);

            var affectedWidth = Math.Max(currentWidth, previous.Symbol.Width());
            invalidated = Math.Max(affectedWidth, invalidated).SaturatingSub(1);
        }

        return updates;
    }
}
