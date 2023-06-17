using Boto.Layouts;

namespace Boto.Widgets;

/// <summary>
/// The <see cref="Chart"/> layout.
/// </summary>
public class ChartLayout
{
    /// <summary>
    /// The title X.
    /// </summary>
    public Position? TitleX { get; set;}
    
    /// <summary>
    /// The title Y.
    /// </summary>
    public Position? TitleY { get; set;}
    
    /// <summary>
    /// The label X.
    /// </summary>
    public int? LabelX { get; set;}
    
    /// <summary>
    /// The label Y.
    /// </summary>
    public int? LabelY { get; set;}
    
    /// <summary>
    /// The axis X.
    /// </summary>
    public int? AxisX { get; set;}
    
    /// <summary>
    /// The axis Y.
    /// </summary>
    public int? AxisY { get; set;}
    
    /// <summary>
    /// The legend area.
    /// </summary>
    public Rect? LegendArea { get; set;}
    
    /// <summary>
    /// The graph area.
    /// </summary>
    public Rect GraphArea { get; set;}
}
