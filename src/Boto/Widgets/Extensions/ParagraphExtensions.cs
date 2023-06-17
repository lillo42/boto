using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;

namespace Boto.Widgets.Extensions;

/// <summary>
/// The <see cref="Paragraph"/> extensions method.
/// </summary>
public static class ParagraphExtensions
{
    /// <summary>
    /// Change the <see cref="Paragraph.Block"/>.
    /// </summary>
    /// <param name="paragraph">The <see cref="Paragraph"/>.</param>
    /// <param name="block">The <see cref="Block"/>.</param>
    /// <returns>The <paramref name="paragraph"/> with <see cref="Paragraph.Block"/> as <paramref name="block"/>.</returns>
    public static Paragraph SetBlock(this Paragraph paragraph, Block block)
    {
        paragraph.Block = block;
        return paragraph;
    }

    /// <summary>
    /// Change the <see cref="Paragraph.Style"/>.
    /// </summary>
    /// <param name="paragraph">The <see cref="Paragraph"/>.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The <paramref name="paragraph"/> with <see cref="Paragraph.Style"/> as <paramref name="style"/>.</returns>
    public static Paragraph SetStyle(this Paragraph paragraph, Style style)
    {
        paragraph.Style = style;
        return paragraph;
    }

    /// <summary>
    /// Change the <see cref="Paragraph.Text"/>.
    /// </summary>
    /// <param name="paragraph">The <see cref="Paragraph"/>.</param>
    /// <param name="text">The <see cref="Text"/>.</param>
    /// <returns>The <paramref name="paragraph"/> with <see cref="Paragraph.Text"/> as <paramref name="text"/>.</returns>
    public static Paragraph SetText(this Paragraph paragraph, Text text)
    {
        paragraph.Text = text;
        return paragraph;
    }

    /// <summary>
    /// Change the <see cref="Paragraph.Text"/>.
    /// </summary>
    /// <param name="paragraph">The <see cref="Paragraph"/>.</param>
    /// <param name="text">The <see cref="Text"/>.</param>
    /// <returns>The <paramref name="paragraph"/> with <see cref="Paragraph.Text"/> as <paramref name="text"/>.</returns>
    public static Paragraph SetText(this Paragraph paragraph, string text)
    {
        paragraph.Text = new Text(text);
        return paragraph;
    }

    /// <summary>
    /// Change the <see cref="Paragraph.Text"/>.
    /// </summary>
    /// <param name="paragraph">The <see cref="Paragraph"/>.</param>
    /// <param name="text">The <see cref="Text"/>.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The <paramref name="paragraph"/> with <see cref="Paragraph.Text"/> as <paramref name="text"/>.</returns>
    public static Paragraph SetText(this Paragraph paragraph, string text, Style style)
    {
        paragraph.Text = new Text(text, style);
        return paragraph;
    }

    /// <summary>
    /// Change the <see cref="Paragraph.Text"/>.
    /// </summary>
    /// <param name="paragraph">The <see cref="Paragraph"/>.</param>
    /// <param name="text">The <see cref="Text"/>.</param>
    /// <returns>The <paramref name="paragraph"/> with <see cref="Paragraph.Text"/> as <paramref name="text"/>.</returns>
    public static Paragraph SetText(this Paragraph paragraph, Spans text)
    {
        paragraph.Text = new Text(new List<Spans> { text });
        return paragraph;
    }

    /// <summary>
    /// Change the <see cref="Paragraph.Text"/>.
    /// </summary>
    /// <param name="paragraph">The <see cref="Paragraph"/>.</param>
    /// <param name="text">The <see cref="Text"/>.</param>
    /// <returns>The <paramref name="paragraph"/> with <see cref="Paragraph.Text"/> as <paramref name="text"/>.</returns>
    public static Paragraph SetText(this Paragraph paragraph, IEnumerable<Spans> text)
    {
        paragraph.Text = new Text(text.ToList());
        return paragraph;
    }

