using BotoColor = Boto.Styles.Color;
using TutuColor = Tutu.Style.Types.Color;

namespace Boto.Tutu.Extensions;

internal static class ColorExtensions
{
    public static TutuColor MapToTutuColor(this BotoColor color)
    {
        if (color == BotoColor.Reset)
        {
            return TutuColor.Reset;
        }

        if (color == BotoColor.Black)
        {
            return TutuColor.Black;
        }

        if (color == BotoColor.Red)
        {
            return TutuColor.DarkRed;
        }

        if (color == BotoColor.Green)
        {
            return TutuColor.DarkGreen;
        }

        if (color == BotoColor.Yellow)
        {
            return TutuColor.DarkYellow;
        }

        if (color == BotoColor.Blue)
        {
            return TutuColor.DarkBlue;
        }

        if (color == BotoColor.Magenta)
        {
            return TutuColor.DarkMagenta;
        }
        
        if (color == BotoColor.Cyan)
        {
            return TutuColor.DarkCyan;
        }
        
        if (color == BotoColor.Gray)
        {
            return TutuColor.Grey;
        }
        
        if (color == BotoColor.DarkGray)
        {
            return TutuColor.DarkGrey;
        }
        
        if (color == BotoColor.LightRed)
        {
            return TutuColor.Red;
        }
        
        if (color == BotoColor.LightGreen)
        {
            return TutuColor.Green;
        }
        
        if (color == BotoColor.LightBlue)
        {
            return TutuColor.Blue;
        }
        
        if (color == BotoColor.LightYellow)
        {
            return TutuColor.Yellow;
        }
        
        if (color == BotoColor.LightMagenta)
        {
            return TutuColor.Magenta;
        }
        
        if (color == BotoColor.LightCyan)
        {
            return TutuColor.Cyan;
        }
        
        if (color == BotoColor.White)
        {
            return TutuColor.White;
        }

        if (color.Values.Length == 1)
        {
            return TutuColor.AnsiValue(color.Values[0]);
        }
        
        if (color.Values.Length == 3)
        {
            return TutuColor.Rgb(color.Values[0], color.Values[1], color.Values[2]);
        }

        throw new ArgumentOutOfRangeException(nameof(color), color, "Unknown color");
    }
}
