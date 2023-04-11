using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;
using Boto.Widget;
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
        block = new Block { Borders = Widget.Borders.Left };
        block.Inner(new Rect(0, 0, 0, 1))
            .Should().Be(new Rect(0, 0, 0, 1));

        block.Inner(new Rect(0, 0, 1, 1))
            .Should().Be(new Rect(1, 0, 0, 1));

        block.Inner(new Rect(0, 0, 2, 1))
            .Should().Be(new Rect(1, 0, 1, 1));

        // Top border
        block = new Block { Borders = Widget.Borders.Top };
        block.Inner(new Rect(0, 0, 1, 0))
            .Should().Be(new Rect(0, 0, 1, 0));

        block.Inner(new Rect(0, 0, 1, 1))
            .Should().Be(new Rect(0, 1, 1, 0));

        block.Inner(new Rect(0, 0, 1, 2))
            .Should().Be(new Rect(0, 1, 1, 1));

        // Right border
        block = new Block { Borders = Widget.Borders.Right };
        block.Inner(new Rect(0, 0, 0, 1))
            .Should().Be(new Rect(0, 0, 0, 1));

        block.Inner(new Rect(0, 0, 1, 1))
            .Should().Be(new Rect(0, 0, 0, 1));

        block.Inner(new Rect(0, 0, 2, 1))
            .Should().Be(new Rect(0, 0, 1, 1));

        // Bottom border
        block = new Block { Borders = Widget.Borders.Bottom };
        block.Inner(new Rect(0, 0, 1, 0))
            .Should().Be(new Rect(0, 0, 1, 0));

        block.Inner(new Rect(0, 0, 1, 1))
            .Should().Be(new Rect(0, 0, 1, 0));

        block.Inner(new Rect(0, 0, 1, 2))
            .Should().Be(new Rect(0, 0, 1, 1));

        // All borders
        block = new Block { Borders = Widget.Borders.All };
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
        block.Title("Test")
            .Title.Should().BeEquivalentTo((Spans)"Test");

        block.Title(new Spans("Test2"))
            .Title.Should().BeEquivalentTo((Spans)"Test2");

        var spans = new List<Span> { "Test3", new("Test4", new Style { Foreground = Color.Yellow }) };
        block.Title(spans)
            .Title.Should().BeEquivalentTo(new Spans(spans));

        block.Title("Test5", new Span("Test6", new Style { Foreground = Color.Yellow }))
            .Title.Should().BeEquivalentTo(new Spans(new List<Span>
            {
                "Test5", new("Test6", new Style { Foreground = Color.Yellow })
            }));
    }

    [Fact]
    public void TitleAlignment()
    {
        var block = new Block();
        block.TitleAlignment(Alignment.Left)
            .TitleAlignment.Should().Be(Alignment.Left);

        block.TitleAlignment(Alignment.Center)
            .TitleAlignment.Should().Be(Alignment.Center);

        block.TitleAlignment(Alignment.Right)
            .TitleAlignment.Should().Be(Alignment.Right);

        block.LeftTitleAlignment()
            .TitleAlignment.Should().Be(Alignment.Left);

        block.CenterTitleAlignment()
            .TitleAlignment.Should().Be(Alignment.Center);

        block.RightTitleAlignment()
            .TitleAlignment.Should().Be(Alignment.Right);
    }

    [Fact]
    public void Borders()
    {
        var block = new Block();
        block.Borders(Widget.Borders.Left)
            .Borders.Should().Be(Widget.Borders.Left);

        block.NoneBorders()
            .Borders.Should().Be(Widget.Borders.None);

        block.TopBorders()
            .Borders.Should().Be(Widget.Borders.Top);

        block.BottomBorders()
            .Borders.Should().Be(Widget.Borders.Bottom);

        block.LeftBorders()
            .Borders.Should().Be(Widget.Borders.Left);

        block.RightBorders()
            .Borders.Should().Be(Widget.Borders.Right);

        block.AllBorders()
            .Borders.Should().Be(Widget.Borders.All);
    }

    [Fact]
    public void BorderStyle()
    {
        var block = new Block();
        block.BorderStyle(new Style { Foreground = Color.Red })
            .BorderStyle.Should().BeEquivalentTo(new Style { Foreground = Color.Red });
    }

    [Fact]
    public void BorderType()
    {
        var block = new Block();
        block.BorderType(Widget.BorderType.Double)
            .BorderType.Should().Be(Widget.BorderType.Double);

        block.RoundedBorderType()
            .BorderType.Should().Be(Widget.BorderType.Rounded);

        block.RoundedBorderType()
            .BorderType.Should().Be(Widget.BorderType.Rounded);

        block.PlainBorderType()
            .BorderType.Should().Be(Widget.BorderType.Plain);

        block.ThickBorderType()
            .BorderType.Should().Be(Widget.BorderType.Thick);

        block.DoubleBorderType()
            .BorderType.Should().Be(Widget.BorderType.Double);
    }

    [Fact]
    public void Style()
    {
        var block = new Block();
        block.Style(new Style { Foreground = Color.Red })
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
            Borders = Widget.Borders.All,
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
