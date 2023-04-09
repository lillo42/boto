using Boto.Styles;
using Boto.Texts;

namespace Boto.Widget;

public static class TabExtensions
{
    public static Tabs Block(this Tabs tabs, Block block)
    {
        tabs.Block = block;
        return tabs;
    }

    public static Tabs Style(this Tabs tabs, Style style)
    {
        tabs.Style = style;
        return tabs;
    }

    public static Tabs Selected(this Tabs tabs, int selected)
    {
        tabs.Selected = selected;
        return tabs;
    }

    public static Tabs HighlightStyle(this Tabs tabs, Style highlightStyle)
    {
        tabs.HighlightStyle = highlightStyle;
        return tabs;
    }

    public static Tabs Divider(this Tabs tabs, Span divider)
    {
        tabs.Divider = divider;
        return tabs;
    }

    public static Tabs Divider(this Tabs tabs, string divider)
        => tabs.Divider(new Span(divider));


    public static Tabs Divider(this Tabs tabs, string divider, Style style)
        => tabs.Divider(new Span(divider, style));
    
    public static Tabs AddTitle(this Tabs tabs, Spans title)
    {
        tabs.Titles.Add(title);
        return tabs;
    }
    
    public static Tabs AddTitles(this Tabs tabs, IEnumerable<Spans> titles)
    {
        tabs.Titles.AddRange(titles);
        return tabs;
    }
    
    public static Tabs AddTitles(this Tabs tabs, params Spans[] titles)
    {
        tabs.Titles.AddRange(titles);
        return tabs;
    }
    
    public static Tabs AddTitle(this Tabs tabs, string title)
        => tabs.AddTitle(new Spans(title));
    
    public static Tabs AddTitle(this Tabs tabs, string title, Style style)
        => tabs.AddTitle(new Spans(title, style));
    
    public static Tabs AddTitles(this Tabs tabs, IEnumerable<string> titles)
        => tabs.AddTitles(titles.Select(t => new Spans(t)));
}
