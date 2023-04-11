using System.Runtime.CompilerServices;

namespace Boto.Extensions;

internal static class NumericExtensions
{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int SaturatingSub(this int a, int b) => Math.Max(a - b, 0);
}
