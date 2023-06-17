using Boto.Styles;
using Boto.Texts;

namespace Boto.Widgets.Extensions;

/// <summary>
/// The <see cref="Gauge"/> extensions method.
/// </summary>
public static class GaugeExtensions
{
    /// <summary>
    /// Change the <see cref="Gauge.Block"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="Gauge"/>.</param>
    /// <param name="block">The <see cref="Block"/>.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="Gauge.Block"/> as <paramref name="block"/>.</returns>
    public static Gauge SetBlock(this Gauge gauge, Block block)
    {
        gauge.Block = block;
        return gauge;
    }

    /// <summary>
    /// Change the <see cref="Gauge.Ratio"/> as percent.
    /// </summary>
    /// <param name="gauge">The <see cref="Gauge"/>.</param>
    /// <param name="percent">The ratio as percent.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="Gauge.Ratio"/> as <paramref name="percent"/> / 100.</returns>
    public static Gauge SetPercent(this Gauge gauge, int percent)
    {
        gauge.Ratio = percent / 100.0;
        return gauge;
    }

    /// <summary>
    /// Change the <see cref="Gauge.Ratio"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="Gauge"/>.</param>
    /// <param name="ratio">The ratio.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="Gauge.Ratio"/> as <paramref name="ratio"/>.</returns>
    public static Gauge SetRatio(this Gauge gauge, double ratio)
    {
        gauge.Ratio = ratio;
        return gauge;
    }

    /// <summary>
    /// Change the <see cref="Gauge.Label"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="Gauge"/>.</param>
    /// <param name="label">The label.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="Gauge.Label"/> as <paramref name="label"/>.</returns>
    public static Gauge SetLabel(this Gauge gauge, string label)
    {
        gauge.Label = new Span(label);
        return gauge;
    }

    /// <summary>
    /// Change the <see cref="Gauge.Label"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="Gauge"/>.</param>
    /// <param name="label">The label.</param>
    /// <param name="style">The <see cref="Styles.Style"/>.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="Gauge.Label"/> as <paramref name="label"/>.</returns>
    public static Gauge SetLabel(this Gauge gauge, string label, Style style)
    {
        gauge.Label = new Span(label, style);
        return gauge;
    }

    /// <summary>
    /// Change the <see cref="Gauge.Label"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="Gauge"/>.</param>
    /// <param name="label">The label as <see cref="Span"/>.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="Gauge.Label"/> as <paramref name="label"/>.</returns>
    public static Gauge SetLabel(this Gauge gauge, Span label)
    {
        gauge.Label = label;
        return gauge;
    }

    /// <summary>
    /// Change the <see cref="Gauge.Style"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="Gauge"/>.</param>
    /// <param name="style">The <see cref="Styles.Style"/>.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="Gauge.Style"/> as <paramref name="style"/>.</returns>
    public static Gauge SetStyle(this Gauge gauge, Style style)
    {
        gauge.Style = style;
        return gauge;
    }

    /// <summary>
    /// Change the <see cref="Gauge.GaugeStyle"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="Gauge"/>.</param>
    /// <param name="style">The <see cref="Styles.Style"/>.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="Gauge.GaugeStyle"/> as <paramref name="style"/>.</returns>
    public static Gauge SetGaugeStyle(this Gauge gauge, Style style)
    {
        gauge.GaugeStyle = style;
        return gauge;
    }

    /// <summary>
    /// Change the <see cref="Gauge.UseUnicode"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="Gauge"/>.</param>
    /// <param name="useUnicode">Flag indicating if should use unicode.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="Gauge.UseUnicode"/> as <paramref name="useUnicode"/>.</returns>
    public static Gauge SetUnicode(this Gauge gauge, bool useUnicode)
    {
        gauge.UseUnicode = useUnicode;
        return gauge;
    }

    /// <summary>
    /// Enable the <see cref="Gauge.UseUnicode"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="Gauge"/>.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="Gauge.UseUnicode"/> as true.</returns>
    public static Gauge EnableUnicode(this Gauge gauge)
    {
        gauge.UseUnicode = true;
        return gauge;
    }

    /// <summary>
    /// Disable the <see cref="Gauge.UseUnicode"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="Gauge"/>.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="Gauge.UseUnicode"/> as false.</returns>
    public static Gauge DisableUnicode(this Gauge gauge)
    {
        gauge.UseUnicode = false;
        return gauge;
    }
}
