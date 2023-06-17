using Boto.Styles;
using Boto.Texts;

namespace Boto.Widgets.Extensions;

/// <summary>
/// The <see cref="LineGauge"/> extensions method.
/// </summary>
public static class LineGaugeExtensions
{
    /// <summary>
    /// Change the <see cref="LineGauge.Block"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="LineGauge"/>.</param>
    /// <param name="block">The <see cref="Block"/>.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="LineGauge.Block"/> as <paramref name="block"/>.</returns>
    public static LineGauge SetBlock(this LineGauge gauge, Block block)
    {
        gauge.Block = block;
        return gauge;
    }

    /// <summary>
    /// Change the <see cref="LineGauge.Style"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="LineGauge"/>.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="LineGauge.Style"/> as <paramref name="style"/>.</returns>
    public static LineGauge SetStyle(this LineGauge gauge, Style style)
    {
        gauge.Style = style;
        return gauge;
    }

    /// <summary>
    /// Change the <see cref="LineGauge.Ratio"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="LineGauge"/>.</param>
    /// <param name="ratio">The ratio.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="LineGauge.Ratio"/> as <paramref name="ratio"/>.</returns>
    public static LineGauge SetRatio(this LineGauge gauge, double ratio)
    {
        gauge.Ratio = ratio;
        return gauge;
    }

    /// <summary>
    /// Change the <see cref="LineGauge.Ratio"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="LineGauge"/>.</param>
    /// <param name="percent">The ratio as percent.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="LineGauge.Ratio"/> as <paramref name="percent"/> / 100.</returns>
    public static LineGauge SetPercent(this LineGauge gauge, double percent)
    {
        gauge.Ratio = percent / 100;
        return gauge;
    }

    /// <summary>
    /// Change the <see cref="LineGauge.GaugeStyle"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="LineGauge"/>.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="LineGauge.GaugeStyle"/> as <paramref name="style"/>.</returns>
    public static LineGauge SetGaugeStyle(this LineGauge gauge, Style style)
    {
        gauge.GaugeStyle = style;
        return gauge;
    }

    /// <summary>
    /// Change the <see cref="LineGauge.Label"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="LineGauge"/>.</param>
    /// <param name="label">The label as <see cref="Spans"/>.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="LineGauge.GaugeStyle"/> as <paramref name="label"/>.</returns>
    public static LineGauge SetLabel(this LineGauge gauge, Spans label)
    {
        gauge.Label = label;
        return gauge;
    }

    /// <summary>
    /// Change the <see cref="LineGauge.Label"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="LineGauge"/>.</param>
    /// <param name="label">The label as <see cref="Span"/>.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="LineGauge.GaugeStyle"/> as <paramref name="label"/>.</returns>
    public static LineGauge SetLabel(this LineGauge gauge, Span label)
        => gauge.SetLabel(new Spans(label));

    /// <summary>
    /// Change the <see cref="LineGauge.Label"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="LineGauge"/>.</param>
    /// <param name="label">The label.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="LineGauge.GaugeStyle"/> as <paramref name="label"/>.</returns>
    public static LineGauge SetLabel(this LineGauge gauge, string label)
        => gauge.SetLabel(new Spans(label));

    /// <summary>
    /// Change the <see cref="LineGauge.Label"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="LineGauge"/>.</param>
    /// <param name="label">The label.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="LineGauge.GaugeStyle"/> as <paramref name="label"/>.</returns>
    public static LineGauge SetLabel(this LineGauge gauge, string label, Style style)
        => gauge.SetLabel(new Spans(label, style));

    /// <summary>
    /// Change the <see cref="LineGauge.Label"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="LineGauge"/>.</param>
    /// <param name="labels">The label collection of <see cref="Span"/>.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="LineGauge.GaugeStyle"/> as <paramref name="labels"/>.</returns>
    public static LineGauge SetLabel(this LineGauge gauge, IEnumerable<Span> labels)
        => gauge.SetLabel(new Spans(labels.ToList()));

    /// <summary>
    /// Change the <see cref="LineGauge.Label"/>.
    /// </summary>
    /// <param name="gauge">The <see cref="LineGauge"/>.</param>
    /// <param name="labels">The label collection of <see cref="Span"/>.</param>
    /// <returns>The <paramref name="gauge"/> with the <see cref="LineGauge.GaugeStyle"/> as <paramref name="labels"/>.</returns>
    public static LineGauge SetLabel(this LineGauge gauge, params Span[] labels)
        => gauge.SetLabel(new Spans(labels.ToList()));
}
