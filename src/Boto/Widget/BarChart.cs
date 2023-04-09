using Boto.Extensions;
using Boto.Layouts;
using Boto.Styles;
using Boto.Symbols;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widget;

public class BarChart : IWidget
{
    public Block? Block { get; set; }
    public int BarWidth { get; set; } = 1;
    public int BarGap { get; set; } = 1;
    public long? Max { get; set; }
    public Set BarSet { get; set; } = Bar.NineLevels;
    public Style BarStyle { get; set; } = new();
    public Style LabelStyle { get; set; } = new();
    public Style ValueStyle { get; set; } = new();
    public Style Style { get; set; } = new();

    public List<IBarCharItem> Items { get; } = new();

    public void Render(Rect area, Buffer buffer)
    {
        buffer.SetStyle(area, Style);
        var chartArea = area;
        if (Block != null)
        {
            var inner = Block.Inner(area);
            Block.Render(area, buffer);
            chartArea = inner;
        }

        if (chartArea.Height < 2)
        {
            return;
        }

        var max = Max ?? Items.Max(x => x.Value);
        var maxIndex = Math.Min(chartArea.Width / (BarWidth + BarGap), Items.Count);

        var data = Items.Take(maxIndex)
            .Select(x => (x.Label, x.Value * (chartArea.Height - 1) * 8 / Math.Max(max, 1)))
            .ToList();

        for (var j = chartArea.Height - 2; j >= 0; j--)
        {
            for (var i = 0; i < data.Count; i++)
            {
                var (label, value) = data[i];
                var symbol = value switch
                {
                    0 => BarSet.Empty,
                    1 => BarSet.OneEighth,
                    2 => BarSet.OneQuarter,
                    3 => BarSet.ThreeEighths,
                    4 => BarSet.Half,
                    5 => BarSet.FiveEighths,
                    6 => BarSet.ThreeQuarters,
                    7 => BarSet.SevenEighths,
                    _ => BarSet.Full
                };

                for (var x = 0; x < BarWidth; x++)
                {
                    var indexX = chartArea.Left + i * (BarWidth + BarGap) + x;
                    var indexY = chartArea.Top + j;
                    buffer[indexX, indexY] = buffer[indexX, indexY].With(BarStyle) with { Symbol = symbol };
                }

                if (value > 8)
                {
                    data[i] = (label, value - 8);
                }
                else
                {
                    data[i] = (label, 0);
                }
            }
        }

        for (var i = 0; i < maxIndex; i++)
        {
            var label = Items[i].Label;
            var labelStyle = Items[i].LabelStyle ?? LabelStyle;
            var value = Items[i].Value;
            var valueLabel = Items[i].ValueLabel;
            var valueStyle = Items[i].ValueStyle ?? ValueStyle;

            if (value > 0)
            {
                var width = valueLabel.Width();
                if (width < BarWidth)
                {
                    buffer.SetString(
                        chartArea.Left + i * (BarWidth + BarGap) + (BarWidth - width) / 2,
                        chartArea.Bottom - 2,
                        valueLabel,
                        valueStyle
                    );
                }
            }

            buffer.SetString(chartArea.Left + i * (BarWidth + BarGap),
                chartArea.Bottom - 1,
                label,
                BarWidth,
                labelStyle);
        }
    }
}

