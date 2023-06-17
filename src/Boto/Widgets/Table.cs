using Boto.Extensions;
using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widgets;

/// <summary>
/// The buffer cells.
/// </summary>
/// <param name="Content">The <see cref="Text"/>.</param>
/// <param name="Style">The <see cref="Styles.Style"/>.</param>
public record Cell(Text Content, Style Style)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> class.
    /// </summary>
    /// <param name="content">The <see cref="Text"/>.</param>
    public Cell(Text content)
        : this(content, new())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> class.
    /// </summary>
    /// <param name="spans">The <see cref="List{T}"/> of <see cref="Spans"/>.</param>
    public Cell(List<Spans> spans)
        : this(new Text(spans))
    {
        
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Cell"/> class.
    /// </summary>
    /// <param name="content">The <see cref="List{T}"/> of <see cref="Spans"/>.</param>
    public Cell(string content)
        : this(new List<Spans> { content })
    {
        
    }
}

/// <summary>
/// The table row.
/// </summary>
/// <param name="Cells">The <see cref="Cell"/> collection.</param>
/// <param name="Height">The height.</param>
/// <param name="Style">The <see cref="Styles.Style"/>.</param>
/// <param name="BottomMargin">The bottom margin.</param>
public record Row(List<Cell> Cells, int Height, Style Style, int BottomMargin)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Row"/> class.
    /// </summary>
    /// <param name="cells">The <see cref="Cell"/> collection.</param>
    public Row(IEnumerable<Cell> cells)
        : this(cells.ToList(), 1, new(), 0)
    {
    }

    /// <summary>
    /// The total height.
    /// </summary>
    public int TotalHeight => Height + BottomMargin;
}

/// <summary>
/// The table state.
/// </summary>
public record TableState
{
    /// <summary>
    /// The offset.
    /// </summary>
    public int Offset { get; set; }

    private int? _selected;

    /// <summary>
    /// The selected index.
    /// </summary>
    public int? Selected
    {
        get => _selected;
        set
        {
            _selected = value;
            if (value == null)
            {
                Offset = 0;
            }
        }
    }
}

