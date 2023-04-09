using Boto.Layouts;
using Boto.Widget;
using FluentAssertions;

using Buffer = Boto.Buffers.Buffer;

namespace Boto.Tests.Widgets;

public class BlockTest
{
    [Fact]
    public void TakeIntoAccountTheBorders()
    {
        // No borders
        var block = new Block();
        block.Inner(new Rect())
            .Should().Be(new Rect(0, 0, 0, 0));

        block.Inner(new Rect(0, 0, 1, 1))
            .Should().Be(new Rect(0, 0, 1, 1));
        
        // Left border
        block = new Block { Borders = Borders.Left };
        block.Inner(new Rect(0, 0, 0, 1))
            .Should().Be(new Rect(0, 0, 0, 1));

        block.Inner(new Rect(0, 0, 1, 1))
            .Should().Be(new Rect(1, 0, 0, 1));
        
        block.Inner(new Rect(0, 0, 2, 1))
            .Should().Be(new Rect(1, 0, 1, 1));
        
        // Top border
        block = new Block { Borders = Borders.Top };
        block.Inner(new Rect(0, 0, 1, 0))
            .Should().Be(new Rect(0, 0, 1, 0));

        block.Inner(new Rect(0, 0, 1, 1))
            .Should().Be(new Rect(0, 1, 1, 0));

        block.Inner(new Rect(0, 0, 1, 2))
            .Should().Be(new Rect(0, 1, 1, 1));
        
        // Right border
        block = new Block { Borders = Borders.Right };
        block.Inner(new Rect(0, 0, 0, 1))
            .Should().Be(new Rect(0, 0, 0, 1));

        block.Inner(new Rect(0, 0, 1, 1))
            .Should().Be(new Rect(0, 0, 0, 1));
        
        block.Inner(new Rect(0, 0, 2, 1))
            .Should().Be(new Rect(0, 0, 1, 1));
        
        // Bottom border
        block = new Block { Borders = Borders.Bottom };
        block.Inner(new Rect(0, 0, 1, 0))
            .Should().Be(new Rect(0, 0, 1, 0));
        
        block.Inner(new Rect(0, 0, 1, 1))
            .Should().Be(new Rect(0, 0, 1, 0));

        block.Inner(new Rect(0, 0, 1, 2))
            .Should().Be(new Rect(0, 0, 1, 1));
        
        // All borders
        block = new Block { Borders = Borders.All };
        block.Inner(new Rect())
            .Should().Be(new Rect(0, 0, 0, 0));

        block.Inner(new Rect(0, 0, 1, 1))
            .Should().Be(new Rect(1, 1, 0, 0));

        block.Inner(new Rect(0, 0, 2, 2))
            .Should().Be(new Rect(1, 1, 0, 0));

        block.Inner(new Rect(0, 0, 3, 3))
            .Should().Be(new Rect(1, 1, 1, 1));
    }

    [Fact]
    public void TakesIntoAccountTheTitle()
    {
        var block = new Block { Title = "Test" };
        block.Inner(new Rect(0, 0, 0, 1))
            .Should().Be(new Rect(0, 1, 0, 0));
        
        
        block = new Block { Title = "Test", TitleAlignment = Alignment.Center };
        block.Inner(new Rect(0, 0, 0, 1))
            .Should().Be(new Rect(0, 1, 0, 0));

        block = new Block { Title = "Test", TitleAlignment = Alignment.Right };
        block.Inner(new Rect(0, 0, 0, 1))
            .Should().Be(new Rect(0, 1, 0, 0));
    }
    
    [Fact]
    public void Render_Should_DoNothing_When_AreaIsZero()
    {
        var block = new Block();
        block.Render(new Rect(), new Buffer(new Rect()));
        
    }
}
