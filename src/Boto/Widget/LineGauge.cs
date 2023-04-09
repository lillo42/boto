using Boto.Extensions;
using Boto.Layouts;
using Boto.Styles;
using Boto.Symbols;
using Boto.Texts;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widget;

public class LineGauge : IWidget
{
    public Block? Block { get; set; }
    public double Ratio { get; set; }
    public Spans? Label { get; set; }
    public Line.Set LineSet { get; set; } = Line.Normal;
    public Style Style { get; set; }
    public Style GaugeStyle { get; set; }


    public void Render(Rect area, Buffer buffer)
    {
        buffer.SetStyle(area, Style);
        var gaugeArea = area;
        if (Block != null)
        {
            var innerArea = Block.Inner(area);
            Block.Render(area, buffer);
            gaugeArea = innerArea;
        }

        if (gaugeArea.Height < 1)
        {
            return;
        }

        var ratio = Ratio;
        var label = Label ?? $"{ratio * 100}%";

        var (col, row) = buffer.SetSpan(gaugeArea.Left, gaugeArea.Top, label, gaugeArea.Width);

        var start = col + 1;
        if (start >= gaugeArea.Right)
        {
            return;
        }

        var end = (int)Math.Floor(start + gaugeArea.Right.SaturatingSub(start) * Ratio);
        for (var x = start; x < end; x++)
        {
            buffer[x, row] = buffer[x, row].With(new()
                {
                    Foreground = GaugeStyle.Foreground,
                    Background = null,
                    AddModifier = GaugeStyle.AddModifier,
                    RemoveModifier = GaugeStyle.RemoveModifier
                }) with
                {
                    Symbol = LineSet.Horizontal
                };
        }

        for (var x = end; x < gaugeArea.Right; x++)
        {
            buffer[x, row] = buffer[x, row].With(new()
                {
                    Foreground = GaugeStyle.Foreground,
                    Background = null,
                    AddModifier = GaugeStyle.AddModifier,
                    RemoveModifier = GaugeStyle.RemoveModifier
                }) with
                {
                    Symbol = LineSet.Horizontal
                };
        }
    }
}
