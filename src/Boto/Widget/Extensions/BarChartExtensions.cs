using Boto.Styles;
using Boto.Symbols;

namespace Boto.Widget;

public static class BarChartExtensions
{
    public static BarChart Block(this BarChart barChart, Block? block)
    {
        barChart.Block = block;
        return barChart;
    }

    public static BarChart Style(this BarChart barChart, Style style)
    {
        barChart.Style = style;
        return barChart;
    }

    public static BarChart BarSet(this BarChart barChart, Set set)
    {
        barChart.BarSet = set;
        return barChart;
    }

    public static BarChart BarWidth(this BarChart barChart, int width)
    {
        barChart.BarWidth = width;
        return barChart;
    }

    public static BarChart BarGap(this BarChart barChart, int gap)
    {
        barChart.BarGap = gap;
        return barChart;
    }

    public static BarChart BarStyle(this BarChart barChart, Style style)
    {
        barChart.BarStyle = style;
        return barChart;
    }

    public static BarChart Max(this BarChart barChart, long? max)
    {
        barChart.Max = max;
        return barChart;
    }

    public static BarChart LabelStyle(this BarChart barChart, Style style)
    {
        barChart.LabelStyle = style;
        return barChart;
    }

    public static BarChart ValueStyle(this BarChart barChart, Style style)
    {
        barChart.ValueStyle = style;
        return barChart;
    }

    public static BarChart AddItem(this BarChart barChart, IBarCharItem item)
    {
        barChart.Items.Add(item);
        return barChart;
    }

    public static BarChart AddItems(this BarChart barChart, IEnumerable<IBarCharItem> items)
    {
        barChart.Items.AddRange(items);
        return barChart;
    }

    public static BarChart AddItems(this BarChart barChart, params IBarCharItem[] items)
    {
        barChart.Items.AddRange(items);
        return barChart;
    }

    public static BarChart AddItem(this BarChart barChart, string label, int value)
        => barChart.AddItem(new BarChartItem(label, value));

    public static BarChart AddItem(this BarChart barChart,string label, int value, string valueLabel)
        => barChart.AddItem(new BarChartItem(label, value, valueLabel));

    public static BarChart AddItems(this BarChart barChart, IEnumerable<(string label, int value)> items)
        => barChart.AddItems(items.Select(x => new BarChartItem(x.label, x.value)));

    public static BarChart AddItems(this BarChart barChart, IEnumerable<(string label, int value, string valueLabel)> items)
        => barChart.AddItems(items.Select(x => new BarChartItem(x.label, x.value, x.valueLabel)));

    public static BarChart AddItems<T>(this BarChart barChart, IEnumerable<T> items, Func<T, IBarCharItem> map)
        => barChart.AddItems(items.Select(map));
}
