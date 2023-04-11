using Boto.Layouts;
using Boto.Styles;
using Boto.Symbols;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widget;

public class Sparkline : IWidget
{
    public Block? Block { get; set; }
    public Style Style { get; set; } = new();
    public int? Max { get; set; }
    public Set BarSet { get; set; } = Bar.NineLevels;
    public List<int> Items { get; set; } = new();

    public void Render(Rect area, Buffer buffer)
    {
        var sparkArea = area;
        if (Block != null)
        {
            var inner = Block.Inner(area);
            Block.Render(area, buffer);
            sparkArea = inner;
        }

        if (sparkArea.Height < 1)
        {
            return;
        }

        var max = Max ?? (Items.Count == 0 ? 1 : Items.Max());
        var maxIndex = Math.Min(sparkArea.Width, Items.Count);
        var data = Items.Take(maxIndex)
            .Select(x => max == 0 ? 0 : x * sparkArea.Height * 8 / max)
            .ToList();

        for (var j = sparkArea.Height - 1; j >= 0; j--)
        {
            for (var i = 0; i < data.Count; i++)
            {
                var d = data[i];
                var symbol = d switch
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

                var x = sparkArea.Left + i;
                var y = sparkArea.Top + j;
                buffer[x, y] = buffer[x, y].With(Style) with
                {
                    Symbol = symbol
                };

                if (d > 8)
                {
                    data[i] = d - 8;
                }
                else
                {
                    data[i] = 0;
                }
            }
        }
    }
}
