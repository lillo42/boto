using Boto.Layouts;
using Boto.Styles;

namespace Boto.Widgets.Extensions;

/// <summary>
/// The <see cref="Table"/> extensions method.
/// </summary>
public static class TableExtensions
{
    /// <summary>
    /// Change the <see cref="Table.Block"/>.
    /// </summary>
    /// <param name="table">The <see cref="Table"/>.</param>
    /// <param name="block">The <see cref="Block"/>/</param>
    /// <returns>The <paramref name="table"/> with <see cref="Table.Block"/> as <paramref name="block"/>.</returns>
    public static Table SetBlock(this Table table, Block block)
    {
        table.Block = block;
        return table;
    }

    /// <summary>
    /// Change the <see cref="Table.Style"/>.
    /// </summary>
    /// <param name="table">The <see cref="Table"/>.</param>
    /// <param name="style">The <see cref="Style"/>/</param>
    /// <returns>The <paramref name="table"/> with <see cref="Table.Style"/> as <paramref name="style"/>.</returns>
    public static Table SetStyle(this Table table, Style style)
    {
        table.Style = style;
        return table;
    }

    /// <summary>
    /// Change the <see cref="Table.ColumnSpacing"/>.
    /// </summary>
    /// <param name="table">The <see cref="Table"/>.</param>
    /// <param name="columnSpacing">The column spacing.</param>
    /// <returns>The <paramref name="table"/> with <see cref="Table.ColumnSpacing"/> as <paramref name="columnSpacing"/>.</returns>
    public static Table SetColumnSpacing(this Table table, int columnSpacing)
    {
        table.ColumnSpacing = columnSpacing;
        return table;
    }

    /// <summary>
    /// Change the <see cref="Table.HighlightStyle"/>.
    /// </summary>
    /// <param name="table">The <see cref="Table"/>.</param>
    /// <param name="highlightStyle">The high <see cref="Style"/>.</param>
    /// <returns>The <paramref name="table"/> with <see cref="Table.HighlightStyle"/> as <paramref name="highlightStyle"/>.</returns>
    public static Table SetHighlightStyle(this Table table, Style highlightStyle)
    {
        table.HighlightStyle = highlightStyle;
        return table;
    }

    /// <summary>
    /// Change the <see cref="Table.HighlightSymbol"/>.
    /// </summary>
    /// <param name="table">The <see cref="Table"/>.</param>
    /// <param name="highlightSymbol">The highlight symbol.</param>
    /// <returns>The <paramref name="table"/> with <see cref="Table.HighlightSymbol"/> as <paramref name="highlightSymbol"/>.</returns>
    public static Table HighlightSymbol(this Table table, string? highlightSymbol)
    {
        table.HighlightSymbol = highlightSymbol;
        return table;
    }

    /// <summary>
    /// Add a <see cref="IConstraint"/> to the <see cref="Table.Widths"/>.
    /// </summary>
    /// <param name="table">The <see cref="Table"/>.</param>
    /// <param name="constraint">The <see cref="IConstraint"/>.</param>
    /// <returns>The <paramref name="table"/> with <see cref="Table.Widths"/> plus <paramref name="constraint"/>.</returns>
    public static Table AddConstraint(this Table table, IConstraint constraint)
    {
        table.Widths.Add(constraint);
        return table;
    }

    /// <summary>
    /// Add a <see cref="IConstraint"/> to the <see cref="Table.Widths"/>.
    /// </summary>
    /// <param name="table">The <see cref="Table"/>.</param>
    /// <param name="constraints">The <see cref="IConstraint"/> collection.</param>
    /// <returns>The <paramref name="table"/> with <see cref="Table.Widths"/> plus <paramref name="constraints"/> collection.</returns>
    public static Table AddConstraints(this Table table, IEnumerable<IConstraint> constraints)
    {
        table.Widths.AddRange(constraints);
        return table;
    }

    /// <summary>
    /// Add a <see cref="IConstraint"/> to the <see cref="Table.Widths"/>.
    /// </summary>
    /// <param name="table">The <see cref="Table"/>.</param>
    /// <param name="constraints">The <see cref="IConstraint"/> collection.</param>
    /// <returns>The <paramref name="table"/> with <see cref="Table.Widths"/> plus <paramref name="constraints"/> collection.</returns>
    public static Table AddConstraints(this Table table, params IConstraint[] constraints)
    {
        table.Widths.AddRange(constraints);
        return table;
    }

    /// <summary>
    /// Change the <see cref="Table.Header"/>.
    /// </summary>
    /// <param name="table">The <see cref="Table"/>.</param>
    /// <param name="headers">The header <see cref="Row"/>.</param>
    /// <returns>The <paramref name="table"/> with <see cref="Table.Header"/> as <paramref name="headers"/>.</returns>
    public static Table SetHeaders(this Table table, Row? headers)
    {
        table.Header = headers;
        return table;
    }

    /// <summary>
    /// Add a <see cref="Row"/> to the <see cref="Table.Rows"/>.
    /// </summary>
    /// <param name="table">The <see cref="Table"/>.</param>
    /// <param name="row">The <see cref="Row"/>.</param>
    /// <returns>The <paramref name="table"/> with <see cref="Table.Rows"/> plus <paramref name="row"/>.</returns>
    public static Table AddRow(this Table table, Row row)
    {
        table.Rows.Add(row);
        return table;
    }

    /// <summary>
    /// Add a <see cref="Row"/> to the <see cref="Table.Rows"/>.
    /// </summary>
    /// <param name="table">The <see cref="Table"/>.</param>
    /// <param name="rows">The <see cref="Row"/> collection.</param>
    /// <returns>The <paramref name="table"/> with <see cref="Table.Rows"/> plus <paramref name="rows"/>.</returns>
    public static Table AddRows(this Table table, IEnumerable<Row> rows)
    {
        table.Rows.AddRange(rows);
        return table;
    }

    /// <summary>
    /// Add a <see cref="Row"/> to the <see cref="Table.Rows"/>.
    /// </summary>
    /// <param name="table">The <see cref="Table"/>.</param>
    /// <param name="rows">The <see cref="Row"/> collection.</param>
    /// <returns>The <paramref name="table"/> with <see cref="Table.Rows"/> plus <paramref name="rows"/>.</returns>
    public static Table AddRows(this Table table, params Row[] rows)
    {
        table.Rows.AddRange(rows);
        return table;
    }

    /// <summary>
    /// Add a <see cref="Row"/> to the <see cref="Table.Rows"/>.
    /// </summary>
    /// <param name="table">The <see cref="Table"/>.</param>
    /// <param name="items">The items.</param>
    /// <param name="map">The map function.</param>
    /// <returns>The <paramref name="table"/> with <see cref="Table.Rows"/> plus <paramref name="items"/>.</returns>
    public static Table AddRows<T>(this Table table, IEnumerable<T> items, Func<T, Row> map)
    {
        table.Rows.AddRange(items.Select(map));
        return table;
    }
}
