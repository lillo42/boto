using Boto.Buffers;
using Boto.Layouts;
using Boto.Styles;
using FluentAssertions;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Tests.Buffers;

public class BufferTest
{
    [Fact]
    public void ItTranslatesToAndFromCoordinates()
    {
        var rect = new Rect(200, 100, 50, 50);
        var buffer = new Buffer(rect);

        // First Cell is at upper left corner
        buffer.PosOf(0).Should().Be(new Coordinate(200, 100));
        buffer.IndexOf(200, 100).Should().Be(0);

        // Last Cell is at lower right corner
        buffer.PosOf(buffer.Content.Count - 1).Should().Be(new Coordinate(249, 149));
        buffer.IndexOf(249, 149).Should().Be(buffer.Content.Count - 1);
    }

    [Fact]
    public void SetString()
    {
        var area = new Rect(0, 0, 5, 1);
        var buffer = new Buffer(area);

        // Zero-width
        buffer.SetString(0, 0, "aaa", 0, new Style());
        buffer.Should().BeEquivalentTo(new Buffer(new List<string> { "     " }));

        buffer.SetString(0, 0, "aaa", new Style());
        buffer.Should().BeEquivalentTo(new Buffer(new List<string> { "aaa  " }));

        // Width limit:
        buffer.SetString(0, 0, "bbbbbbbbbbbbbb", 4, new Style());
        buffer.Should().BeEquivalentTo(new Buffer(new List<string> { "bbbb " }));

        buffer.SetString(0, 0, "12345", new Style());
        buffer.Should().BeEquivalentTo(new Buffer(new List<string> { "12345" }));

        buffer.SetString(0, 0, "123456", new Style());
        buffer.Should().BeEquivalentTo(new Buffer(new List<string> { "12345" }));
    }

    [Fact]
    public void SetStringZeroWidth()
    {
        var area = new Rect(0, 0, 1, 1);
        var buffer = new Buffer(area);

        // Leading grapheme with zero width
        var s = "\x1" + "a";
        buffer.SetString(0, 0, s, 1, new Style());
        buffer.Should().BeEquivalentTo(new Buffer(new List<string> { "a" }));

        s = "a\x1";
        buffer.SetString(0, 0, s, 1, new Style());
        buffer.Should().BeEquivalentTo(new Buffer(new List<string> { "a" }));
    }


    [Fact]
    public void SetStringDoubleWidth()
    {
        var area = new Rect(0, 0, 5, 1);
        var buffer = new Buffer(area);

        buffer.SetString(0, 0, "コン", new Style());
        buffer.Should().BeEquivalentTo(new Buffer(new List<string> { "コン " }));

        // Only 1 space left.
        buffer.SetString(0, 0, "コンピ", new Style());
        buffer.Should().BeEquivalentTo(new Buffer(new List<string> { "コン " }));
    }

    [Fact]
    public void WithLines()
    {
        var buffer = new Buffer(new List<string> { "┌────────┐", "│コンピュ│", "│ーa 上で│", "└────────┘" });
        buffer.Area.Should().Be(new Rect(0, 0, 10, 4));
    }

    [Fact]
    public void DiffingEmptyEmpty()
    {
        var area = new Rect(0, 0, 40, 40);
        var prev = new Buffer(area);
        var next = new Buffer(area);
        var diff = prev.Diff(next);
        diff.Should().BeEmpty();
    }

    [Fact]
    public void DiffingEmptyFilled()
    {
        var area = new Rect(0, 0, 40, 40);
        var prev = new Buffer(area);
        var next = new Buffer(area, new Cell { Symbol = "a" });
        var diff = prev.Diff(next);
        diff.Should().HaveCount(40 * 40);
    }

    [Fact]
    public void DiffingFilledFilled()
    {
        var area = new Rect(0, 0, 40, 40);
        var prev = new Buffer(area, new Cell { Symbol = "a" });
        var next = new Buffer(area, new Cell { Symbol = "a" });
        var diff = prev.Diff(next);
        diff.Should().BeEmpty();
    }

    [Fact]
    public void DiffingSingleWidth()
    {
        var prev = new Buffer(new List<string>
        {
            "          ",
            "┌Title─┐  ",
            "│      │  ",
            "│      │  ",
            "└──────┘  ",
        });

        var next = new Buffer(new List<string>
        {
            "          ",
            "┌TITLE─┐  ",
            "│      │  ",
            "│      │  ",
            "└──────┘  ",
        });

        var diff = prev.Diff(next);
        diff.Should().BeEquivalentTo(new List<BufferDiff>
        {
            new(1, 2, new Cell { Symbol = "I" }),
            new(1, 3, new Cell { Symbol = "T" }),
            new(1, 4, new Cell { Symbol = "L" }),
            new(1, 5, new Cell { Symbol = "E" }),
        });
    }

