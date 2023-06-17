using Boto.Styles;
using Boto.Texts;

namespace Boto.Widgets.Extensions;

/// <summary>
/// The <see cref="Tabs"/> extensions method.
/// </summary>
public static class TabsExtensions
{
    /// <summary>
    /// Change the <see cref="Tabs.Block"/>.
    /// </summary>
    /// <param name="tabs">The <see cref="Tabs"/>.</param>
    /// <param name="block">The <see cref="Block"/>.</param>
    /// <returns>The <paramref name="tabs"/> with <see cref="Tabs.Block"/> as <paramref name="block"/>.</returns>
    public static Tabs SetBlock(this Tabs tabs, Block block)
    {
        tabs.Block = block;
        return tabs;
    }

    /// <summary>
    /// Change the <see cref="Tabs.Style"/>.
    /// </summary>
    /// <param name="tabs">The <see cref="Tabs"/>.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The <paramref name="tabs"/> with <see cref="Tabs.Style"/> as <paramref name="style"/>.</returns>
    public static Tabs SetStyle(this Tabs tabs, Style style)
    {
        tabs.Style = style;
        return tabs;
    }

    /// <summary>
    /// Change the <see cref="Tabs.Selected"/>.
    /// </summary>
    /// <param name="tabs">The <see cref="Tabs"/>.</param>
    /// <param name="selected">The selected.</param>
    /// <returns>The <paramref name="tabs"/> with <see cref="Tabs.Selected"/> as <paramref name="selected"/>.</returns>
    public static Tabs SetSelected(this Tabs tabs, int selected)
    {
        tabs.Selected = selected;
        return tabs;
    }

    /// <summary>
    /// Change the <see cref="Tabs.HighlightStyle"/>.
    /// </summary>
    /// <param name="tabs">The <see cref="Tabs"/>.</param>
    /// <param name="highlightStyle">The highlight <see cref="Style"/>.</param>
    /// <returns>The <paramref name="tabs"/> with <see cref="Tabs.HighlightStyle"/> as <paramref name="highlightStyle"/>.</returns>
    public static Tabs SetHighlightStyle(this Tabs tabs, Style highlightStyle)
    {
        tabs.HighlightStyle = highlightStyle;
        return tabs;
    }

    /// <summary>
    /// Change the <see cref="Tabs.Divider"/>.
    /// </summary>
    /// <param name="tabs">The <see cref="Tabs"/>.</param>
    /// <param name="divider">The divider as <see cref="Span"/>.</param>
    /// <returns>The <paramref name="tabs"/> with <see cref="Tabs.Divider"/> as <paramref name="divider"/>.</returns>
    public static Tabs SetDivider(this Tabs tabs, Span divider)
    {
        tabs.Divider = divider;
        return tabs;
    }

    /// <summary>
    /// Change the <see cref="Tabs.Divider"/>.
    /// </summary>
    /// <param name="tabs">The <see cref="Tabs"/>.</param>
    /// <param name="divider">The divider.</param>
    /// <returns>The <paramref name="tabs"/> with <see cref="Tabs.Divider"/> as <paramref name="divider"/>.</returns>
    public static Tabs SetDivider(this Tabs tabs, string divider)
        => tabs.SetDivider(new Span(divider));

    /// <summary>
    /// Change the <see cref="Tabs.Divider"/>.
    /// </summary>
    /// <param name="tabs">The <see cref="Tabs"/>.</param>
    /// <param name="divider">The divider.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The <paramref name="tabs"/> with <see cref="Tabs.Divider"/> as <paramref name="divider"/>.</returns>
    public static Tabs SetDivider(this Tabs tabs, string divider, Style style)
        => tabs.SetDivider(new Span(divider, style));

    /// <summary>
    /// Add <paramref name="title"/> to <see cref="Tabs.Titles"/>.
    /// </summary>
    /// <param name="tabs">The <see cref="Tabs"/>.</param>
    /// <param name="title">The title as <see cref="Spans"/>.</param>
    /// <returns>The <paramref name="tabs"/> with <see cref="Tabs.Titles"/> plus <paramref name="title"/>.</returns>
    public static Tabs AddTitle(this Tabs tabs, Spans title)
    {
        tabs.Titles.Add(title);
        return tabs;
    }

    /// <summary>
    /// Add <paramref name="titles"/> to <see cref="Tabs.Titles"/>.
    /// </summary>
    /// <param name="tabs">The <see cref="Tabs"/>.</param>
    /// <param name="titles">The titles as <see cref="Spans"/>.</param>
    /// <returns>The <paramref name="tabs"/> with <see cref="Tabs.Titles"/> plus <paramref name="titles"/>.</returns>
    public static Tabs AddTitles(this Tabs tabs, IEnumerable<Spans> titles)
    {
        tabs.Titles.AddRange(titles);
        return tabs;
    }

    /// <summary>
    /// Add <paramref name="titles"/> to <see cref="Tabs.Titles"/>.
    /// </summary>
    /// <param name="tabs">The <see cref="Tabs"/>.</param>
    /// <param name="titles">The titles as <see cref="Spans"/>.</param>
    /// <returns>The <paramref name="tabs"/> with <see cref="Tabs.Titles"/> plus <paramref name="titles"/>.</returns>
    public static Tabs AddTitles(this Tabs tabs, params Spans[] titles)
    {
        tabs.Titles.AddRange(titles);
        return tabs;
    }

    /// <summary>
    /// Add <paramref name="title"/> to <see cref="Tabs.Titles"/>.
    /// </summary>
    /// <param name="tabs">The <see cref="Tabs"/>.</param>
    /// <param name="title">The titles.</param>
    /// <returns>The <paramref name="tabs"/> with <see cref="Tabs.Titles"/> plus <paramref name="title"/>.</returns>
    public static Tabs AddTitle(this Tabs tabs, string title)
        => tabs.AddTitle(new Spans(title));

    /// <summary>
    /// Add <paramref name="title"/> to <see cref="Tabs.Titles"/>.
    /// </summary>
    /// <param name="tabs">The <see cref="Tabs"/>.</param>
    /// <param name="title">The titles.</param>
    /// <param name="style">The title <see cref="Style"/>.</param>
    /// <returns>The <paramref name="tabs"/> with <see cref="Tabs.Titles"/> plus <paramref name="title"/>.</returns>
    public static Tabs AddTitle(this Tabs tabs, string title, Style style)
        => tabs.AddTitle(new Spans(title, style));

    /// <summary>
    /// Add <paramref name="titles"/> to <see cref="Tabs.Titles"/>.
    /// </summary>
    /// <param name="tabs">The <see cref="Tabs"/>.</param>
    /// <param name="titles">The titles.</param>
    /// <returns>The <paramref name="tabs"/> with <see cref="Tabs.Titles"/> plus <paramref name="titles"/>.</returns>
    public static Tabs AddTitles(this Tabs tabs, IEnumerable<string> titles)
        => tabs.AddTitles(titles.Select(t => new Spans(t)));

    /// <summary>
    /// Add <paramref name="titles"/> to <see cref="Tabs.Titles"/>.
    /// </summary>
    /// <param name="tabs">The <see cref="Tabs"/>.</param>
    /// <param name="titles">The titles.</param>
    /// <returns>The <paramref name="tabs"/> with <see cref="Tabs.Titles"/> plus <paramref name="titles"/>.</returns>
    public static Tabs AddTitles(this Tabs tabs, params string[] titles)
        => tabs.AddTitles(titles.Select(t => new Spans(t)));
}
