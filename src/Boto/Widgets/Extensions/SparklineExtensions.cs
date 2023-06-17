using Boto.Styles;
using Boto.Symbols;

namespace Boto.Widgets.Extensions;

/// <summary>
/// The <see cref="Sparkline"/> extensions method.
/// </summary>
public static class SparklineExtensions
{
    /// <summary>
    /// Change the <see cref="Sparkline.Block"/>.
    /// </summary>
    /// <param name="sparkline">The <see cref="Sparkline"/>.</param>
    /// <param name="block">The <see cref="Block"/>.</param>
    /// <returns>The <paramref name="sparkline"/> with <see cref="Sparkline.Block"/> as <paramref name="block"/>.</returns>
    public static Sparkline SetBlock(this Sparkline sparkline, Block block)
    {
        sparkline.Block = block;
        return sparkline;
    }

    /// <summary>
    /// Change the <see cref="Sparkline.Style"/>.
    /// </summary>
    /// <param name="sparkline">The <see cref="Sparkline"/>.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The <paramref name="sparkline"/> with <see cref="Sparkline.Style"/> as <paramref name="style"/>.</returns>
    public static Sparkline SetStyle(this Sparkline sparkline, Style style)
    {
        sparkline.Style = style;
        return sparkline;
    }

    /// <summary>
    /// Change the <see cref="Sparkline.Max"/>.
    /// </summary>
    /// <param name="sparkline">The <see cref="Sparkline"/>.</param>
    /// <param name="max">The max.</param>
    /// <returns>The <paramref name="sparkline"/> with <see cref="Sparkline.Max"/> as <paramref name="max"/>.</returns>
    public static Sparkline SetMax(this Sparkline sparkline, int? max)
    {
        sparkline.Max = max;
        return sparkline;
    }

    /// <summary>
    /// Change the <see cref="Sparkline.BarSet"/>.
    /// </summary>
    /// <param name="sparkline">The <see cref="Sparkline"/>.</param>
    /// <param name="barSet">The <see cref="Set"/>.</param>
    /// <returns>The <paramref name="sparkline"/> with <see cref="Sparkline.BarSet"/> as <paramref name="barSet"/>.</returns>
    public static Sparkline SetBarSet(this Sparkline sparkline, Set barSet)
    {
        sparkline.BarSet = barSet;
        return sparkline;
    }

    /// <summary>
    /// Add the <paramref name="item"/> to <see cref="Sparkline.Items"/>.
    /// </summary>
    /// <param name="sparkline">The <see cref="Sparkline"/>.</param>
    /// <param name="item">The item.</param>
    /// <returns>The <paramref name="sparkline"/> with <see cref="Sparkline.Items"/> plus <paramref name="item"/>.</returns>
    public static Sparkline AddItem(this Sparkline sparkline, int item)
    {
        sparkline.Items.Add(item);
        return sparkline;
    }

    /// <summary>
    /// Add the <paramref name="items"/> to <see cref="Sparkline.Items"/>.
    /// </summary>
    /// <param name="sparkline">The <see cref="Sparkline"/>.</param>
    /// <param name="items">The items collection.</param>
    /// <returns>The <paramref name="sparkline"/> with <see cref="Sparkline.Items"/> plus <paramref name="items"/>.</returns>
    public static Sparkline AddItems(this Sparkline sparkline, IEnumerable<int> items)
    {
        sparkline.Items.AddRange(items.ToList());
        return sparkline;
    }

    /// <summary>
    /// Add the <paramref name="items"/> to <see cref="Sparkline.Items"/>.
    /// </summary>
    /// <param name="sparkline">The <see cref="Sparkline"/>.</param>
    /// <param name="items">The items collection.</param>
    /// <returns>The <paramref name="sparkline"/> with <see cref="Sparkline.Items"/> plus <paramref name="items"/>.</returns>
    public static Sparkline AddItems(this Sparkline sparkline, params int[] items)
    {
        sparkline.Items.AddRange(items.ToList());
        return sparkline;
    }
}
