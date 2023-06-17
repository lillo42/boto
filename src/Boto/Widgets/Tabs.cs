using Boto.Extensions;
using Boto.Layouts;
using Boto.Styles;
using Boto.Symbols;
using Boto.Texts;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widgets;

/// <summary>
/// The tabs widget.
/// </summary>
public class Tabs : IWidget
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tabs"/> class.
    /// </summary>
    public Tabs()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Tabs"/> class.
    /// </summary>
    /// <param name="titles">The title collection.</param>
    public Tabs(List<Spans> titles)
    {
        Titles = titles;
    }

    /// <summary>
    /// The <see cref="Widgets.Block"/>.
    /// </summary>
    public Block? Block { get; set; }
    
    /// <summary>
    /// The selected.
    /// </summary>
    public int Selected { get; set; }
    
    /// <summary>
    /// The <see cref="Styles.Style"/>.
    /// </summary>
    public Style Style { get; set; }
    
    /// <summary>
    /// The highlight <see cref="Styles.Style"/>.
    /// </summary>
    public Style HighlightStyle { get; set; }
    
    /// <summary>
    /// The divider <see cref="Span"/>.
    /// </summary>
    public Span Divider { get; set; } = new(Line.Vertical);
    
    /// <summary>
    /// The title collection.
    /// </summary>
    public List<Spans> Titles { get; set; } = new();

    /// <inheritdoc cref="IWidget.Render"/>
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
