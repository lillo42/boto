using Boto.Styles;

namespace Boto.Widgets;

/// <summary>
/// The dataset use in <see cref="Chart"/>.
/// </summary>
public class Dataset
{
    /// <summary>
    /// The name.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// The <see cref="Widgets.GraphType"/>.
    /// </summary>
    public GraphType GraphType { get; set; } = GraphType.Scatter;
    
    /// <summary>
    /// The <see cref="Styles.Style"/>.
    /// </summary>
    public Style Style { get; set; }
    
    /// <summary>
    /// The symbol <see cref="Symbols.Marker"/>.
    /// </summary>
    public Symbols.Marker Marker { get; set; } = Symbols.Marker.Dot;
    
    /// <summary>
    /// The data.
    /// </summary>
    public (double, double)[] Data { get; set; } = Array.Empty<(double, double)>();
}
