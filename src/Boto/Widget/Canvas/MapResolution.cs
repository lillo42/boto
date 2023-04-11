namespace Boto.Widget.Canvas;

public enum MapResolution
{
    Low,
    High
}

public static class MapResolutionExtensions
{
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
