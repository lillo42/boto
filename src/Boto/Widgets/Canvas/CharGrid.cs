using System.Text;
using Boto.Styles;

namespace Boto.Widgets.Canvas;

internal record CharGrid(int Width, int Height, List<string> Cells, List<Color> Colors, string CellChar) : IGrid
{
    public CharGrid(int width, int height, string cellChar)
        : this(width, height,
            Enumerable.Repeat(" ", width * height).ToList(),
            Enumerable.Repeat(Color.Reset, width * height).ToList(),
            cellChar)
    {
    }


    public (double, double) Resolution => (Width - 1, Height - 1);

    public void Paint(int x, int y, Color color)
    {
        var index = y * Width + x;
        if (index < Cells.Count)
        {
            Cells[index] = CellChar;
        }
        
        if (index < Colors.Count)
        {
            Colors[index] = color;
        }
    }

    public Layer Save()
    {
        var sb = new StringBuilder();

        foreach (var cell in Cells)
        {
            sb.Append(cell);
        }
        
        return new Layer(sb.ToString(), Colors.ToList());
    }

    public void Reset()
    {
        for (var i = 0; i < Cells.Count; i++)
        {
            Cells[i] = " ";
        }
        
        for (var i = 0; i < Colors.Count; i++)
        {
            Colors[i] = Color.Reset;
        }
    }
}
