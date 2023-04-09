using Boto.Styles;

namespace Boto.Widget;

public interface IBarCharItem
{
    string Label { get; }
    Style? LabelStyle { get; }
    int Value { get; }
    string ValueLabel { get; }
    Style? ValueStyle { get; }
}

public record BarChartItem
    (string Label, Style? LabelStyle, int Value, string ValueLabel, Style? ValueStyle) : IBarCharItem
{
    public BarChartItem(string label, int value)
        : this(label, null, value, value.ToString(), null)
    {
    }

    public BarChartItem(string label, int value, string valueLabel)
        : this(label, null, value, valueLabel, null)
    {
    }
}
