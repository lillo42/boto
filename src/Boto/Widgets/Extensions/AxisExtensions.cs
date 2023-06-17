using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;

namespace Boto.Widgets.Extensions;

/// <summary>
/// The <see cref="Axis"/> extensions method.
/// </summary>
public static class AxisExtensions
{
    /// <summary>
    /// Change <see cref="Axis.Title"/>.
    /// </summary>
    /// <param name="axis">The <see cref="Axis"/>.</param>
    /// <param name="title">The title.</param>
    /// <returns>The <paramref name="axis"/> with <see cref="Axis.Title"/> as <paramref name="title"/>.</returns>
    public static Axis SetTitle(this Axis axis, string title)
    {
        axis.Title = new Spans(title);
        return axis;
    }

    /// <summary>
    /// Change <see cref="Axis.Title"/>.
    /// </summary>
    /// <param name="axis">The <see cref="Axis"/>.</param>
    /// <param name="title">The title.</param>
    /// <param name="style">The <see cref="Styles.Style"/>.</param>
    /// <returns>The <paramref name="axis"/> with <see cref="Axis.Title"/> as <paramref name="title"/>.</returns>
    public static Axis SetTitle(this Axis axis, string title, Style style)
    {
        axis.Title = new Spans(title, style);
        return axis;
    }

    /// <summary>
    /// Change <see cref="Axis.Title"/>.
    /// </summary>
    /// <param name="axis">The <see cref="Axis"/>.</param>
    /// <param name="title">The title as <see cref="Spans"/>.</param>
    /// <returns>The <paramref name="axis"/> with <see cref="Axis.Title"/> as <paramref name="title"/>.</returns>
    public static Axis SetTitle(this Axis axis, Spans title)
    {
        axis.Title = title;
        return axis;
    }

    /// <summary>
    /// Change <see cref="Axis.Bounds"/>.
    /// </summary>
    /// <param name="axis">The <see cref="Axis"/>.</param>
    /// <param name="min">The min value.</param>
    /// <param name="max">The max value.</param>
    /// <returns>The <paramref name="axis"/> with <see cref="Axis.Bounds"/> as <paramref name="min"/> and <paramref name="max"/>.</returns>
    public static Axis SetBounds(this Axis axis, double min, double max)
    {
        axis.Bounds = new[] { min, max };
        return axis;
    }

    /// <summary>
    /// Change <see cref="Axis.Bounds"/>.
    /// </summary>
    /// <param name="axis">The <see cref="Axis"/>.</param>
    /// <param name="bounds">The bounds.</param>
    /// <returns>The <paramref name="axis"/> with <see cref="Axis.Bounds"/> as <paramref name="bounds"/>.</returns>
    public static Axis SetBounds(this Axis axis, double[] bounds)
    {
        axis.Bounds = bounds;
        return axis;
    }

    /// <summary>
    /// Add labels to <see cref="Axis.Labels"/>.
    /// </summary>
    /// <param name="axis">The <see cref="Axis"/>.</param>
    /// <param name="label">The label</param>
    /// <returns>The <paramref name="axis"/> with <see cref="Axis.Labels"/> plus <paramref name="label"/>.</returns>
    public static Axis AddLabels(this Axis axis, string label)
    {
        axis.Labels ??= new();
        axis.Labels.Add(label);
        return axis;
    }

    /// <summary>
    /// Add labels to <see cref="Axis.Labels"/>.
    /// </summary>
    /// <param name="axis">The <see cref="Axis"/>.</param>
    /// <param name="label">The label</param>
    /// <param name="style">The <see cref="Styles.Style"/>.</param>
    /// <returns>The <paramref name="axis"/> with <see cref="Axis.Labels"/> plus <paramref name="label"/>.</returns>
    public static Axis AddLabels(this Axis axis, string label, Style style)
    {
        axis.Labels ??= new();
        axis.Labels.Add(new(label, style));
        return axis;
    }

    /// <summary>
    /// Add labels to <see cref="Axis.Labels"/>.
    /// </summary>
    /// <param name="axis">The <see cref="Axis"/>.</param>
    /// <param name="labels">The collection of label.</param>
    /// <returns>The <paramref name="axis"/> with <see cref="Axis.Labels"/> plus <paramref name="labels"/>.</returns>
    public static Axis AddLabels(this Axis axis, IEnumerable<string> labels)
    {
        axis.Labels ??= new();
        axis.Labels.AddRange(labels.Select(l => new Span(l)));
        return axis;
    }

    /// <summary>
    /// Add labels to <see cref="Axis.Labels"/>.
    /// </summary>
    /// <param name="axis">The <see cref="Axis"/>.</param>
    /// <param name="labels">The collection of label.</param>
    /// <returns>The <paramref name="axis"/> with <see cref="Axis.Labels"/> plus <paramref name="labels"/>.</returns>
    public static Axis AddLabels(this Axis axis, params string[] labels)
    {
        axis.Labels ??= new();
        axis.Labels.AddRange(labels.Select(l => new Span(l)));
        return axis;
    }

    /// <summary>
    /// Add labels to <see cref="Axis.Labels"/>.
    /// </summary>
    /// <param name="axis">The <see cref="Axis"/>.</param>
    /// <param name="label">The label as <see cref="Span"/>.</param>
    /// <returns>The <paramref name="axis"/> with <see cref="Axis.Labels"/> plus <paramref name="label"/>.</returns>
    public static Axis AddLabel(this Axis axis, Span label)
    {
        axis.Labels ??= new();
        axis.Labels.Add(label);
        return axis;
    }

    /// <summary>
    /// Add labels to <see cref="Axis.Labels"/>.
    /// </summary>
    /// <param name="axis">The <see cref="Axis"/>.</param>
    /// <param name="labels">The collection of label as <see cref="Span"/>.</param>
    /// <returns>The <paramref name="axis"/> with <see cref="Axis.Labels"/> plus <paramref name="labels"/>.</returns>
    public static Axis AddLabels(this Axis axis, IEnumerable<Span> labels)
    {
        axis.Labels ??= new();
        axis.Labels.AddRange(labels);
        return axis;
    }

    /// <summary>
    /// Add labels to <see cref="Axis.Labels"/>.
    /// </summary>
    /// <param name="axis">The <see cref="Axis"/>.</param>
    /// <param name="labels">The collection label as <see cref="Span"/>.</param>
    /// <returns>The <paramref name="axis"/> with <see cref="Axis.Labels"/> plus <paramref name="labels"/>.</returns>
    public static Axis AddLabels(this Axis axis, params Span[] labels)
    {
        axis.Labels ??= new();
        axis.Labels.AddRange(labels);
        return axis;
    }

    /// <summary>
    /// Change <see cref="Axis.Style"/>.
    /// </summary>
    /// <param name="axis">The <see cref="Axis"/>.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    /// <returns>The <paramref name="axis"/> with <see cref="Axis.Style"/> plus <paramref name="style"/>.</returns>
    public static Axis SetStyle(this Axis axis, Style style)
    {
        axis.Style = style;
        return axis;
    }

    /// <summary>
    /// Change <see cref="Axis.LabelsAlignment"/>.
    /// </summary>
    /// <param name="axis">The <see cref="Axis"/>.</param>
    /// <param name="alignment">The label <see cref="Alignment"/>.</param>
    /// <returns>The <paramref name="axis"/> with <see cref="Axis.LabelsAlignment"/> plus <paramref name="alignment"/>.</returns>
    public static Axis SetLabelsAlignment(this Axis axis, Alignment alignment)
    {
        axis.LabelsAlignment = alignment;
        return axis;
    }
}
