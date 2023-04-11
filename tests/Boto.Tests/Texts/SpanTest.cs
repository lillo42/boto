using Boto.Styles;
using Boto.Texts;
using FluentAssertions;

namespace Boto.Tests.Texts;

public class SpanTest
{
    [Fact]
    public void StyledGraphemes()
    {
        var span = new Span("Text", new Style { Foreground = Color.Yellow });
        
        var styledGraphemes = span.StyledGraphemes(new Style
        {
            Foreground = Color.Green, 
            Background = Color.Black
        });
        
        styledGraphemes.Should().BeEquivalentTo(new[]
        {
            new StyledGrapheme("T", new Style { Foreground = Color.Yellow, Background = Color.Black }),
            new StyledGrapheme("e", new Style { Foreground = Color.Yellow, Background = Color.Black }),
            new StyledGrapheme("x", new Style { Foreground = Color.Yellow, Background = Color.Black }),
            new StyledGrapheme("t", new Style { Foreground = Color.Yellow, Background = Color.Black }),
        });
    }
}
