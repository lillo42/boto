using System.Text;
using Boto.Styles;

namespace Boto.Widget.Canvas;

internal record BrailleGrid(int Width, int Height, List<int> Cells, List<Color> Colors) : IGrid
{
    public BrailleGrid(int width, int height) 
        : this(width, height, 
            Enumerable.Repeat((int)Symbols.Braille.Blank,  width * height).ToList(), 
            Enumerable.Repeat(Color.Reset,  width * height).ToList()) { }

    public (float, float) Resolution => (Width * 2 - 1, Height * 4 - 1);
    
    public void Paint(int x, int y, Color color)
    {
        var index = y / 4 * Width + x / 2;
        if (index < Cells.Count)
        {
            Cells[index] |= Symbols.Braille.Dots[y % 4][x % 2];
        }

        if (index < Colors.Count)
        {
            Colors[index] = color;
        }
    }

    public Layer Save()
    {
        var s = Encoding.Unicode.GetString(Cells.SelectMany(x => BitConverter.GetBytes((ushort)x)).ToArray());
        return new Layer(s, Colors.ToList());
    }

    public void Reset()
    {
        for (var index = 0; index < Cells.Count; index++)
        {
            Cells[index] = Symbols.Braille.Blank;
        }
        
        for (var index = 0; index < Colors.Count; index++)
        {
            Colors[index] = Color.Reset;
        }
    }
}
