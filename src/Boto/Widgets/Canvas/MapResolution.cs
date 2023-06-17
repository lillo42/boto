namespace Boto.Widgets.Canvas;

/// <summary>
/// The map resolution.
/// </summary>
public enum MapResolution
{
    /// <summary>
    /// Low resolution.
    /// </summary>
    Low,
    
    /// <summary>
    /// High resolution.
    /// </summary>
    High
}

/// <summary>
/// The <see cref="MapResolution"/> extensions method.
/// </summary>
public static class MapResolutionExtensions
{
    /// <summary>
    /// The map data.
    /// </summary>
    /// <param name="resolution">The <see cref="MapResolution"/>.</param>
    /// <returns>The map data.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static (double, double)[] Data(this MapResolution resolution)
    {
        return resolution switch
        {
            MapResolution.Low => World.WorldLowResolution,
            MapResolution.High => World.WorldHighResolution,
            _ => throw new ArgumentOutOfRangeException(nameof(resolution), resolution, null)
        };
    }
}
