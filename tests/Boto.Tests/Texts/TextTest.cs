using Boto.Styles;
using Boto.Texts;
using FluentAssertions;

namespace Boto.Tests.Texts;

public class TextTest
{
    [Fact]
    public void Width_Should_Return15()
    {
        var text = new Text("The first line\nThe second line");
        text.Width.Should().Be(15);
    }

    [Fact]
    public void Height_Should_Return2()
    {
        var text = new Text("The first line\nThe second line");
        text.Height.Should().Be(2);
    }

    [Fact]
    public void PatchStyle_Should_MergeStyle()
    {
        var style = new Style { Foreground = Color.Yellow, AddModifier = Modifier.Italic };

        var rawText = new Text("The first line\nThe second line");
        var styledText = new Text("The first line\nThe second line", style);

        rawText.Should().NotBeEquivalentTo(styledText);

        rawText.PatchStyle(style);
        rawText.Should().BeEquivalentTo(styledText);
    }
}