    /// <summary>
    /// Change the <see cref="Paragraph.Text"/>.
    /// </summary>
    /// <param name="paragraph">The <see cref="Paragraph"/>.</param>
    /// <param name="text">The <see cref="Text"/>.</param>
    /// <returns>The <paramref name="paragraph"/> with <see cref="Paragraph.Text"/> as <paramref name="text"/>.</returns>
    public static Paragraph SetText(this Paragraph paragraph, params Spans[] text)
    {
        paragraph.Text = new Text(text.ToList());
        return paragraph;
    }

    /// <summary>
    /// Change the <see cref="Paragraph.Text"/>.
    /// </summary>
    /// <param name="paragraph">The <see cref="Paragraph"/>.</param>
    /// <param name="text">The <see cref="Text"/>.</param>
    /// <returns>The <paramref name="paragraph"/> with <see cref="Paragraph.Text"/> as <paramref name="text"/>.</returns>
    public static Paragraph SetText(this Paragraph paragraph, IEnumerable<string> text)
    {
        paragraph.Text = new Text(text.Select(x => new Spans(x)).ToList());
        return paragraph;
    }

    /// <summary>
    /// Change the <see cref="Paragraph.Text"/>.
    /// </summary>
    /// <param name="paragraph">The <see cref="Paragraph"/>.</param>
    /// <param name="text">The <see cref="Text"/>.</param>
    /// <returns>The <paramref name="paragraph"/> with <see cref="Paragraph.Text"/> as <paramref name="text"/>.</returns>
    public static Paragraph SetText(this Paragraph paragraph, params string[] text)
    {
        paragraph.Text = new Text(text.Select(x => new Spans(x)).ToList());
        return paragraph;
    }

    /// <summary>
    /// Change the <see cref="Paragraph.Trim"/>.
    /// </summary>
    /// <param name="paragraph">The <see cref="Paragraph"/>.</param>
    /// <param name="trim">Flag indication if should trim..</param>
    /// <returns>The <paramref name="paragraph"/> with <see cref="Paragraph.Trim"/> as <paramref name="trim"/>.</returns>
    public static Paragraph SetTrim(this Paragraph paragraph, bool? trim)
    {
        paragraph.Trim = trim;
        return paragraph;
    }

    /// <summary>
    /// Enable the <see cref="Paragraph.Trim"/>.
    /// </summary>
    /// <param name="paragraph">The <see cref="Paragraph"/>.</param>
    /// <returns>The <paramref name="paragraph"/> with <see cref="Paragraph.Trim"/> as true.</returns>
    public static Paragraph EnableTrim(this Paragraph paragraph)
    {
        paragraph.Trim = true;
        return paragraph;
    }

    /// <summary>
    /// Disable the <see cref="Paragraph.Trim"/>.
    /// </summary>
    /// <param name="paragraph">The <see cref="Paragraph"/>.</param>
    /// <returns>The <paramref name="paragraph"/> with <see cref="Paragraph.Trim"/> as false.</returns>
    public static Paragraph DisableTrim(this Paragraph paragraph)
    {
        paragraph.Trim = false;
        return paragraph;
    }

    /// <summary>
    /// Change the <see cref="Paragraph.Scroll"/>.
    /// </summary>
    /// <param name="paragraph">The <see cref="Paragraph"/>.</param>
    /// <param name="scroll">The scroll.</param>
    /// <returns>The <paramref name="paragraph"/> with <see cref="Paragraph.Scroll"/> as <paramref name="scroll"/>.</returns>
    public static Paragraph SetScroll(this Paragraph paragraph, (int, int) scroll)
    {
        paragraph.Scroll = scroll;
        return paragraph;
    }

    /// <summary>
    /// Change the <see cref="Paragraph.Scroll"/>.
    /// </summary>
    /// <param name="paragraph">The <see cref="Paragraph"/>.</param>
    /// <param name="alignment">The <see cref="Alignment"/>.</param>
    /// <returns>The <paramref name="paragraph"/> with <see cref="Paragraph.Alignment"/> as <paramref name="alignment"/>.</returns>
    public static Paragraph SetAlignment(this Paragraph paragraph, Alignment alignment)
    {
        paragraph.Alignment = alignment;
        return paragraph;
    }
}
