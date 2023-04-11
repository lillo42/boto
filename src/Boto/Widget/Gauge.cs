using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widget;

public class Gauge : IWidget
{
    public Block? Block { get; set; }
    public double Ratio { get; set; }
    public Span? Label { get; set; }
    public bool UseUnicode { get; set; }
    public Style Style { get; set; }
    public Style GaugeStyle { get; set; }

    public void Render(Rect area, Buffer buffer)
    {
        buffer.SetStyle(area, Style);
        var gaugeArea = area;
        if (Block != null)
        {
            var inner = Block.Inner(area);
            Block.Render(area, buffer);
            gaugeArea = inner;
        }

        buffer.SetStyle(gaugeArea, GaugeStyle);
        if (gaugeArea.Height < 1)
        {
            return;
        }

        // compute label value and its position
        // label is put at the center of the gauge_area
        var label = Label ?? $"{Math.Round(Ratio * 100)}%";

        var clampedLabelWidth = Math.Min(gaugeArea.Width, label.Width);
        var labelCol = gaugeArea.Left + (gaugeArea.Width - clampedLabelWidth) / 2;
        var labelRow = gaugeArea.Top + gaugeArea.Height / 2;

        // the gauge will be filled proportionally to the ratio
        var filledWith = gaugeArea.Width * Ratio;
        var end = (int)(gaugeArea.Left + (UseUnicode ? Math.Floor(filledWith) : Math.Round(filledWith)));

        for (var y = gaugeArea.Top; y < gaugeArea.Bottom; y++)
        {
            // render the filled area (left to end)
            for (var x = gaugeArea.Left; x < end; x++)
            {
                // spaces are needed to apply the background styling
                buffer[x, y] = buffer[x, y] with
                {
                    Symbol = " ",
                    Foreground = GaugeStyle.Background ?? Color.Reset,
                    BackgroundColor = GaugeStyle.Foreground ?? Color.Reset
                };
            }

            if (UseUnicode && Ratio < 1)
            {
                buffer[end, y] = buffer[end, y] with
                {
                    Symbol = Convert.ToInt32(Math.Round(filledWith % 1 * 8)) switch
                    {
                        1 => Symbols.Block.OneEighth,
                        2 => Symbols.Block.OneQuarter,
                        3 => Symbols.Block.ThreeEighths,
                        4 => Symbols.Block.Half,
                        5 => Symbols.Block.FiveEighths,
                        6 => Symbols.Block.ThreeQuarters,
                        7 => Symbols.Block.SevenEighths,
                        8 => Symbols.Block.Full,
                        _ => " "
                    }
                };
            }
        }

        buffer.SetSpan(labelCol, labelRow, label, clampedLabelWidth);
    }
}
