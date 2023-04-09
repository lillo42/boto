using AutoFixture;
using Boto.Layouts;
using Boto.Terminals;
using FluentAssertions;

namespace Boto.Tests.Terminals;

public class ViewportTest
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Fixed()
    {
        var area = _fixture.Create<Rect>();
        var viewport = Viewport.Fixed(area);
        viewport.ResizeBehavior.Should().Be(ResizeBehavior.Fixed);
        viewport.Area.Should().Be(area);
    }
}
