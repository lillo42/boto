using Boto.Extensions;
using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widget;

public record ListState
{
    public int Offset { get; set; }
    public int? Selected { get; set; }
}

public record ListItem(Text Content, Style Style)
{
    public ListItem(List<Spans> content)
        : this(new Text(content))
    {
        
    }
    public ListItem(Text content)
        : this(content, new())
    {
    }

    public int Height => Content.Height;
}

public class List : IWidget, IStateWidget<ListState>
{
    public Block? Block { get; set; }
    public Style Style { get; set; }
    public Corner StartCorner { get; set; } = Corner.TopLeft;
    public Style HighlightStyle { get; set; }
    public string? HighlightSymbol { get; set; }
    public bool RepeatHighlightSymbol { get; set; }
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

    public void Render(Rect area, Buffer buffer) 
        => Render(area, buffer, new ListState());
}
