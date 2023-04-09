using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;

namespace Boto.Widget;

public static class ListExtensions
{
    public static List Block(this List list, Block block)
    {
        list.Block = block;
        return list;
    }

    public static List NoBlock(this List list)
    {
        list.Block = null;
        return list;
    }

    public static List Style(this List list, Style style)
    {
        list.Style = style;
        return list;
    }

    public static List StartCorner(this List list, Corner corner)
    {
        list.StartCorner = corner;
        return list;
    }

    public static List StartTopLeft(this List list)
        => list.StartCorner(Corner.TopLeft);

    public static List StartTopRight(this List list)
        => list.StartCorner(Corner.TopRight);

    public static List StartBottomLeft(this List list)
        => list.StartCorner(Corner.BottomLeft);

    public static List StartBottomRight(this List list)
        => list.StartCorner(Corner.BottomRight);

    public static List HighlightStyle(this List list, Style style)
    {
        list.HighlightStyle = style;
        return list;
    }

    public static List HighlightSymbol(this List list, string symbol)
    {
        list.HighlightSymbol = symbol;
        return list;
    }

    public static List NoHighlightSymbol(this List list)
    {
        list.HighlightSymbol = null;
        return list;
    }

    public static List RepeatHighlightSymbol(this List list, bool repeat)
    {
        list.RepeatHighlightSymbol = repeat;
        return list;
    }

    public static List EnableRepeatHighlightSymbol(this List list)
        => list.RepeatHighlightSymbol(true);

    public static List DisableRepeatHighlightSymbol(this List list)
        => list.RepeatHighlightSymbol(false);

    public static List AddItem(this List list, ListItem item)
    {
        list.Items.Add(item);
        return list;
    }

    public static List AddItem(this List list, string content)
        => list.AddItem(new ListItem(new Text(content)));

    public static List AddItem(this List list, string content, Style style)
        => list.AddItem(new ListItem(new Text(content), style));

    public static List AddItem(this List list, List<Spans> content)
        => list.AddItem(new ListItem(new Text(content)));

    public static List AddItems(this List list, IEnumerable<ListItem> items)
    {
        list.Items.AddRange(items);
        return list;
    }

    public static List AddItems(this List list, params ListItem[] items)
    {
        list.Items.AddRange(items);
        return list;
    }

    public static List AddItems(this List list, IEnumerable<string> items)
    {
        foreach (var item in items)
        {
            list.AddItem(item);
        }

        return list;
    }

    public static List AddItems(this List list, IEnumerable<(string, Style)> items)
    {
        foreach (var (content, style) in items)
        {
            list.AddItem(content, style);
        }

        return list;
    }

    public static List AddItems(this List list, IEnumerable<List<Spans>> items)
    {
        foreach (var spans in items)
        {
            list.AddItem(spans);
        }

        return list;
    }
    
    public static List AddItems<T>(this List list, IEnumerable<T> items, Func<T, ListItem> map)
    {
        list.Items.AddRange(items.Select(map));
        return list;
    }
}
