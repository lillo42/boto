using Boto.Styles;

namespace Boto.Widget.Canvas;

public interface IGrid
{
    int Width { get; }
    int Height { get; }
    (float, float) Resolution { get; }

    void Paint(int x, int y, Color color);
    
    Layer Save();
    void Reset();
}
