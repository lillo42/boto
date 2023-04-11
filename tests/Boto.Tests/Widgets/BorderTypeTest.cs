using Boto.Symbols;
using Boto.Widget;
using FluentAssertions;

namespace Boto.Tests.Widgets;

public class BorderTypeTest
{
    [Theory]
    [MemberData(nameof(LineSymbolData))]
    public void LineSymbol(BorderType type, Line.Set set)
    {
        type.LineSymbol().Should().Be(set);
    }

    public static IEnumerable<object[]> LineSymbolData
    {
        get
        {
            yield return new object[] { BorderType.Plain, Line.Normal };
            yield return new object[] { BorderType.Rounded, Line.Rounded };
            yield return new object[] { BorderType.Double, Line.Double };
            yield return new object[] { BorderType.Thick, Line.Thick };
        }
    }
}
