using Boto.Extensions;
using Boto.Layouts;
using Boto.Styles;
using Boto.Symbols;
using Boto.Texts;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widget;

public class Tabs : IWidget
{
    public Tabs()
    {
    }

    public Tabs(List<Spans> titles)
    {
        Titles = titles;
    }

    public Block? Block { get; set; }
    public int Selected { get; set; }
    public Style Style { get; set; }
    public Style HighlightStyle { get; set; }
    public Span Divider { get; set; } = new(Line.Vertical);
    public List<Spans> Titles { get; set; } = new();

    public void Render(Rect area, Buffer buffer)
    {
        buffer.SetStyle(area, Style);
        var tabsArea = area;
        if (Block != null)
        {
            var inner = Block.Inner(area);
            Block.Render(area, buffer);
            tabsArea = inner;
        }

        if (tabsArea.Height < 1)
        {
            return;
        }

        var x = tabsArea.Left;
        for (var i = 0; i < Titles.Count; i++)
        {
            var title = Titles[i];
            var isLastTitle = i == Titles.Count - 1;
            x++;
            var remainingWidth = tabsArea.Right - x;
            if (remainingWidth < 0)
            {
                break;
            }

            var pos = buffer.SetSpan(x, tabsArea.Top, title, remainingWidth);
            if (i == Selected)
            {
                buffer.SetStyle
                (new Rect(x, tabsArea.Top, pos.X.SaturatingSub(x), 1),
                    HighlightStyle);
            }

            x = pos.X + 1;
            remainingWidth = tabsArea.Right - x;
            if (remainingWidth <= 0 || isLastTitle)
            {
                break;
            }

            (x, _) = buffer.SetSpan(x, tabsArea.Top, Divider, remainingWidth);
        }
    }
}