    [Fact]
    public void DiffingMultiWidth()
    {
        var prev = new Buffer(new List<string> { "┌Title─┐  ", "└──────┘  ", });

        var next = new Buffer(new List<string> { "┌称号──┐  ", "└──────┘  ", });

        var diff = prev.Diff(next);
        diff.Should().BeEquivalentTo(new List<BufferDiff>
        {
            new(0, 1, new Cell { Symbol = "称" }),
            // Skipped "i"
            new(0, 3, new Cell { Symbol = "号" }),
            // Skipped "l"
            new(0, 5, new Cell { Symbol = "─" }),
        });
    }

    [Fact]
    public void DiffingMultiWidthOffset()
    {
        var prev = new Buffer(new List<string> { "┌称号──┐" });
        var next = new Buffer(new List<string> { "┌─称号─┐" });

        var diff = prev.Diff(next);
        diff.Should().BeEquivalentTo(new List<BufferDiff>
        {
            new(0, 1, new Cell { Symbol = "─" }),
            new(0, 2, new Cell { Symbol = "称" }),
            new(0, 4, new Cell { Symbol = "号" }),
        });
    }

    [Fact]
    public void Cell()
    {
        var buffer = new Buffer(new List<string>
        {
            "          ",
            "┌TITLE─┐  ",
            "│      │  ",
            "│      │  ",
            "└──────┘  ",
        });

        buffer[0, 0].Symbol.Should().Be(" ");
        buffer[0, 1].Symbol.Should().Be("┌");
        buffer[1, 1].Symbol.Should().Be("T");
        buffer[1, 1] = buffer[1, 1] with { Symbol = "a" };
        buffer[1, 1].Symbol.Should().Be("a");
    }

    [Fact]
    public void Merge()
    {
        var one = new Buffer(new Rect(0, 0, 2, 2), new Cell { Symbol = "1" });
        var two = new Buffer(new Rect(0, 2, 2, 2), new Cell { Symbol = "2" });

        one.Merge(two);
        one.Should().BeEquivalentTo(new Buffer(new List<string> { "11", "11", "22", "22" }));
    }

    [Fact]
    public void Merge2()
    {
        var one = new Buffer(new Rect(2, 2, 2, 2), new Cell { Symbol = "1" });
        var two = new Buffer(new Rect(0, 0, 2, 2), new Cell { Symbol = "2" });

        one.Merge(two);
        one.Should().BeEquivalentTo(new Buffer(new List<string> { "22  ", "22  ", "  11", " 11" }));
    }

    [Fact]
    public void Merge3()
    {
        var one = new Buffer(new Rect(3, 3, 2, 2), new Cell { Symbol = "1" });
        var two = new Buffer(new Rect(1, 1, 3, 4), new Cell { Symbol = "2" });

        one.Merge(two);
        one.Should().BeEquivalentTo(new Buffer(new List<string> { "222 ", "222 ", "2221", "2221" }) with
        {
            Area = new Rect(1, 1, 4, 4)
        });
    }

    [Fact]
    public void SetStyle()
    {
        var buffer = new Buffer(new List<string>
        {
            "          ",
            "┌TITLE─┐  ",
            "│      │  ",
            "│      │  ",
            "└──────┘  ",
        });

        var style = new Style { Foreground = Color.Yellow, Background = Color.Reset, Underline = Color.Reset};
        buffer.SetStyle(buffer.Area, style);
        for (var y = buffer.Area.Top; y < buffer.Area.Bottom; y++)
        {
            for (var x = buffer.Area.Left; x < buffer.Area.Right; x++)
            {
                buffer[x, y].Style.Should().Be(style);
            }
        }
    }

    [Fact]
    public void ResizeLarge()
    {
        var buffer = new Buffer(new List<string>
        {
            "          ",
            "┌TITLE─┐  ",
            "│      │  ",
            "│      │  ",
            "└──────┘  ",
        });

        buffer.Resize(new Rect(0, 0, 100, 100));

        buffer.Area.Width.Should().Be(100);
        buffer.Area.Height.Should().Be(100);
        buffer.Area.Area.Should().Be(100 * 100);
        buffer.Content.Should().HaveCount(100 * 100);
    }

    [Fact]
    public void ResizeSmall()
    {
        var buffer = new Buffer(new List<string>
        {
            "          ",
            "┌TITLE─┐  ",
            "│      │  ",
            "│      │  ",
            "└──────┘  ",
        });

        buffer.Resize(new Rect(0, 0, 2, 2));

        buffer.Area.Width.Should().Be(2);
        buffer.Area.Height.Should().Be(2);
        buffer.Area.Area.Should().Be(2 * 2);
        buffer.Content.Should().HaveCount(2 * 2);
    }

    [Fact]
    public void Reset()
    {
        var buffer = new Buffer(new List<string>
        {
            "          ",
            "┌TITLE─┐  ",
            "│      │  ",
            "│      │  ",
            "└──────┘  ",
        });

        buffer.Reset();

        for (var y = buffer.Area.Top; y < buffer.Area.Bottom; y++)
        {
            for (var x = buffer.Area.Left; x < buffer.Area.Right; x++)
            {
                buffer[x, y].Should().Be(new Cell());
            }
        }
    }
}
