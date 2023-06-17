using Boto.Extensions;
using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;
using Boto.Widgets.Canvas;
using Boto.Widgets.Extensions;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Widgets;

/// <summary>
/// The chart widget.
/// </summary>
public class Chart : IWidget
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Chart"/> class.
    /// </summary>
    public Chart()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Chart"/> class.
    /// </summary>
    /// <param name="datasets">The <see cref="IEnumerable{T}"/> of <see cref="Dataset"/>.</param>
    public Chart(IEnumerable<Dataset> datasets)
    {
        Datasets = datasets.ToList();
    }

    /// <summary>
    /// The <see cref="Widgets.Block"/>
    /// </summary>
    public Block? Block { get; set; }
    
    /// <summary>
    /// The X <see cref="Axis"/>.
    /// </summary>
    public Axis XAxis { get; set; } = new();
    
    /// <summary>
    /// The Y <see cref="Axis"/>.
    /// </summary>
    public Axis YAxis { get; set; } = new();
    
    /// <summary>
    /// The collection of <see cref="Dataset"/>.
    /// </summary>
    public List<Dataset> Datasets { get; set; } = new();
    
    /// <summary>
    /// The <see cref="Styles.Style"/>.
    /// </summary>
    public Style Style { get; set; }

    /// <summary>
    /// The hidden legend <see cref="IConstraint"/>.
    /// </summary>
    public (IConstraint Width, IConstraint Height) HiddenLegendConstraint { get; set; } =
        (Constraints.Ratio(1, 4), Constraints.Ratio(1, 4));

    private ChartLayout CreateLayout(Rect area)
    {
        var layout = new ChartLayout();

        if (area.Height == 0 || area.Width == 0)
        {
            return layout;
        }

        var x = area.Left;
        var y = area.Bottom - 1;

        if (XAxis.Labels != null && y > area.Top)
        {
            layout.LabelX = y;
            y--;
        }

        layout.LabelY = YAxis.Labels == null ? null : x;
        x += MaxWidthOfLabelsLeftOfYAxis(area, YAxis.Labels != null);

        if (XAxis.Labels != null && y > area.Top)
        {
            layout.AxisX = y;
            y--;
        }

        if (YAxis.Labels != null && x + 1 < area.Right)
        {
            layout.AxisY = x;
            x++;
        }

        if (x < area.Right && y > 1)
        {
            layout.GraphArea = new Rect(x, area.Top, area.Right - x, y - area.Top + 1);
        }

        if (XAxis.Title != null)
        {
            var w = XAxis.Title.Width;
            if (w < layout.GraphArea.Width && layout.GraphArea.Height > 2)
            {
                layout.TitleX = new Position(x + layout.GraphArea.Width - w, y);
            }
        }

        if (YAxis.Title != null)
        {
            var w = YAxis.Title.Width;
            if (w + 1 < layout.GraphArea.Width && layout.GraphArea.Height > 2)
            {
                layout.TitleY = new Position(x, area.Top);
            }
        }

        if (Datasets.Count > 0)
        {
            var innerWidth = Datasets.Select(dataset => dataset.Name.Width()).Max();
            var legendWidth = innerWidth + 2;
            var legendHeight = Datasets.Count + 2;
            var maxWidth = HiddenLegendConstraint.Width.Apply(layout.GraphArea.Width);
            var maxHeight = HiddenLegendConstraint.Height.Apply(layout.GraphArea.Height);
            if (innerWidth > 0
                && legendWidth < maxWidth
                && legendHeight < maxHeight)
            {
                layout.LegendArea = new Rect(
                    layout.GraphArea.Right - legendWidth,
                    layout.GraphArea.Top,
                    legendWidth,
                    legendHeight);
            }
        }


        return layout;
    }

    private int MaxWidthOfLabelsLeftOfYAxis(Rect area, bool hasYAxis)
    {
        var maxWidth = YAxis.Labels?.Max(x => x.Width) ?? 0;
        if (XAxis.Labels != null)
        {
            var firstXLabel = XAxis.Labels[0];
            var firstLabelWidth = firstXLabel.Width;
            var widthLeftOfYAxis = XAxis.LabelsAlignment switch
            {
                // The last character of the label should be below the Y-Axis when it exists, not on its left
                Alignment.Left => firstLabelWidth.SaturatingSub(hasYAxis ? 1 : 0),
                Alignment.Center => firstLabelWidth / 2,
                Alignment.Right => 0,
                _ => throw new ArgumentOutOfRangeException()
            };

            maxWidth = Math.Max(maxWidth, widthLeftOfYAxis);
        }

        // labels of y axis and first label of x axis can take at most 1/3rd of the total width
        return Math.Min(maxWidth, area.Width / 3);
    }

    private void RenderXLabels(Buffer buffer, ChartLayout layout, Rect chartArea, Rect graphArea)
    {
        if (layout.LabelX is null || XAxis.Labels is null || XAxis.Labels.Count < 2)
        {
            return;
        }

        int x;
        var y = layout.LabelX.Value;
        var labels = XAxis.Labels!;
        var widthBetweenTicks = graphArea.Width / labels.Count;

        var labelArea = FirstXLabelArea(
            y,
            labels[0].Width,
            widthBetweenTicks,
            chartArea,
            graphArea);

        var labelAlignment = XAxis.LabelsAlignment switch
        {
            Alignment.Left => Alignment.Right,
            Alignment.Center => Alignment.Center,
            Alignment.Right => Alignment.Left,
            _ => throw new ArgumentOutOfRangeException()
        };

        RenderLabel(buffer, labels[0], labelArea, labelAlignment);

        foreach (var (i, label) in labels.Skip(1).Take(labels.Count - 2).WithIndex())
        {
            // We add 1 to x (and width-1 below) to leave at least one space before each intermediate labels
            x = graphArea.Left + (i + 1) * widthBetweenTicks + 1;
            labelArea = new Rect(x, y, widthBetweenTicks.SaturatingSub(1), 1);

            RenderLabel(buffer, label, labelArea, Alignment.Center);
        }

        x = graphArea.Right - widthBetweenTicks;
        labelArea = new Rect(x, y, widthBetweenTicks, 1);

        // The last label should be aligned Right to be at the edge of the graph area
        RenderLabel(buffer, labels[^1], labelArea, Alignment.Right);
    }

    private void RenderYLabels(Buffer buffer, ChartLayout layout, Rect chartArea, Rect graphArea)
    {
        if (layout.LabelY is null || YAxis.Labels is null)
        {
            return;
        }

        var x = layout.LabelY.Value;
        var labels = YAxis.Labels!;
        var labelCount = labels.Count;
        foreach (var (i, label) in labels.WithIndex())
        {
            var dy = i * (graphArea.Height - 1) / (labelCount - 1);
            if (dy < graphArea.Bottom)
            {
                var labelArea = new Rect(x,
                    graphArea.Bottom.SaturatingSub(1) - dy,
                    (graphArea.Left - chartArea.Left).SaturatingSub(1),
                    1);

                RenderLabel(buffer, label, labelArea, YAxis.LabelsAlignment);
            }
        }
    }

    private Rect FirstXLabelArea(int y, int labelWidth, int maxWidthAfterYAxis, Rect chartArea, Rect graphArea)
    {
        var (minX, maxX) = XAxis.LabelsAlignment switch
        {
            Alignment.Left => (chartArea.Left, graphArea.Left),
            Alignment.Center => (chartArea.Left, graphArea.Left + Math.Min(maxWidthAfterYAxis, labelWidth)),
            Alignment.Right => (graphArea.Left.SaturatingSub(1), graphArea.Left + maxWidthAfterYAxis),
            _ => throw new ArgumentOutOfRangeException()
        };

        return new Rect(minX, y, maxX - minX, 1);
    }

    private static void RenderLabel(Buffer buffer, Span label, Rect labelArea, Alignment alignment)
    {
        var labelWidth = label.Width;
        var boundedLabelWidth = Math.Min(labelWidth, labelArea.Width);

        var x = alignment switch
        {
            Alignment.Left => labelArea.Left,
            Alignment.Center => labelArea.Left + labelArea.Width / 2 - boundedLabelWidth / 2,
            Alignment.Right => labelArea.Right - boundedLabelWidth,
            _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null)
        };

        buffer.SetSpan(x, labelArea.Top, label, boundedLabelWidth);
    }

    /// <inheritdoc cref="IWidget.Render"/>
    public void Render(Rect area, Buffer buffer)
    {
        if (area.Area == 0)
        {
            return;
        }

        buffer.SetStyle(area, Style);
        // Sample the style of the entire widget. This sample will be used to reset the style of
        // the cells that are part of the components put on top of the grah area (i.e legend and
        // axis names).
        var originalStyle = buffer[area.Left, area.Top].Style;

        var chartArea = area;
        if (Block != null)
        {
            var inner = Block.Inner(area);
            Block.Render(area, buffer);
            chartArea = inner;
        }

        var layout = CreateLayout(chartArea);
        var graphArea = layout.GraphArea;
        if (graphArea.Width < 1 || graphArea.Height < 1)
        {
            return;
        }

        RenderXLabels(buffer, layout, chartArea, graphArea);
        RenderYLabels(buffer, layout, chartArea, graphArea);

        int x;
        int y;
        if (layout.AxisX.HasValue)
        {
            y = layout.AxisX.Value;
            for (x = graphArea.Left; x < graphArea.Right; x++)
            {
                buffer[x, y] = buffer[x, y].With(XAxis.Style) with { Symbol = Symbols.Line.Horizontal };
            }
        }

        if (layout.AxisY.HasValue)
        {
            x = layout.AxisY.Value;
            for (y = graphArea.Top; y < graphArea.Bottom; y++)
            {
                buffer[x, y] = buffer[x, y].With(YAxis.Style) with { Symbol = Symbols.Line.Vertical };
            }
        }

        if (layout is { AxisX: { }, AxisY: { } })
        {
            y = layout.AxisX.Value;
            x = layout.AxisY.Value;
            buffer[x, y] = buffer[x, y].With(XAxis.Style) with { Symbol = Symbols.Line.BottomLeft };
        }

        foreach (var dataset in Datasets)
        {
            new Canvas.Canvas()
                .SetBackground(Style.Background ?? Color.Reset)
                .SetXBounds(XAxis.Bounds)
                .SetYBounds(YAxis.Bounds)
                .SetMarker(dataset.Marker)
                .SetPaint(ctx =>
                {
                    ctx.Draw(new Points(dataset.Data, dataset.Style.Foreground ?? Color.Reset));
                    if (dataset.GraphType == GraphType.Line)
                    {
                        foreach (var data in dataset.Data.Windows(2))
                        {
                            ctx.Draw(new Line(data[0].Item1,
                                data[0].Item2,
                                data[1].Item1,
                                data[1].Item2,
                                dataset.Style.Foreground ?? Color.Reset));
                        }
                    }
                }).Render(graphArea, buffer);
        }

        if (layout.LegendArea is { } legendArea)
        {
            buffer.SetStyle(legendArea, originalStyle);
            new Block()
                .SetBorders(Borders.All)
                .Render(legendArea, buffer);

            foreach (var (i, dataset) in Datasets.WithIndex())
            {
                buffer.SetString(
                    legendArea.X + 1,
                    legendArea.Y + i + 1,
                    dataset.Name,
                    dataset.Style);
            }
        }

        if (layout.TitleX is { })
        {
            (x, y) = layout.TitleX.Value;
            var title = XAxis.Title!;
            var width = graphArea.Right.SaturatingSub(x);
            buffer.SetStyle(new Rect(x, y, width, 1), originalStyle);
            buffer.SetSpan(x, y, title, width);
        }

        if (layout.TitleY is { })
        {
            (x, y) = layout.TitleY.Value;
            var title = YAxis.Title!;
            var width = graphArea.Right.SaturatingSub(y);
            buffer.SetStyle(new Rect(x, y, width, 1), originalStyle);
            buffer.SetSpan(x, y, title, width);
        }
    }
}
