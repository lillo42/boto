using Boto.Styles;
using Boto.Symbols;

namespace Boto.Widgets.Extensions;

/// <summary>
/// The <see cref="BarChart"/> extensions method.
/// </summary>
public static class BarChartExtensions
{
    /// <summary>
    /// Change the <see cref="BarChart.Block"/>.
    /// </summary>
    /// <param name="barChart">The <see cref="BarChart"/>.</param>
    /// <param name="block">The <see cref="Block"/>.</param>
    /// <returns>The <paramref name="barChart"/> with <see cref="BarChart.Block"/> as <paramref name="block"/>.</returns>
    public static BarChart SetBlock(this BarChart barChart, Block? block)
    {
        barChart.Block = block;
        return barChart;
    }

    /// <summary>
    /// Change the <see cref="BarChart.Style"/>.
    /// </summary>
    /// <param name="barChart">The <see cref="BarChart"/>.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The <paramref name="barChart"/> with <see cref="BarChart.Style"/> as <paramref name="style"/>.</returns>
    public static BarChart SetStyle(this BarChart barChart, Style style)
    {
        barChart.Style = style;
        return barChart;
    }

    /// <summary>
    /// Change the <see cref="BarChart.BarSet"/>.
    /// </summary>
    /// <param name="barChart">The <see cref="BarChart"/>.</param>
    /// <param name="set">The <see cref="Set"/>.</param>
    /// <returns>The <paramref name="barChart"/> with <see cref="BarChart.BarSet"/> as <paramref name="set"/>.</returns>
    public static BarChart SetBarSet(this BarChart barChart, Set set)
    {
        barChart.BarSet = set;
        return barChart;
    }

    /// <summary>
    /// Change the <see cref="BarChart.BarWidth"/>.
    /// </summary>
    /// <param name="barChart">The <see cref="BarChart"/>.</param>
    /// <param name="width">The width.</param>
    /// <returns>The <paramref name="barChart"/> with <see cref="BarChart.BarWidth"/> as <paramref name="width"/>.</returns>
    public static BarChart SetBarWidth(this BarChart barChart, int width)
    {
        barChart.BarWidth = width;
        return barChart;
    }

    /// <summary>
    /// Change the <see cref="BarChart.BarGap"/>.
    /// </summary>
    /// <param name="barChart">The <see cref="BarChart"/>.</param>
    /// <param name="gap">The gap size.</param>
    /// <returns>The <paramref name="barChart"/> with <see cref="BarChart.BarGap"/> as <paramref name="gap"/>.</returns>
    public static BarChart SetBarGap(this BarChart barChart, int gap)
    {
        barChart.BarGap = gap;
        return barChart;
    }

    /// <summary>
    /// Change the <see cref="BarChart.BarStyle"/>.
    /// </summary>
    /// <param name="barChart">The <see cref="BarChart"/>.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The <paramref name="barChart"/> with <see cref="BarChart.Style"/> as <paramref name="style"/>.</returns>
    public static BarChart SetBarStyle(this BarChart barChart, Style style)
    {
        barChart.BarStyle = style;
        return barChart;
    }

    /// <summary>
    /// Change the <see cref="BarChart.Max"/>
    /// </summary>
    /// <param name="barChart">The <see cref="BarChart"/>.</param>
    /// <param name="max">The max size.</param>
    /// <returns>The <paramref name="barChart"/> with <see cref="BarChart.Max"/> as <paramref name="max"/>.</returns>
    public static BarChart SetMax(this BarChart barChart, long? max)
    {
        barChart.Max = max;
        return barChart;
    }

    /// <summary>
    /// Change the <see cref="BarChart.LabelStyle"/>.
    /// </summary>
    /// <param name="barChart">The <see cref="BarChart"/>.</param>
    /// <param name="style">Tha label <see cref="Style"/>.</param>
    /// <returns>The <paramref name="barChart"/> with <see cref="BarChart.LabelStyle"/> as <paramref name="style"/>.</returns>
    public static BarChart SetLabelStyle(this BarChart barChart, Style style)
    {
        barChart.LabelStyle = style;
        return barChart;
    }

    /// <summary>
    /// Change the <see cref="BarChart.ValueStyle"/>.
    /// </summary>
    /// <param name="barChart">The <see cref="BarChart"/>.</param>
    /// <param name="style">Tha value <see cref="Style"/>.</param>
    /// <returns>The <paramref name="barChart"/> with <see cref="BarChart.ValueStyle"/> as <paramref name="style"/>.</returns>
    public static BarChart SetValueStyle(this BarChart barChart, Style style)
    {
        barChart.ValueStyle = style;
        return barChart;
    }

