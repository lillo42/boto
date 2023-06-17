using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;

namespace Boto.Widgets.Extensions;

/// <summary>
/// The <see cref="List"/> extensions method.
/// </summary>
public static class ListExtensions
{
    /// <summary>
    /// Change the <see cref="List.Block"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="block">The <see cref="Block"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Block"/> as <paramref name="block"/>.</returns>
    public static List SetBlock(this List list, Block block)
    {
        list.Block = block;
        return list;
    }

    /// <summary>
    /// Change the <see cref="List.Block"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Style"/> as <paramref name="style"/>.</returns>
    public static List SetStyle(this List list, Style style)
    {
        list.Style = style;
        return list;
    }

    /// <summary>
    /// Change the <see cref="List.StartCorner"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="corner">The <see cref="Corner"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.StartCorner"/> as <paramref name="corner"/>.</returns>
    public static List SetStartCorner(this List list, Corner corner)
    {
        list.StartCorner = corner;
        return list;
    }

    /// <summary>
    /// Change the <see cref="List.HighlightSymbol"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.HighlightStyle"/> as <paramref name="style"/>.</returns>
    public static List SetHighlightStyle(this List list, Style style)
    {
        list.HighlightStyle = style;
        return list;
    }

    /// <summary>
    /// Change the <see cref="List.HighlightSymbol"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="symbol">The symbol.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.HighlightSymbol"/> as <paramref name="symbol"/>.</returns>
    public static List SetHighlightSymbol(this List list, string symbol)
    {
        list.HighlightSymbol = symbol;
        return list;
    }

    /// <summary>
    /// Change the <see cref="List.RepeatHighlightSymbol"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="repeat">The flag indicating is should repeat.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.HighlightSymbol"/> as <paramref name="repeat"/>.</returns>
    public static List SetRepeatHighlightSymbol(this List list, bool repeat)
    {
        list.RepeatHighlightSymbol = repeat;
        return list;
    }

    /// <summary>
    /// Enable the <see cref="List.RepeatHighlightSymbol"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.RepeatHighlightSymbol"/> as true.</returns>
    public static List EnableRepeatHighlightSymbol(this List list)
    {
        list.RepeatHighlightSymbol = true;
        return list;
    }

    /// <summary>
    /// Disable the <see cref="List.RepeatHighlightSymbol"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.RepeatHighlightSymbol"/> as false.</returns>
    public static List DisableRepeatHighlightSymbol(this List list)
    {
        list.RepeatHighlightSymbol = false;
        return list;
    }

    /// <summary>
    /// Add a <see cref="ListItem"/> to the <see cref="List"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="item">The <see cref="ListItem"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Items"/> plus <paramref name="item"/>.</returns>
    public static List AddItem(this List list, ListItem item)
    {
        list.Items.Add(item);
        return list;
    }

    /// <summary>
    /// Add a <see cref="ListItem"/> to the <see cref="List"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="item">The item value.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Items"/> plus <paramref name="item"/>.</returns>
    public static List AddItem(this List list, string item)
        => list.AddItem(new ListItem(new Text(item)));

    /// <summary>
    /// Add a <see cref="ListItem"/> to the <see cref="List"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="item">The item value.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Items"/> plus <paramref name="item"/>.</returns>
    public static List AddItem(this List list, string item, Style style)
        => list.AddItem(new ListItem(new Text(item), style));

    /// <summary>
    /// Add a <see cref="ListItem"/> to the <see cref="List"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="item">The item value.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Items"/> plus <paramref name="item"/>.</returns>
    public static List AddItem(this List list, Text item, Style style)
        => list.AddItem(new ListItem(item, style));

    /// <summary>
    /// Add a <see cref="ListItem"/> to the <see cref="List"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="item">The item value.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Items"/> plus <paramref name="item"/>.</returns>
    public static List AddItem(this List list, Text item)
        => list.AddItem(new ListItem(item));


    /// <summary>
    /// Add a <see cref="ListItem"/> to the <see cref="List"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="items">The collection of <see cref="Spans"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Items"/> plus <paramref name="items"/>.</returns>
    public static List AddItem(this List list, IEnumerable<Spans> items)
        => list.AddItem(new ListItem(new Text(items.ToList())));

