using Boto.Styles;

namespace Boto.Widgets;

/// <summary>
/// The <see cref="BarChart"/> item.
/// </summary>
public interface IBarCharItem
{
    /// <summary>
    /// The label.
    /// </summary>
    string Label { get; }

    /// <summary>
    /// Th label <see cref="Style"/>.
    /// </summary>
    Style? LabelStyle { get; }

    /// <summary>
    /// The value.
    /// </summary>
    int Value { get; }

    /// <summary>
    /// The value label.
    /// </summary>
    string ValueLabel { get; }

    /// <summary>
    /// The value <see cref="Style"/>.
    /// </summary>
    Style? ValueStyle { get; }
}

/// <summary>
/// Default <see cref="IBarCharItem"/> implementation.
/// </summary>
/// <param name="Label">The label.</param>
/// <param name="LabelStyle">The label <see cref="Style"/>.</param>
/// <param name="Value">The value.</param>
/// <param name="ValueLabel">The value label.</param>
/// <param name="ValueStyle">The value <see cref="Style"/>.</param>
public record BarChartItem(string Label, Style? LabelStyle, 
    int Value, string ValueLabel, Style? ValueStyle) : IBarCharItem
{
    /// <summary>
    /// Initialize a new <see cref="BarChartItem"/>.
    /// </summary>
    /// <param name="label">The label.</param>
    /// <param name="value">The value.</param>
    public BarChartItem(string label, int value)
        : this(label, null, value, value.ToString(), null)
    {
    }

    /// <summary>
    /// Initialize a new <see cref="BarChartItem"/>.
    /// </summary>
    /// <param name="label">The label.</param>
    /// <param name="value">The value.</param>
    /// <param name="valueLabel">The value label.</param>
    public BarChartItem(string label, int value, string valueLabel)
        : this(label, null, value, valueLabel, null)
    {
    }
}
