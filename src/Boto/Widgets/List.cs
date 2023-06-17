using Boto.Extensions;
using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widgets;

/// <summary>
/// The list state.
/// </summary>
public record ListState
{
    /// <summary>
    /// List offset.
    /// </summary>
    public int Offset { get; set; }
    
    /// <summary>
    /// The selected item.
    /// </summary>
    public int? Selected { get; set; }
}


/// <summary>
/// The list item.
/// </summary>
/// <param name="Content">The content.</param>
/// <param name="Style">The <see cref="Styles.Style"/>.</param>
public record ListItem(Text Content, Style Style)
{
    /// <summary>
    /// Initialize a new instance of <see cref="ListItem"/>.
    /// </summary>
    /// <param name="content">The collection of <see cref="Spans"/>.</param>
    public ListItem(IEnumerable<Spans> content)
        : this(new Text(content.ToList()))
    {
        
    }
    
    /// <summary>
    /// Initialize a new instance of <see cref="ListItem"/>.
    /// </summary>
    /// <param name="content">The content.</param>
    public ListItem(Text content)
        : this(content, new())
    {
    }

    /// <summary>
    /// The content item.
    /// </summary>
    public int Height => Content.Height;
}

/// <summary>
/// The list widget.
/// </summary>
public class List : IWidget, IStateWidget<ListState>
{
    /// <summary>
    /// The <see cref="Widgets.Block"/>.
    /// </summary>
    public Block? Block { get; set; }
    
    /// <summary>
    /// The <see cref="Styles.Style"/>.
    /// </summary>
    public Style Style { get; set; }
    
    /// <summary>
    /// The start <see cref="Corner"/>.
    /// </summary>
    public Corner StartCorner { get; set; } = Corner.TopLeft;
    
    /// <summary>
    /// The highlight <see cref="Styles.Style"/>.
    /// </summary>
    public Style HighlightStyle { get; set; }
    
    /// <summary>
    /// The highlight symbol.
    /// </summary>
    public string? HighlightSymbol { get; set; }
    
    /// <summary>
    /// Flag indicating to repeat the highlight symbol.
    /// </summary>
    public bool RepeatHighlightSymbol { get; set; }
    
    /// <summary>
    /// The collection of <see cref="ListItem"/>.
    /// </summary>
    public List<ListItem> Items { get; set; } = new();

    private (int, int) GetItemsBounds(int? selected, int offset, int maxHeight)
    {
        offset = Math.Min(offset, Items.Count.SaturatingSub(1));
        var start = offset;
        var end = offset;
        var height = 0;

        foreach (var item in Items.Skip(offset))
        {
            if (height + item.Height > maxHeight)
            {
                break;
            }

            height += item.Height;
            end++;
        }

        selected = Math.Min(selected ?? 0, Items.Count - 1);
        while (selected >= end)
        {
            height += Items[end].Height;
            end++;

            while (height > maxHeight)
            {
                height = height.SaturatingSub(Items[start].Height);
                start++;
            }
        }

        while (selected < start)
        {
            start--;
            height += Items[start].Height;
            while (height > maxHeight)
            {
                end--;
                height = height.SaturatingSub(Items[end].Height);
            }
        }

        return (start, end);
    }

    /// <inheritdoc cref="IStateWidget{T}.Render"/>
    public void Render(Rect area, Buffer buffer, ListState state)
    {
        buffer.SetStyle(area, Style);
        var listArea = area;
        if (Block != null)
        {
            var innerArea = Block.Inner(area);
            Block.Render(area, buffer);
            listArea = innerArea;
        }

        if (listArea.Width < 1 || listArea.Height < 1)
        {
            return;
        }

        if (Items.Count == 0)
        {
            return;
        }

        var (start, end) = GetItemsBounds(state.Selected, state.Offset, listArea.Height);
        state.Offset = start;

        var highlightSymbol = HighlightSymbol ?? string.Empty;
        var blankSymbol = new string(' ', highlightSymbol.Width());

        var currentHeight = 0;
        var hasSelection = state.Selected.HasValue;
        foreach (var (i, item) in Items.Skip(state.Offset).Take(end - start).WithIndex(state.Offset))
        {
            var (x, y) = (0, 0);
            if (StartCorner == Corner.BottomLeft)
            {
                currentHeight += item.Height;
                (x, y) = (listArea.Left, listArea.Bottom - currentHeight);
            }
            else
            {
                (x, y) = (listArea.Left, listArea.Top + currentHeight);
                currentHeight += item.Height;
            }

            area = new(x, y, listArea.Width, item.Height);
            
            var itemStyle = item.Style.Merge(item.Style);
            buffer.SetStyle(area, itemStyle);

            var isSelected = state.Selected == i;

            foreach (var (j, line) in item.Content.Lines.WithIndex())
            {
                // if the item is selected, we need to display the hightlight symbol:
                // - either for the first line of the item only,
                // - or for each line of the item if the appropriate option is set
                var symbol = isSelected && (j == 0 || RepeatHighlightSymbol)
                    ? highlightSymbol
                    : blankSymbol;

                var (elemX, maxElementWidth) = (x, listArea.Width);
                if (hasSelection)
                {
                    (elemX, _) = buffer.SetString(x, y + j, symbol, listArea.Width, itemStyle);
                    maxElementWidth = listArea.Width - (elemX - x);
                }

                buffer.SetSpan(elemX, y + j, line, maxElementWidth);
            }

            if (isSelected)
            {
                buffer.SetStyle(area, HighlightStyle);
            }
        }
    }

    /// <inheritdoc cref="IWidget.Render"/>
    public void Render(Rect area, Buffer buffer) 
        => Render(area, buffer, new ListState());
}
