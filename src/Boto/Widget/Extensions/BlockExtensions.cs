using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;

namespace Boto.Widget;

public static class BlockExtensions
{
    #region Title

    public static Block Title(this Block block, Spans spans)
    {
        block.Title = spans;
        return block;
    }

    public static Block Title(this Block block, string title)
        => block.Title(new Spans(title));
    
    public static Block Title(this Block block, string title, Style style)
        => block.Title(new Spans(title, style));

    public static Block Title(this Block block, IEnumerable<Span> spans)
        => block.Title(new Spans(spans.ToList()));
    
    public static Block Title(this Block block, params Span[] spans)
        => block.Title(new Spans(spans.ToList()));

    #endregion

    #region Text Alignment

    public static Block TitleAlignment(this Block block, Alignment alignment)
    {
        block.TitleAlignment = alignment;
        return block;
    }

    public static Block LeftTitleAlignment(this Block block)
        => block.TitleAlignment(Alignment.Left);

    public static Block RightTitleAlignment(this Block block)
        => block.TitleAlignment(Alignment.Right);

    public static Block CenterTitleAlignment(this Block block)
        => block.TitleAlignment(Alignment.Center);

    #endregion

    #region Borders

    public static Block Borders(this Block block, Borders borders)
    {
        block.Borders = borders;
        return block;
    }

    public static Block NoneBorders(this Block block)
        => block.Borders(Widget.Borders.None);

    public static Block TopBorders(this Block block)
        => block.Borders(Widget.Borders.Top);

    public static Block RightBorders(this Block block)
        => block.Borders(Widget.Borders.Right);

    public static Block BottomBorders(this Block block)
        => block.Borders(Widget.Borders.Bottom);

    public static Block LeftBorders(this Block block)
        => block.Borders(Widget.Borders.Left);

    public static Block AllBorders(this Block block)
        => block.Borders(Widget.Borders.All);

    #endregion

    #region Border Style

    public static Block BorderStyle(this Block block, Style style)
    {
        block.BorderStyle = style;
        return block;
    }

    #endregion

    #region Border Type

    public static Block BorderType(this Block block, BorderType borderType)
    {
        block.BorderType = borderType;
        return block;
    }
    
    public static Block DoubleBorderType(this Block block)
        => block.BorderType(Widget.BorderType.Double);
    
    public static Block RoundedBorderType(this Block block)
        => block.BorderType(Widget.BorderType.Rounded);
    
    public static Block PlainBorderType(this Block block)
        => block.BorderType(Widget.BorderType.Plain);
    
    public static Block ThickBorderType(this Block block)
        => block.BorderType(Widget.BorderType.Thick);
    
    #endregion

    #region Style
    
    public static Block Style(this Block block, Style style)
    {
        block.Style = style;
        return block;
    }

    #endregion
}
