using Boto.Layouts;
using Boto.Styles;

namespace Boto.Widget;

public static class TableExtensions
{
    public static Table Block(this Table table, Block block)
    {
        table.Block = block;
        return table;
    }

    public static Table Style(this Table table, Style style)
    {
        table.Style = style;
        return table;
    }

    public static Table ColumnSpacing(this Table table, int columnSpacing)
    {
        table.ColumnSpacing = columnSpacing;
        return table;
    }

    public static Table HighlightStyle(this Table table, Style highlightStyle)
    {
        table.HighlightStyle = highlightStyle;
        return table;
    }

    public static Table HighlightSymbol(this Table table, string highlightSymbol)
    {
        table.HighlightSymbol = highlightSymbol;
        return table;
    }

    public static Table NoHighlightSymbol(this Table table)
    {
        table.HighlightSymbol = null;
        return table;
    }

    public static Table AddConstraint(this Table table, IConstraint constraint)
    {
        table.Widths.Add(constraint);
        return table;
    }

    public static Table AddConstraints(this Table table, IEnumerable<IConstraint> constraints)
    {
        table.Widths.AddRange(constraints);
        return table;
    }

    public static Table AddConstraints(this Table table, params IConstraint[] constraints)
    {
        table.Widths.AddRange(constraints);
        return table;
    }

    public static Table NoHeaders(this Table table)
    {
        table.Header = null;
        return table;
    }

    public static Table Headers(this Table table, Row headers)
    {
        table.Header = headers;
        return table;
    }
    
    public static Table AddRow(this Table table, Row row)
    {
        table.Rows.Add(row);
        return table;
    }
    
    public static Table AddRows(this Table table, IEnumerable<Row> rows)
    {
        table.Rows.AddRange(rows);
        return table;
    }
    
    public static Table AddRows(this Table table, params Row[] rows)
    {
        table.Rows.AddRange(rows);
        return table;
    }
    
    public static Table AddRows<T>(this Table table, IEnumerable<T> items, Func<T, Row> map)
    {
        table.Rows.AddRange(items.Select(map));
        return table;
    }
}
