using Boto.Styles;
using Boto.Texts;

namespace Boto.Widget;

public static class LineGaugeExtensions
{
    public static LineGauge Block(this LineGauge gauge, Block block)
    {
        gauge.Block = block;
        return gauge;
    }

    public static LineGauge NoBlock(this LineGauge gauge)
    {
        gauge.Block = null;
        return gauge;
    }

    public static LineGauge Style(this LineGauge gauge, Style style)
    {
        gauge.Style = style;
        return gauge;
    }

    public static LineGauge Ratio(this LineGauge gauge, double ratio)
    {
        gauge.Ratio = ratio;
        return gauge;
    }

    public static LineGauge Percent(this LineGauge gauge, double percent)
    {
        gauge.Ratio = percent / 100;
        return gauge;
    }

    public static LineGauge GaugeStyle(this LineGauge gauge, Style gaugeStyle)
    {
        gauge.GaugeStyle = gaugeStyle;
        return gauge;
    }

    public static LineGauge NoLabel(this LineGauge gauge, Spans label)
    {
        gauge.Label = null;
        return gauge;
    }

    public static LineGauge Label(this LineGauge gauge, Spans label)
    {
        gauge.Label = label;
        return gauge;
    }
    
    public static LineGauge Label(this LineGauge gauge, Span label)
        => gauge.Label(new Spans(label));
    
    public static LineGauge Label(this LineGauge gauge, string label)
        => gauge.Label(new Spans(label));
    
    public static LineGauge Label(this LineGauge gauge, string label, Style style)
        => gauge.Label(new Spans(label, style));
    
    public static LineGauge Label(this LineGauge gauge, IEnumerable<Span> label)
            => gauge.Label(new Spans(label.ToList()));
}
