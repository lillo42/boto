using Boto.Styles;
using Boto.Symbols;

namespace Boto.Widget;

public static class SparklineExtensions
{
    public static Sparkline Block(this Sparkline sparkline, Block block)
    {
        sparkline.Block = block;
        return sparkline;
    }

    public static Sparkline Style(this Sparkline sparkline, Style style)
    {
        sparkline.Style = style;
        return sparkline;
    }

    public static Sparkline Max(this Sparkline sparkline, int max)
    {
        sparkline.Max = max;
        return sparkline;
    }

    public static Sparkline NoMax(this Sparkline sparkline)
    {
        sparkline.Max = null;
        return sparkline;
    }

    public static Sparkline BarSet(this Sparkline sparkline, Set barSet)
    {
        sparkline.BarSet = barSet;
        return sparkline;
    }

    public static Sparkline AddItem(this Sparkline sparkline, int item)
    {
        sparkline.Items.Add(item);
        return sparkline;
    }

    public static Sparkline AddItems(this Sparkline sparkline, IEnumerable<int> items)
    {
        sparkline.Items.AddRange(items.ToList());
        return sparkline;
    }

    public static Sparkline AddItems(this Sparkline sparkline, params int[] items)
    {
        sparkline.Items.AddRange(items.ToList());
        return sparkline;
    }
}
