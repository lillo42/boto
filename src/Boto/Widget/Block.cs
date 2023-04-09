using Boto.Extensions;
using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widget;

public class Block : IWidget
{
    public Spans? Title { get; set; }
    public Alignment TitleAlignment { get; set; } = Alignment.Left;
    public Borders Borders { get; set; } = Borders.None;
    public Style BorderStyle { get; set; } = new();
    public BorderType BorderType { get; set; } = BorderType.Plain;
    public Style Style { get; set; } = new();

    public Rect Inner(Rect area)
    {
        var inner = area;
        if (Borders.HasFlag(Borders.Left))
        {
            inner = inner with
            {
                X = Math.Min(inner.X + 1, inner.Right), 
                Width = inner.Width.SaturatingSub(1)
            };
        }

        if (Borders.HasFlag(Borders.Top) || Title != null)
        {
            inner = inner with
            {
                Y = Math.Min(inner.Y + 1, inner.Bottom), 
                Height = inner.Height.SaturatingSub(1)
            };
        }

        if (Borders.HasFlag(Borders.Right))
        {
            inner = inner with { Width = inner.Width.SaturatingSub(1) };
        }

        if (Borders.HasFlag(Borders.Bottom))
        {
            inner = inner with { Height = inner.Height.SaturatingSub(1) };
        }

        return inner;
    }

    public void Render(Rect area, Buffer buffer)
    {
        if (area.Area == 0)
        {
            return;
        }

        buffer.SetStyle(area, Style);
        var symbols = BorderType.LineSymbol();

        // Sides
        if (Borders.HasFlag(Borders.Left))
        {
            for (var y = area.Top; y < area.Bottom; y++)
            {
                buffer[area.Left, y] = buffer[area.Left, y].With(BorderStyle) with { Symbol = symbols.Vertical };
            }
        }

        if (Borders.HasFlag(Borders.Top))
        {
            for (var x = area.Left; x < area.Right; x++)
            {
                buffer[x, area.Top] = buffer[x, area.Top].With(BorderStyle) with { Symbol = symbols.Horizontal };
            }
        }

        if (Borders.HasFlag(Borders.Right))
        {
            var x = area.Right - 1;
            for (var y = area.Top; y < area.Bottom; y++)
            {
                buffer[x, y] = buffer[x, y].With(BorderStyle) with { Symbol = symbols.Vertical };
            }
        }

        if (Borders.HasFlag(Borders.Bottom))
        {
            var y = area.Bottom - 1;
            for (var x = area.Left; x < area.Right; x++)
            {
                buffer[x, y] = buffer[x, y].With(BorderStyle) with { Symbol = symbols.Horizontal };
            }
        }

        // Corners
        if (Borders.HasFlag(Borders.Right) || Borders.HasFlag(Borders.Bottom))
        {
            buffer[area.Right - 1, area.Bottom - 1] = buffer[area.Right - 1, area.Bottom - 1].With(BorderStyle) with
            {
                Symbol = symbols.BottomRight
            };
        }

        if (Borders.HasFlag(Borders.Right) || Borders.HasFlag(Borders.Top))
        {
            buffer[area.Right - 1, area.Top] = buffer[area.Right - 1, area.Top].With(BorderStyle) with
            {
                Symbol = symbols.TopRight
            };
        }

        if (Borders.HasFlag(Borders.Left) || Borders.HasFlag(Borders.Bottom))
        {
            buffer[area.Left, area.Bottom - 1] = buffer[area.Left, area.Bottom - 1].With(BorderStyle) with
            {
                Symbol = symbols.BottomLeft
            };
        }
        
        if (Borders.HasFlag(Borders.Left) || Borders.HasFlag(Borders.Top))
        {
            buffer[area.Left, area.Top] = buffer[area.Left, area.Top].With(BorderStyle) with
            {
                Symbol = symbols.TopLeft
            };
        }
        
        // Title
        if (Title != null)
        {
            var leftBorderIndex = Borders.HasFlag(Borders.Left) ? 1 : 0;
            var rightBorderIndex = Borders.HasFlag(Borders.Right) ? 1 : 0;

            var titleAreaWidth = unchecked(area.Width - leftBorderIndex - rightBorderIndex);
            var titleIndex = TitleAlignment switch
            {
                Alignment.Left => leftBorderIndex,
                Alignment.Center => area.Width.SaturatingSub(Title.Width) / 2,
                Alignment.Right => area.Width.SaturatingSub(Title.Width).SaturatingSub(rightBorderIndex),
                _ => throw new ArgumentOutOfRangeException(nameof(TitleAlignment), TitleAlignment, null)
            };

            var titleX = area.Left + titleIndex;
            var titleY = area.Top;

            buffer.SetSpan(titleX, titleY, Title, titleAreaWidth);
        }
    }
}
