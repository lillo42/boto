using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;

namespace Boto.Widgets;

/// <summary>
/// The axis of <see cref="Dataset"/>.
/// </summary>
public class Axis
{
    /// <summary>
    /// THe title.
    /// </summary>
    public Spans? Title { get; set; }
    
    /// <summary>
    /// The data bounds.
    /// </summary>
    public double[] Bounds { get; set; } = { 0, 0 };
    
    /// <summary>
    /// The labels.
    /// </summary>
    public List<Span>? Labels { get; set; }
    
    /// <summary>
    /// The <see cref="Styles.Style"/>.
    /// </summary>
    public Style Style { get; set; }
    
    /// <summary>
    /// The labels <see cref="Alignment"/>.
    /// </summary>
    public Alignment LabelsAlignment { get; set; }
}
