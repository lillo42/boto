using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;
using Boto.Widgets;
using Boto.Widgets.Extensions;
using FluentAssertions;
using Buffer = Boto.Buffers.Buffer;
using Cell = Boto.Buffers.Cell;

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
        block = new Block { Borders = Boto.Widgets.Borders.Left };
        block.Inner(new Rect(0, 0, 0, 1))
            .Should().Be(new Rect(0, 0, 0, 1));

        block.Inner(new Rect(0, 0, 1, 1))
            .Should().Be(new Rect(1, 0, 0, 1));

        block.Inner(new Rect(0, 0, 2, 1))
            .Should().Be(new Rect(1, 0, 1, 1));

        // Top border
        block = new Block { Borders = Boto.Widgets.Borders.Top };
        block.Inner(new Rect(0, 0, 1, 0))
            .Should().Be(new Rect(0, 0, 1, 0));

        block.Inner(new Rect(0, 0, 1, 1))
            .Should().Be(new Rect(0, 1, 1, 0));

        block.Inner(new Rect(0, 0, 1, 2))
            .Should().Be(new Rect(0, 1, 1, 1));

        // Right border
        block = new Block { Borders = Boto.Widgets.Borders.Right };
        block.Inner(new Rect(0, 0, 0, 1))
            .Should().Be(new Rect(0, 0, 0, 1));

        block.Inner(new Rect(0, 0, 1, 1))
            .Should().Be(new Rect(0, 0, 0, 1));

        block.Inner(new Rect(0, 0, 2, 1))
            .Should().Be(new Rect(0, 0, 1, 1));

        // Bottom border
        block = new Block { Borders = Boto.Widgets.Borders.Bottom };
        block.Inner(new Rect(0, 0, 1, 0))
            .Should().Be(new Rect(0, 0, 1, 0));

        block.Inner(new Rect(0, 0, 1, 1))
            .Should().Be(new Rect(0, 0, 1, 0));

        block.Inner(new Rect(0, 0, 1, 2))
            .Should().Be(new Rect(0, 0, 1, 1));

        // All borders
        block = new Block { Borders = Boto.Widgets.Borders.All };
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
    public void Title()
    {
        var block = new Block();
        block.SetTitle("Test")
            .Title.Should().BeEquivalentTo((Spans)"Test");

        block.SetTitle(new Spans("Test2"))
            .Title.Should().BeEquivalentTo((Spans)"Test2");

        var spans = new List<Span> { "Test3", new("Test4", new Style { Foreground = Color.Yellow }) };
        block.SetTitle(spans)
            .Title.Should().BeEquivalentTo(new Spans(spans));

        block.SetTitle("Test5", new Span("Test6", new Style { Foreground = Color.Yellow }))
            .Title.Should().BeEquivalentTo(new Spans(new List<Span>
            {
                "Test5", new("Test6", new Style { Foreground = Color.Yellow })
            }));
    }

    [Fact]
    public void TitleAlignment()
    {
        var block = new Block();
        block.SetTitleAlignment(Alignment.Left)
            .TitleAlignment.Should().Be(Alignment.Left);

        block.SetTitleAlignment(Alignment.Center)
            .TitleAlignment.Should().Be(Alignment.Center);

        block.SetTitleAlignment(Alignment.Right)
            .TitleAlignment.Should().Be(Alignment.Right);
    }

    [Fact]
    public void Borders()
    {
        var block = new Block();
        block.SetBorders(Boto.Widgets.Borders.Left)
            .Borders.Should().Be(Boto.Widgets.Borders.Left);
    }

    [Fact]
    public void BorderStyle()
    {
        var block = new Block();
        block.SetBorderStyle(new Style { Foreground = Color.Red })
            .BorderStyle.Should().BeEquivalentTo(new Style { Foreground = Color.Red });
    }

    [Fact]
    public void BorderType()
    {
        var block = new Block();
        block.SetBorderType(Boto.Widgets.BorderType.Double)
            .BorderType.Should().Be(Boto.Widgets.BorderType.Double);
    }

    [Fact]
    public void Style()
    {
        var block = new Block();
        block.SetStyle(new Style { Foreground = Color.Red })
            .Style.Should().BeEquivalentTo(new Style { Foreground = Color.Red });
    }

    [Fact]
    public void Render_Should_DoNothing_When_AreaIsZero()
    {
        var block = new Block();
        block.Render(new Rect(), new Buffer(new Rect()));
    }
    
    [Theory]
    [InlineData(Alignment.Center, 3, 0)]
    [InlineData(Alignment.Left, 1, 0)]
    [InlineData(Alignment.Right, 5, 0)]
    public void Render(Alignment alignment, int x, int y)
    {
        var block = new Block
        {
            Title = "Test",
            TitleAlignment = alignment,
            Borders = Boto.Widgets.Borders.All,
            BorderStyle = new Style { Foreground = Color.White }
        };
        var buffer = new Buffer(new Rect(0, 0, 10, 10));
        block.Render(new Rect(0, 0, 10, 10), buffer);
        buffer[x, y].Should().BeEquivalentTo(new Cell { Symbol = "T", Foreground = Color.White });
        buffer[x + 1, y].Should().BeEquivalentTo(new Cell { Symbol = "e", Foreground = Color.White });
        buffer[x + 2, y].Should().BeEquivalentTo(new Cell { Symbol = "s", Foreground = Color.White });
        buffer[x + 3, y].Should().BeEquivalentTo(new Cell { Symbol = "t", Foreground = Color.White });
    }
}
