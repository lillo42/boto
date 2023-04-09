using Boto.Styles;

namespace Boto.Tests;

public class StylesTest
{
    public static IEnumerable<object[]> Styles => new List<object[]>
    {
        new object[] { new Style() },
        new object[] { new Style { Foreground = Color.Yellow} },
        new object[] { new Style { Background = Color.Yellow} },
        new object[] { new Style { AddModifier = Modifier.Bold } },
        new object[] { new Style { RemoveModifier = Modifier.Bold } },
        new object[] { new Style { AddModifier = Modifier.Italic } },
        new object[] { new Style { RemoveModifier = Modifier.Italic } },
        new object[] { new Style { AddModifier = Modifier.Italic | Modifier.Bold } },
        new object[] { new Style { RemoveModifier = Modifier.Italic | Modifier.Bold } },
    };
    
}
