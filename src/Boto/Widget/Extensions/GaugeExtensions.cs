using Boto.Styles;
using Boto.Texts;

namespace Boto.Widget;

public static class GaugeExtensions
{
    public static Gauge Block(this Gauge gauge, Block block)
    {
        gauge.Block = block;
        return gauge;
    }

    public static Gauge Percent(this Gauge gauge, int percent)
    {
        gauge.Ratio = percent / 100.0;
        return gauge;
    }

    public static Gauge Ratio(this Gauge gauge, double ratio)
    {
        gauge.Ratio = ratio;
        return gauge;
    }

    public static Gauge Label(this Gauge gauge, string label)
    {
        gauge.Label = new Span(label);
        return gauge;
    }

    public static Gauge Label(this Gauge gauge, string label, Style style)
    {
        gauge.Label = new Span(label, style);
        return gauge;
    }

    public static Gauge Label(this Gauge gauge, Span label)
    {
        gauge.Label = label;
        return gauge;
    }

    public static Gauge Style(this Gauge gauge, Style style)
    {
        gauge.Style = style;
        return gauge;
    }

    public static Gauge GaugeStyle(this Gauge gauge, Style style)
    {
        gauge.GaugeStyle = style;
        return gauge;
    }

    public static Gauge Unicode(this Gauge gauge, bool useUnicode)
    {
        gauge.UseUnicode = useUnicode;
        return gauge;
    }
    
    public static Gauge EnableUnicode(this Gauge gauge)
        => gauge.Unicode(true);
    
    public static Gauge DisableUnicode(this Gauge gauge)
        => gauge.Unicode(false);
}
