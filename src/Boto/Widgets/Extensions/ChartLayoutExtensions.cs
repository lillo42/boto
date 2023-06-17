using Boto.Layouts;

namespace Boto.Widgets.Extensions;

/// <summary>
/// The <see cref="Chart"/> layout extensions.
/// </summary>
public static class ChartLayoutExtensions
{
    /// <summary>
    /// Change the <see cref="ChartLayout.TitleX"/>.
    /// </summary>
    /// <param name="layout">The <see cref="ChartLayout"/>.</param>
    /// <param name="position">The title <see cref="Position"/>.</param>
    /// <returns>The <paramref name="layout"/> with the <see cref="ChartLayout.TitleX"/> <paramref name="position"/>.</returns>
    public static ChartLayout SetTitleX(this ChartLayout layout, Position position)
    {
        layout.TitleX = position;
        return layout;
    }

    /// <summary>
    /// Change the <see cref="ChartLayout.TitleY"/>.
    /// </summary>
    /// <param name="layout">The <see cref="ChartLayout"/>.</param>
    /// <param name="position">The title <see cref="Position"/>.</param>
    /// <returns>The <paramref name="layout"/> with the <see cref="ChartLayout.TitleY"/> <paramref name="position"/>.</returns>
    public static ChartLayout SetTitleY(this ChartLayout layout, Position position)
    {
        layout.TitleY = position;
        return layout;
    }

    /// <summary>
    /// Change the <see cref="ChartLayout.LabelX"/>.
    /// </summary>
    /// <param name="layout">The <see cref="ChartLayout"/>.</param>
    /// <param name="label">The label X.</param>
    /// <returns>The <paramref name="layout"/> with the <see cref="ChartLayout.LabelX"/> <paramref name="label"/>.</returns>
    public static ChartLayout SetLabelX(this ChartLayout layout, int? label)
    {
        layout.LabelX = label;
        return layout;
    }

    /// <summary>
    /// Change the <see cref="ChartLayout.LabelY"/>.
    /// </summary>
    /// <param name="layout">The <see cref="ChartLayout"/>.</param>
    /// <param name="label">The label Y.</param>
    /// <returns>The <paramref name="layout"/> with the <see cref="ChartLayout.LabelY"/> <paramref name="label"/>.</returns>
    public static ChartLayout SetLabelY(this ChartLayout layout, int? label)
    {
        layout.LabelY = label;
        return layout;
    }

    /// <summary>
    /// Change the <see cref="ChartLayout.AxisX"/>.
    /// </summary>
    /// <param name="layout">The <see cref="ChartLayout"/>.</param>
    /// <param name="axis">The axis X.</param>
    /// <returns>The <paramref name="layout"/> with the <see cref="ChartLayout.AxisX"/> <paramref name="axis"/>.</returns>
    public static ChartLayout SetAxisX(this ChartLayout layout, int? axis)
    {
        layout.AxisX = axis;
        return layout;
    }

    /// <summary>
    /// Change the <see cref="ChartLayout.AxisY"/>.
    /// </summary>
    /// <param name="layout">The <see cref="ChartLayout"/>.</param>
    /// <param name="label">The axis Y.</param>
    /// <returns>The <paramref name="layout"/> with the <see cref="ChartLayout.AxisY"/> <paramref name="label"/>.</returns>
    public static ChartLayout SetAxisY(this ChartLayout layout, int? label)
    {
        layout.AxisY = label;
        return layout;
    }

    /// <summary>
    /// Change the <see cref="ChartLayout.LegendArea"/>.
    /// </summary>
    /// <param name="layout">The <see cref="ChartLayout"/>.</param>
    /// <param name="area">The legend <see cref="Rect"/>.</param>
    /// <returns>The <paramref name="layout"/> with the <see cref="ChartLayout.LegendArea"/> <paramref name="area"/>.</returns>
    public static ChartLayout SetLegendArea(this ChartLayout layout, Rect? area)
    {
        layout.LegendArea = area;
        return layout;
    }

    /// <summary>
    /// Change the <see cref="ChartLayout.GraphArea"/>.
    /// </summary>
    /// <param name="layout">The <see cref="ChartLayout"/>.</param>
    /// <param name="graph">The graph <see cref="Rect"/>.</param>
    /// <returns>The <paramref name="layout"/> with the <see cref="ChartLayout.GraphArea"/> <paramref name="graph"/>.</returns>
    public static ChartLayout SetGraphArea(this ChartLayout layout, Rect? graph)
    {
        layout.LegendArea = graph;
        return layout;
    }
}
