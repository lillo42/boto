using Boto.Layouts;
using Boto.Styles;
using Boto.Symbols;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widgets;

/// <summary>
/// The sparkline widget.
/// </summary>
public class Sparkline : IWidget
{
    /// <summary>
    /// The <see cref="Widgets.Block"/>.
    /// </summary>
    public Block? Block { get; set; }

    /// <summary>
    /// The <see cref="Styles.Style"/>.
    /// </summary>
    public Style Style { get; set; } = new();

    /// <summary>
    /// The max value.
    /// </summary>
    public int? Max { get; set; }

    /// <summary>
    /// The bar <see cref="Set"/>.
    /// </summary>
    public Set BarSet { get; set; } = Bar.NineLevels;

    /// <summary>
    /// The items collection.
    /// </summary>
    public List<int> Items { get; set; } = new();

    /// <inheritdoc cref="IWidget.Render"/>
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
                buffer[x, y] = buffer[x, y].With(Style) with { Symbol = symbol };

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
