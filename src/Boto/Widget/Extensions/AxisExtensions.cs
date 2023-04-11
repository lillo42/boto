using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;

namespace Boto.Widget;

public static class AxisExtensions
{
    public static Axis Title(this Axis axis, string title)
    {
        axis.Title = new Spans(title);
        return axis;
    }

    public static Axis Title(this Axis axis, string title, Style style)
    {
        axis.Title = new Spans(title, style);
        return axis;
    }

    public static Axis Title(this Axis axis, Spans title)
    {
        axis.Title = title;
        return axis;
    }

    public static Axis Bounds(this Axis axis, double min, double max)
    {
        axis.Bounds = new[] { min, max };
        return axis;
    }
    
    public static Axis Bounds(this Axis axis, double[] bounds)
    {
        axis.Bounds = bounds;
        return axis;
    }

    public static Axis AddLabels(this Axis axis, string label)
    {
        axis.Labels ??= new();
        axis.Labels.Add(label);
        return axis;
    }
    
    public static Axis AddLabels(this Axis axis, string label, Style style)
    {
        axis.Labels ??= new();
        axis.Labels.Add(new(label, style));
        return axis;
    }
    
    public static Axis AddLabels(this Axis axis, IEnumerable<string> labels)
    {
        axis.Labels ??= new();
        axis.Labels.AddRange(labels.Select(l => new Span(l)));
        return axis;
    }

    public static Axis AddLabels(this Axis axis, params string[] labels)
    {
        axis.Labels ??= new();
        axis.Labels.AddRange(labels.Select(l => new Span(l)));
        return axis;
    }

    public static Axis AddLabels(this Axis axis, IEnumerable<Span> labels)
    {
        axis.Labels ??= new();
        axis.Labels.AddRange(labels);
        return axis;
    }

    public static Axis AddLabels(this Axis axis, params Span[] labels)
    {
        axis.Labels ??= new();
        axis.Labels.AddRange(labels);
        return axis;
    }
    
    public static Axis Style(this Axis axis, Style style)
    {
        axis.Style = style;
        return axis;
    }
    
    public static Axis LabelsAlignment(this Axis axis, Alignment alignment)
    {
        axis.LabelsAlignment = alignment;
        return axis;
    }
}
