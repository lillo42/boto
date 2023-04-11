using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;

namespace Boto.Widget;

public static class BlockExtensions
{
    #region Title

    /// <summary>
    /// Change the title of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <param name="title">The title.</param>
    /// <returns>The <paramref name="block"/> with the given <paramref name="title"/>.</returns>
    public static Block Title(this Block block, Spans title)
    {
        block.Title = title;
        return block;
    }

    /// <summary>
    /// Change the title of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <param name="title">The title.</param>
    /// <returns>The <paramref name="block"/> with the given <paramref name="title"/>.</returns>
    public static Block Title(this Block block, string title)
        => block.Title(new Spans(title));

    /// <summary>
    /// Change the title of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <param name="title">The title.</param>
    /// <param name="style">The title <see cref="Styles.Style"/>.</param>
    /// <returns>The <paramref name="block"/> with the given <paramref name="title"/> and <paramref name="style"/>.</returns>
    public static Block Title(this Block block, string title, Style style)
        => block.Title(new Spans(title, style));

    /// <summary>
    /// Change the title of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <param name="title">The title.</param>
    /// <returns>The <paramref name="block"/> with the given <paramref name="title"/>.</returns>
    public static Block Title(this Block block, IEnumerable<Span> title)
        => block.Title(new Spans(title.ToList()));

    /// <summary>
    /// Change the title of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <param name="title">The title.</param>
    /// <returns>The <paramref name="block"/> with the given <paramref name="title"/>.</returns>
    public static Block Title(this Block block, params Span[] title)
        => block.Title(new Spans(title.ToList()));

    #endregion

    #region Text Alignment

    /// <summary>
    /// Change the text alignment of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <param name="alignment">The <see cref="Alignment"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.TitleAlignment"/> as <paramref name="alignment"/>.</returns>
    public static Block TitleAlignment(this Block block, Alignment alignment)
    {
        block.TitleAlignment = alignment;
        return block;
    }

    /// <summary>
    /// Change the text alignment of the block to <see cref="Alignment.Left"/>.
    /// </summary>
    /// <param name="block">The target block.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.TitleAlignment"/> as <see cref="Alignment.Left"/>.</returns>
    public static Block LeftTitleAlignment(this Block block)
        => block.TitleAlignment(Alignment.Left);

    /// <summary>
    /// Change the text alignment of the block to <see cref="Alignment.Right"/>.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.TitleAlignment"/> as <see cref="Alignment.Right"/>.</returns>
    public static Block RightTitleAlignment(this Block block)
        => block.TitleAlignment(Alignment.Right);

    /// <summary>
    /// Change the text alignment of the block to <see cref="Alignment.Center"/>.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.TitleAlignment"/> as <see cref="Alignment.Center"/>.</returns>
    public static Block CenterTitleAlignment(this Block block)
        => block.TitleAlignment(Alignment.Center);

    #endregion

    #region Borders

    /// <summary>
    /// Change the borders of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <param name="borders">The <see cref="Widget.Borders"/>.</param>
    /// <returns>The <paramref name="block"/> with given <paramref name="borders"/>.</returns>
    public static Block Borders(this Block block, Borders borders)
    {
        block.Borders = borders;
        return block;
    }

    /// <summary>
    /// Change the borders of the block to <see cref="Widget.Borders.None"/>.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.Borders"/> as <see cref="Widget.Borders.None"/>.</returns>
    public static Block NoneBorders(this Block block)
        => block.Borders(Widget.Borders.None);

    /// <summary>
    /// Change the borders of the block to <see cref="Widget.Borders.Top"/>.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.Borders"/> as <see cref="Widget.Borders.Top"/>.</returns>
    public static Block TopBorders(this Block block)
        => block.Borders(Widget.Borders.Top);

    /// <summary>
    /// Change the borders of the block to <see cref="Widget.Borders.Right"/>.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.Borders"/> as <see cref="Widget.Borders.Right"/>.</returns>
    public static Block RightBorders(this Block block)
        => block.Borders(Widget.Borders.Right);

    /// <summary>
    /// Change the borders of the block to <see cref="Widget.Borders.Bottom"/>.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.Borders"/> as <see cref="Widget.Borders.Bottom"/>.</returns>
    public static Block BottomBorders(this Block block)
        => block.Borders(Widget.Borders.Bottom);

    /// <summary>
    /// Change the borders of the block to <see cref="Widget.Borders.Left"/>.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.Borders"/> as <see cref="Widget.Borders.Left"/>.</returns>
    public static Block LeftBorders(this Block block)
        => block.Borders(Widget.Borders.Left);

    /// <summary>
    /// Change the borders of the block to <see cref="Widget.Borders.All"/>.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.Borders"/> as <see cref="Widget.Borders.All"/>.</returns>
    public static Block AllBorders(this Block block)
        => block.Borders(Widget.Borders.All);

    #endregion

    #region Border Style

    /// <summary>
    /// Change the border style of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <param name="style">The <see cref="Styles.Style"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.BorderStyle"/> as <paramref name="style"/>.</returns>
    public static Block BorderStyle(this Block block, Style style)
    {
        block.BorderStyle = style;
        return block;
    }

    #endregion

    #region Border Type

    /// <summary>
    /// Change the border type of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <param name="borderType">The <see cref="Widget.BorderType"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.BorderType"/> as <paramref name="borderType"/>.</returns>
    public static Block BorderType(this Block block, BorderType borderType)
    {
        block.BorderType = borderType;
        return block;
    }

    /// <summary>
    /// Change the border type of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.BorderType"/> as <see cref="Widget.BorderType.Double"/>.</returns>
    public static Block DoubleBorderType(this Block block)
        => block.BorderType(Widget.BorderType.Double);

    /// <summary>
    /// Change the border type of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.BorderType"/> as <see cref="Widget.BorderType.Rounded"/>.</returns>
    public static Block RoundedBorderType(this Block block)
        => block.BorderType(Widget.BorderType.Rounded);

    /// <summary>
    /// Change the border type of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.BorderType"/> as <see cref="Widget.BorderType.Plain"/>.</returns>
    public static Block PlainBorderType(this Block block)
        => block.BorderType(Widget.BorderType.Plain);

    /// <summary>
    /// Change the border type of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.BorderType"/> as <see cref="Widget.BorderType.Thick"/>.</returns>
    public static Block ThickBorderType(this Block block)
        => block.BorderType(Widget.BorderType.Thick);

    #endregion

    #region Style

    /// <summary>
    /// Change the style of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <param name="style">The <see cref="Styles.Style"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.Style"/> as <paramref name="style"/>.</returns>
    public static Block Style(this Block block, Style style)
    {
        block.Style = style;
        return block;
    }

    #endregion
}