    /// <summary>
    /// Add a <see cref="ListItem"/> to the <see cref="List"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="items">The collection of <see cref="ListItem"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Items"/> plus <paramref name="items"/>.</returns>
    public static List AddItems(this List list, params Spans[] items)
    {
        list.Items.Add(new ListItem(items));
        return list;
    }

    /// <summary>
    /// Add a <see cref="ListItem"/> to the <see cref="List"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="items">The collection of <see cref="ListItem"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Items"/> plus <paramref name="items"/>.</returns>
    public static List AddItems(this List list, IEnumerable<ListItem> items)
    {
        list.Items.AddRange(items);
        return list;
    }

    /// <summary>
    /// Add a <see cref="ListItem"/> to the <see cref="List"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="items">The collection of <see cref="ListItem"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Items"/> plus <paramref name="items"/>.</returns>
    public static List AddItems(this List list, params ListItem[] items)
    {
        list.Items.AddRange(items);
        return list;
    }


    /// <summary>
    /// Add a <see cref="ListItem"/> to the <see cref="List"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="items">The collection of <see cref="ListItem"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Items"/> plus <paramref name="items"/>.</returns>
    public static List AddItems(this List list, IEnumerable<string> items)
    {
        foreach (var item in items)
        {
            list.AddItem(item);
        }

        return list;
    }

    /// <summary>
    /// Add a <see cref="ListItem"/> to the <see cref="List"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="items">The collection of <see cref="ListItem"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Items"/> plus <paramref name="items"/>.</returns>
    public static List AddItems(this List list, params string[] items)
    {
        foreach (var item in items)
        {
            list.AddItem(item);
        }

        return list;
    }

    /// <summary>
    /// Add a <see cref="ListItem"/> to the <see cref="List"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="items">The collection of <see cref="ListItem"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Items"/> plus <paramref name="items"/>.</returns>
    public static List AddItems(this List list, IEnumerable<(string, Style)> items)
    {
        foreach (var (content, style) in items)
        {
            list.AddItem(content, style);
        }

        return list;
    }

    /// <summary>
    /// Add a <see cref="ListItem"/> to the <see cref="List"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="items">The collection of <see cref="ListItem"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Items"/> plus <paramref name="items"/>.</returns>
    public static List AddItems(this List list, params (string, Style)[] items)
    {
        foreach (var (content, style) in items)
        {
            list.AddItem(content, style);
        }

        return list;
    }

    /// <summary>
    /// Add a <see cref="ListItem"/> to the <see cref="List"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="items">The collection of <see cref="ListItem"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Items"/> plus <paramref name="items"/>.</returns>
    public static List AddItems(this List list, IEnumerable<IEnumerable<Spans>> items)
    {
        foreach (var spans in items)
        {
            list.AddItem(spans);
        }

        return list;
    }

    /// <summary>
    /// Add a <see cref="ListItem"/> to the <see cref="List"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="items">The collection of <see cref="ListItem"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Items"/> plus <paramref name="items"/>.</returns>
    public static List AddItems(this List list, params IEnumerable<Spans>[] items)
    {
        foreach (var spans in items)
        {
            list.AddItem(spans);
        }

        return list;
    }

    /// <summary>
    /// Add a <see cref="ListItem"/> to the <see cref="List"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="items">The collection of <see cref="ListItem"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Items"/> plus <paramref name="items"/>.</returns>
    public static List AddItems(this List list, IEnumerable<Text> items)
    {
        foreach (var item in items)
        {
            list.AddItem(item);
        }

        return list;
    }

    /// <summary>
    /// Add a <see cref="ListItem"/> to the <see cref="List"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="items">The collection of <see cref="ListItem"/>.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Items"/> plus <paramref name="items"/>.</returns>
    public static List AddItems(this List list, params Text[] items)
    {
        foreach (var spans in items)
        {
            list.AddItem(spans);
        }

        return list;
    }

    /// <summary>
    /// Add a <see cref="ListItem"/> to the <see cref="List"/>.
    /// </summary>
    /// <param name="list">The <see cref="List"/>.</param>
    /// <param name="items">The collection of <typeparamref name="T"/>.</param>
    /// <param name="map">The map function.</param>
    /// <returns>The <paramref name="list"/> with the <see cref="List.Items"/> plus <paramref name="items"/>.</returns>
    public static List AddItems<T>(this List list, IEnumerable<T> items, Func<T, ListItem> map)
    {
        list.Items.AddRange(items.Select(map));
        return list;
    }
}
