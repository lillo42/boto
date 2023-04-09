namespace Boto.Widget;

[Flags]
public enum Borders
{
    None = 0b0000_0001,
    Top = 0b0000_0010,
    Right = 0b0000_0100,
    Bottom = 0b0000_1000,
    Left = 0b0001_0000,
    All = Top | Right | Bottom | Left
}
