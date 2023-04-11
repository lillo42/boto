using System.Collections.Immutable;

namespace Boto.Symbols;

/// <summary>
/// 
/// </summary>
public static class Braille
{
    /// <summary>
    /// 
    /// </summary>
    public const ushort Blank = 0x2800;

    /// <summary>
    /// 
    /// </summary>
    public static readonly ImmutableArray<ImmutableArray<ushort>> Dots =
        ImmutableArray<ImmutableArray<ushort>>
            .Empty
            .AddRange(ImmutableArray<ushort>.Empty.AddRange(0x0001, 0x0008))
            .AddRange(ImmutableArray<ushort>.Empty.AddRange(0x0002, 0x0010))
            .AddRange(ImmutableArray<ushort>.Empty.AddRange(0x0004, 0x0020))
            .AddRange(ImmutableArray<ushort>.Empty.AddRange(0x0040, 0x0080));
}
