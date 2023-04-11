namespace Boto.Layouts;

/// <summary>
/// A simple rectangle used in the computation of the layout and to give widgets a hint about the
/// area they are supposed to render to.
/// </summary>
public readonly record struct Rect
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Rect"/> class.
    /// </summary>
    public Rect()
    {
        X = 0;
        Y = 0;
        Width = 0;
        Height = 0;
    }

    /// <summary>
    /// A simple rectangle used in the computation of the layout and to give widgets a hint about the
    /// area they are supposed to render to.
    /// </summary>
    /// <param name="x">The X.</param>
    /// <param name="y">The Y.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public Rect(int x, int y, int width, int height)
        : this()
    {
        const ushort Max = ushort.MaxValue;
        const double MaxArea = Max;
        if (width * height > Max)
        {
            var aspectRatio = (double)width / height;
            var heightF = Math.Sqrt(MaxArea / aspectRatio);
            var widthF = heightF * aspectRatio;
            X = x;
            Y = y;
            Width = (int)widthF;
            Height = (int)heightF;
        }
        else
        {
            
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }

    /// <summary>
    /// The area of the rectangle.
    /// </summary>
    public int Area => Width * Height;

    /// <summary>
    /// The left value.
    /// </summary>
    /// <remarks>Same value as <see cref="X"/>.</remarks>
    public int Left => X;

    /// <summary>
    /// The Top value.
    /// </summary>
    /// <remarks>Same value as <see cref="Y"/>.</remarks>
    public int Top => Y;

    /// <summary>
    /// The right value.
    /// </summary>
    public int Right => X + Width;

    /// <summary>
    /// The bottom value.
    /// </summary>
    public int Bottom => Y + Height;

    /// <summary>The X.</summary>
    public int X { get; init; }

    /// <summary>The Y.</summary>
    public int Y { get; init; }

    /// <summary>The width.</summary>
    public int Width { get; init; }

    /// <summary>The height.</summary>
    public int Height { get; init; }

    /// <summary>
    /// Inner rectangle.
    /// </summary>
    /// <param name="margin">The <see cref="Margin"/>.</param>
    /// <returns>New instance of <see cref="Margin"/>.</returns>
    public Rect Inner(Margin margin)
    {
        if (Width < 2 * margin.Horizontal || Height < 2 * margin.Vertical)
        {
            return new Rect(0, 0, 0, 0);
        }

        return new Rect(X + margin.Horizontal,
            Y + margin.Vertical,
            Width - 2 * margin.Horizontal,
            Height - 2 * margin.Vertical);
    }

    /// <summary>
    /// The union of two rectangles.
    /// </summary>
    /// <returns></returns>
    public Rect Union(Rect other)
    {
        var x1 = Math.Min(X, other.X);
        var y1 = Math.Min(Y, other.Y);
        var x2 = Math.Max(Right, other.Right);
        var y2 = Math.Max(Bottom, other.Bottom);
        return new Rect(x1, y1, x2 - x1, y2 - y1);
    }

    /// <summary>
    /// The intersection of two rectangles.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public Rect Intersect(Rect other)
    {
        var x1 = Math.Max(X, other.X);
        var y1 = Math.Max(Y, other.Y);
        var x2 = Math.Min(Right, other.Right);
        var y2 = Math.Min(Bottom, other.Bottom);
        return new Rect(x1, y1, x2 - x1, y2 - y1);
    }

    /// <summary>
    /// The intersection of two rectangles.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Intersects(Rect other) =>
        X < other.X + other.Width &&
        X + Width > other.X &&
        Y < other.Y + other.Height &&
        Y + Height > other.Y;
}
