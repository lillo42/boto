using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;

namespace Boto.Widget;

public class Axis
{
    public Spans? Title { get; set; }
    public double[] Bounds { get; set; } = { 0, 0 };
    public List<Span>? Labels { get; set; }
    public Style Style { get; set; }
    public Alignment LabelsAlignment { get; set; }
}
