using Boto.Symbols;

namespace Boto.Widget;

public enum BorderType
{
    Plain,
    Rounded,
    Double,
    Thick,
}

public static class BorderTypeExtensions
{
    public static Line.Set LineSymbol(this BorderType type)
        => type switch
        {
            BorderType.Plain => Line.Normal,
            BorderType.Rounded => Line.Rounded,
            BorderType.Double => Line.Double,
            BorderType.Thick => Line.Thick,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
}
