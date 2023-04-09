using Boto.Extensions;
using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;
using Boto.Widget.Reflow;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widget;

public class Paragraph : IWidget
{
    public Paragraph()
    {
        
    }
    
    public Paragraph(Text text)
    {
        Text = text;
    }
    
    public Block? Block { get; set; }
    public Style Style { get; set; }
    public Text Text { get; set; } = new(string.Empty);
    public bool? Trim { get; set; }
    public (int, int) Scroll { get; set; } = (0, 0);
    public Alignment Alignment { get; set; } = Alignment.Left;

    public void Render(Rect area, Buffer buffer)
    {
        buffer.SetStyle(area, Style);
        var textArea = area;
        if (Block != null)
        {
            var inner = Block.Inner(area);
            Block.Render(area, buffer);
            textArea = inner;
        }

        if (textArea.Height < 1)
        {
            return;
        }

        var styled = Text.Lines.SelectMany(spans =>
        {
            return spans.Value
                .Select(span => span.StyledGraphemes(Style))
                .SelectMany(x => x)
                .Append(new StyledGrapheme("\n", Style));
        });

        IEnumerable<(List<StyledGrapheme>, int)> lineComposer;
        if (Trim is { } trim)
        {
            lineComposer = new WordWrapper(styled, textArea.Width, trim);
        }
        else
        {
            var truncate = new LineTruncator(styled, textArea.Width);
            if (Alignment == Alignment.Left)
            {
                truncate.HorizontalOffset = Scroll.Item2;
            }

            lineComposer = truncate;
        }

        var y = 0;
        foreach (var (currentLine, currentLineWidth)  in lineComposer)
        {
            if (y >= Scroll.Item1)
            {
                var x = GetLineOffset(currentLineWidth, textArea.Width, Alignment);
                foreach (var (symbol, style) in currentLine)
                {
                    var indexX = textArea.Left + x;
                    var indexY = textArea.Top + y - Scroll.Item1;
                    buffer[indexX, indexY] = buffer[indexX, indexY].With(style) with
                    {
                        // If the symbol is empty, the last char which rendered last time will
                        // leave on the line. It's a quick fix.
                        Symbol = symbol.Length == 0 ? " " : symbol
                    };

                    x += symbol.Width();
                }
            }

            y++;
            if (y >= textArea.Height + Scroll.Item1)
            {
                break;
            }
        }
    }

    private static int GetLineOffset(int lineWidth, int textAreaWidth, Alignment alignment)
    {
        return alignment switch
        {
            Alignment.Left => 0,
            Alignment.Center => (textAreaWidth / 2).SaturatingSub(lineWidth / 2),
            Alignment.Right => textAreaWidth.SaturatingSub(lineWidth),
            _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null)
        };
    }
}
