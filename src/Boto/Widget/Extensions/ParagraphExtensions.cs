using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;

namespace Boto.Widget;

public static class ParagraphExtensions
{
    public static Paragraph Block(this Paragraph paragraph, Block block)
    {
        paragraph.Block = block;
        return paragraph;
    }

    public static Paragraph Style(this Paragraph paragraph, Style style)
    {
        paragraph.Style = style;
        return paragraph;
    }

    public static Paragraph Text(this Paragraph paragraph, Text text)
    {
        paragraph.Text = text;
        return paragraph;
    }

    public static Paragraph Text(this Paragraph paragraph, string text)
    {
        paragraph.Text = new Text(text);
        return paragraph;
    }

    public static Paragraph Text(this Paragraph paragraph, string text, Style style)
    {
        paragraph.Text = new Text(text, style);
        return paragraph;
    }

    public static Paragraph Text(this Paragraph paragraph, Spans spans)
    {
        paragraph.Text = new Text(new List<Spans> { spans });
        return paragraph;
    }
    
    public static Paragraph Text(this Paragraph paragraph, IEnumerable<Spans> spans)
    {
        paragraph.Text = new Text(spans.ToList());
        return paragraph;
    }
    
    public static Paragraph Text(this Paragraph paragraph, params Spans[] spans)
    {
        paragraph.Text = new Text(spans.ToList());
        return paragraph;
    }

    public static Paragraph Trim(this Paragraph paragraph, bool trim)
    {
        paragraph.Trim = trim;
        return paragraph;
    }

    public static Paragraph EnableTrim(this Paragraph paragraph)
    {
        paragraph.Trim = true;
        return paragraph;
    }

    public static Paragraph DisableTrim(this Paragraph paragraph)
    {
        paragraph.Trim = false;
        return paragraph;
    }

    public static Paragraph NoTrim(this Paragraph paragraph)
    {
        paragraph.Trim = null;
        return paragraph;
    }

    public static Paragraph Scroll(this Paragraph paragraph, (int, int) scroll)
    {
        paragraph.Scroll = scroll;
        return paragraph;
    }

    public static Paragraph Alignment(this Paragraph paragraph, Alignment alignment)
    {
        paragraph.Alignment = alignment;
        return paragraph;
    }
}
