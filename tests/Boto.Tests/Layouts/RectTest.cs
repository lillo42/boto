using AutoFixture;
using Boto.Layouts;
using FluentAssertions;

namespace Boto.Tests.Layouts;

public class RectTest
{
    private readonly Fixture _fixture = new();
    
    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(15, 27)]
    public void Area(int width, int height)
    {
        var rect = new Rect(0, 0, width, height);
        rect.Area.Should().Be(width * height);
    }

    [Fact]
    public void Directions()
    {
        var rect = _fixture.Create<Rect>();
        rect.Left.Should().Be(rect.X);
        rect.Right.Should().Be(rect.X + rect.Width);
        rect.Top.Should().Be(rect.Y);
        rect.Bottom.Should().Be(rect.Y + rect.Height);
    }
}