/// <summary>
/// The table widget.
/// </summary>
public class Table : IWidget, IStateWidget<TableState>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Table"/> class.
    /// </summary>
    public Table()
    {
        
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Table"/> class.
    /// </summary>
    /// <param name="rows">The <see cref="Row"/> collection.</param>
    public Table(IEnumerable<Row> rows)
    {
        Rows = rows.ToList();
    }
    
    /// <summary>
    /// The <see cref="Widgets.Block"/>.
    /// </summary>
    public Block? Block { get; set; }
    
    /// <summary>
    /// The <see cref="Styles.Style"/>.
    /// </summary>
    public Style Style { get; set; }
    
    /// <summary>
    /// The column spacing.
    /// </summary>
    public int ColumnSpacing { get; set; } = 1;
    
    /// <summary>
    /// The highlight <see cref="Styles.Style"/>.
    /// </summary>
    public Style HighlightStyle { get; set; }
    
    /// <summary>
    /// The highlight symbol.
    /// </summary>
    public string? HighlightSymbol { get; set; }
    
    /// <summary>
    /// The width constraints.
    /// </summary>
    public List<IConstraint> Widths { get; set; } = new();
    
    /// <summary>
    /// The <see cref="Row"/>.
    /// </summary>
    public Row? Header { get; set; }
    
    /// <summary>
    /// The <see cref="Row"/> collection.
    /// </summary>
    public List<Row> Rows { get; set; } = new();

    private List<int> GetColumnsWidths(int maxWidth, bool hasSelection)
    {
        var constraints = new List<IConstraint>(Widths.Count * 2 + 1);
        if (hasSelection)
        {
            var highlightSymbolWidth = HighlightSymbol?.Width() ?? 0;
            constraints.Add(Constraints.Length(highlightSymbolWidth));
        }

        foreach (var constraint in Widths)
        {
            constraints.Add(constraint);
            constraints.Add(Constraints.Length(ColumnSpacing));
        }

        if (Widths.Count > 0)
        {
            constraints.RemoveAt(constraints.Count - 1);
        }

        var chunks = new Layout { Direction = Direction.Horizontal, Constraints = constraints, ExpandToFill = false }
            .Split(new Rect(0, 0, maxWidth, 1));

        if (hasSelection)
        {
            chunks.RemoveAt(0);
        }

        return chunks.StepBy(2)
            .Select(x => x.Width)
            .ToList();
    }

    private (int, int) GetRowBounds(int? selected, int offset, int maxHeight)
    {
        offset = Math.Min(offset, Rows.Count.SaturatingSub(1));

        var start = offset;
        var end = offset;
        var height = 0;

        foreach (var item in Rows.Skip(offset))
        {
            if (height + item.Height > maxHeight)
            {
                break;
            }

            height += item.TotalHeight;
            end++;
        }

        selected = Math.Min(selected ?? 0, Rows.Count - 1);
        while (selected >= end)
        {
            height += Rows[end].TotalHeight;
            end++;

            while (height > maxHeight)
            {
                height = height.SaturatingSub(Rows[start].TotalHeight);
                start++;
            }
        }

        while (selected < start)
        {
            start--;
            height += Rows[start].TotalHeight;
            while (height > maxHeight)
            {
                end--;
                height = height.SaturatingSub(Rows[end].TotalHeight);
            }
        }

        return (start, end);
    }

    /// <inheritdoc cref="IStateWidget{T}.Render"/>
    public void Render(Rect area, Buffer buffer, TableState state)
    {
        if (area.Area == 0)
        {
            return;
        }

        buffer.SetStyle(area, Style);
        var tableArea = area;
        if (Block != null)
        {
            var inner = Block.Inner(area);
            Block.Render(area, buffer);
            tableArea = inner;
        }

        var hasSelection = state.Selected != null;
        var columnsWidths = GetColumnsWidths(tableArea.Width, hasSelection);
        var highlightSymbol = HighlightSymbol ?? string.Empty;
        var blank = new string(' ', highlightSymbol.Width());
        var currentHeight = 0;
        var rowHeight = tableArea.Height;

        // Draw header
        if (Header != null)
        {
            var maxHeaderHeight = Math.Min(tableArea.Height, Header.TotalHeight);
            buffer.SetStyle(
                new Rect(tableArea.Left, tableArea.Top, tableArea.Width,
                    Math.Min(tableArea.Height, Header.Height)),
                Header.Style);

            var col = tableArea.Left;
            if (hasSelection)
            {
                col += Math.Min(highlightSymbol.Width(), tableArea.Width);
            }

            foreach (var (width, cell) in columnsWidths.Zip(Header.Cells))
            {
                RenderCell(buffer, cell, new Rect(col, tableArea.Top, width, maxHeaderHeight));
                col += width + ColumnSpacing;
            }

            currentHeight += maxHeaderHeight;
            rowHeight = rowHeight.SaturatingSub(maxHeaderHeight);
        }

        if (Rows.Count == 0)
        {
            return;
        }

        var (start, end) = GetRowBounds(state.Selected, state.Offset, rowHeight);
        state.Offset = start;
        foreach (var (i, tableRow) in Rows.Skip(state.Offset).Take(end - start).WithIndex(state.Offset))
        {
            var (row, col) = (tableArea.Top + currentHeight, tableArea.Left);
            currentHeight += tableRow.TotalHeight;

            var tableRowArea =  new Rect(col, row, tableArea.Width, tableRow.Height);
            buffer.SetStyle(tableRowArea, tableRow.Style);

            var isSelected = state.Selected is { } s && s == i;
            var tableRowStartCol = col;
            if (hasSelection)
            {
                var symbol = isSelected ? highlightSymbol : blank;
                (tableRowStartCol, _) = buffer.SetString(col, row, symbol, tableArea.Width, tableRow.Style);
            }

            col = tableRowStartCol;
            foreach (var (width, cell) in columnsWidths.Zip(tableRow.Cells))
            {
                RenderCell(buffer, cell, new Rect(col, row, width, tableRow.Height));
                col += width + ColumnSpacing;
            }

            if (isSelected)
            {
                buffer.SetStyle(tableRowArea, HighlightStyle);
            }
        }
    }

    /// <inheritdoc cref="IWidget.Render"/>
    public void Render(Rect area, Buffer buffer) 
        => Render(area, buffer, new TableState());

    private static void RenderCell(Buffer buffer, Cell cell, Rect area)
    {
        buffer.SetStyle(area, cell.Style);
        for (var index = 0; index < cell.Content.Lines.Count; index++)
        {
            if (index >= area.Height)
            {
                break;
            }
            
            var span = cell.Content.Lines[index];
            buffer.SetSpan(area.X, area.Y + index, span, area.Width);
        }
    }
}
