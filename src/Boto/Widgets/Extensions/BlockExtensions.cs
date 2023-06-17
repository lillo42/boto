using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;

namespace Boto.Widgets.Extensions;

/// <summary>
/// The <see cref="Block"/> extensions.
/// </summary>
public static class BlockExtensions
{
    #region Title

    /// <summary>
    /// Change the title of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <param name="title">The title.</param>
    /// <returns>The <paramref name="block"/> with the given <paramref name="title"/>.</returns>
    public static Block SetTitle(this Block block, Spans title)
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
    public static Block SetTitle(this Block block, string title)
        => block.SetTitle(new Spans(title));

    /// <summary>
    /// Change the title of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <param name="title">The title.</param>
    /// <param name="style">The title <see cref="Styles.Style"/>.</param>
    /// <returns>The <paramref name="block"/> with the given <paramref name="title"/> and <paramref name="style"/>.</returns>
    public static Block SetTitle(this Block block, string title, Style style)
        => block.SetTitle(new Spans(title, style));

    /// <summary>
    /// Change the title of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <param name="title">The title.</param>
    /// <returns>The <paramref name="block"/> with the given <paramref name="title"/>.</returns>
    public static Block SetTitle(this Block block, IEnumerable<Span> title)
        => block.SetTitle(new Spans(title.ToList()));

    /// <summary>
    /// Change the title of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <param name="title">The title.</param>
    /// <returns>The <paramref name="block"/> with the given <paramref name="title"/>.</returns>
    public static Block SetTitle(this Block block, params Span[] title)
        => block.SetTitle(new Spans(title.ToList()));

    #endregion

    #region Text Alignment

    /// <summary>
    /// Change the text alignment of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <param name="alignment">The <see cref="Alignment"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.TitleAlignment"/> as <paramref name="alignment"/>.</returns>
    public static Block SetTitleAlignment(this Block block, Alignment alignment)
    {
        block.TitleAlignment = alignment;
        return block;
    }

    #endregion

    #region Borders

    /// <summary>
    /// Change the borders of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <param name="borders">The <see cref="Widgets.Borders"/>.</param>
    /// <returns>The <paramref name="block"/> with given <paramref name="borders"/>.</returns>
    public static Block SetBorders(this Block block, Borders borders)
    {
        block.Borders = borders;
        return block;
    }

    #endregion

    #region Border Style

    /// <summary>
    /// Change the border style of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <param name="style">The <see cref="Styles.Style"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.BorderStyle"/> as <paramref name="style"/>.</returns>
    public static Block SetBorderStyle(this Block block, Style style)
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
    /// <param name="borderType">The <see cref="Widgets.BorderType"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.BorderType"/> as <paramref name="borderType"/>.</returns>
    public static Block SetBorderType(this Block block, BorderType borderType)
    {
        block.BorderType = borderType;
        return block;
    }

    #endregion

    #region Style

    /// <summary>
    /// Change the style of the block.
    /// </summary>
    /// <param name="block">The target <see cref="Block"/>.</param>
    /// <param name="style">The <see cref="Styles.Style"/>.</param>
    /// <returns>The <paramref name="block"/> with <see cref="Block.Style"/> as <paramref name="style"/>.</returns>
    public static Block SetStyle(this Block block, Style style)
    {
        block.Style = style;
        return block;
    }

    #endregion
}
