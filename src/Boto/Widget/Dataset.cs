using Boto.Styles;

namespace Boto.Widget;

public class Dataset
{
    public string Name { get; set; } = string.Empty;
    public GraphType GraphType { get; set; } = GraphType.Scatter;
    public Style Style { get; set; }
    public Symbols.Marker Marker { get; set; } = Symbols.Marker.Dot;
    public (double, double)[] Data { get; set; } = Array.Empty<(double, double)>();
}