    /// <summary>
    /// Add a <see cref="IBarCharItem"/> to the <see cref="BarChart"/>.
    /// </summary>
    /// <param name="barChart">The <see cref="BarChart"/>.</param>
    /// <param name="item">The <see cref="IBarCharItem"/>.</param>
    /// <returns>The <paramref name="barChart"/> with <see cref="BarChart.Items"/> plus <paramref name="item"/>.</returns>
    public static BarChart AddItem(this BarChart barChart, IBarCharItem item)
    {
        barChart.Items.Add(item);
        return barChart;
    }

    /// <summary>
    /// Add a <see cref="IBarCharItem"/> to the <see cref="BarChart"/>.
    /// </summary>
    /// <param name="barChart">The <see cref="BarChart"/>.</param>
    /// <param name="items">The collection <see cref="IBarCharItem"/>.</param>
    /// <returns>The <paramref name="barChart"/> with <see cref="BarChart.Items"/> plus <paramref name="items"/>.</returns>
    public static BarChart AddItems(this BarChart barChart, IEnumerable<IBarCharItem> items)
    {
        barChart.Items.AddRange(items);
        return barChart;
    }

    /// <summary>
    /// Add a <see cref="IBarCharItem"/> to the <see cref="BarChart"/>.
    /// </summary>
    /// <param name="barChart">The <see cref="BarChart"/>.</param>
    /// <param name="items">The collection <see cref="IBarCharItem"/>.</param>
    /// <returns>The <paramref name="barChart"/> with <see cref="BarChart.Items"/> plus <paramref name="items"/>.</returns>
    public static BarChart AddItems(this BarChart barChart, params IBarCharItem[] items)
    {
        barChart.Items.AddRange(items);
        return barChart;
    }

    /// <summary>
    /// Add a <see cref="IBarCharItem"/> to the <see cref="BarChart"/>.
    /// </summary>
    /// <param name="barChart">The <see cref="BarChart"/>.</param>
    /// <param name="label">The label.</param>
    /// <param name="value">The value.</param>
    /// <returns>The <paramref name="barChart"/> with <see cref="BarChart.Items"/> plus (<paramref name="label"/> and <paramref name="value"/>).</returns>
    public static BarChart AddItem(this BarChart barChart, string label, int value)
        => barChart.AddItem(new BarChartItem(label, value));

    /// <summary>
    /// Add a <see cref="IBarCharItem"/> to the <see cref="BarChart"/>.
    /// </summary>
    /// <param name="barChart">The <see cref="BarChart"/>.</param>
    /// <param name="label">The label.</param>
    /// <param name="value">The value.</param>
    /// <param name="valueLabel">The value label.</param>
    /// <returns>The <paramref name="barChart"/> with <see cref="BarChart.Items"/> plus (<paramref name="label"/> and <paramref name="value"/>).</returns>
    public static BarChart AddItem(this BarChart barChart, string label, int value, string valueLabel)
        => barChart.AddItem(new BarChartItem(label, value, valueLabel));

    /// <summary>
    /// Add a <see cref="IBarCharItem"/> to the <see cref="BarChart"/>.
    /// </summary>
    /// <param name="barChart">The <see cref="BarChart"/>.</param>
    /// <param name="items">The collection <see cref="IBarCharItem"/>.</param>
    /// <returns>The <paramref name="barChart"/> with <see cref="BarChart.Items"/> plus <paramref name="items"/>.</returns>
    public static BarChart AddItems(this BarChart barChart, IEnumerable<(string label, int value)> items)
        => barChart.AddItems(items.Select(x => new BarChartItem(x.label, x.value)));

    /// <summary>
    /// Add a <see cref="IBarCharItem"/> to the <see cref="BarChart"/>.
    /// </summary>
    /// <param name="barChart">The <see cref="BarChart"/>.</param>
    /// <param name="items">The collection <see cref="IBarCharItem"/>.</param>
    /// <returns>The <paramref name="barChart"/> with <see cref="BarChart.Items"/> plus <paramref name="items"/>.</returns>
    public static BarChart AddItems(this BarChart barChart,
        IEnumerable<(string label, int value, string valueLabel)> items)
        => barChart.AddItems(items.Select(x => new BarChartItem(x.label, x.value, x.valueLabel)));

    /// <summary>
    /// Add a <see cref="IBarCharItem"/> to the <see cref="BarChart"/>.
    /// </summary>
    /// <param name="barChart">The <see cref="BarChart"/>.</param>
    /// <param name="items">The collection <see cref="IBarCharItem"/>.</param>
    /// <param name="map">The map functions.</param>
    /// <returns>The <paramref name="barChart"/> with <see cref="BarChart.Items"/> plus <paramref name="items"/>.</returns>
    public static BarChart AddItems<T>(this BarChart barChart, IEnumerable<T> items, Func<T, IBarCharItem> map)
        => barChart.AddItems(items.Select(map));
}
