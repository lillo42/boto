using Boto.Layouts;

namespace Boto.Widget;

public class ChartLayout
{
    public Position? TitleX { get; set;}
    public Position? TitleY { get; set;}
    public int? LabelX { get; set;}
    public int? LabelY { get; set;}
    public int? AxisX { get; set;}
    public int? AxisY { get; set;}
    public Rect? LegendArea { get; set;}
    public Rect GraphArea { get; set;}
}
