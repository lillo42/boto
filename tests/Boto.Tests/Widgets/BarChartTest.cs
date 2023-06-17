using Boto.Layouts;
using Boto.Styles;
using Boto.Symbols;
using Boto.Widgets;
using Boto.Widgets.Extensions;
using FluentAssertions;
using NSubstitute;
using Buffer = Boto.Buffers.Buffer;
using Block = Boto.Widgets.Block;

namespace Boto.Tests.Widgets;

public class BarChartTest
{
    [Fact]
    public void SetBlock()
    {
        var chart = new BarChart();
        var block = new Block()
            .SetTitle("test");

        chart
            .SetBlock(block)
            .Block.Should().Be(block);
    }

    [Fact]
    public void SetStyle()
    {
        var chart = new BarChart();
        var style = new Style { Foreground = Color.Blue };

        chart
            .SetStyle(style)
            .Style.Should().Be(style);
    }

    [Fact]
    public void SetBarSet()
    {
        var chart = new BarChart();
        var barSet = new Set
        {
            Full = "test",
            SevenEighths = "test",
            ThreeQuarters = "test",
            FiveEighths = "test",
            Half = "test",
            ThreeEighths = "test",
            OneQuarter = "test",
            OneEighth = "test",
            Empty = "test"
        };

        chart
            .SetBarSet(barSet)
            .BarSet.Should().Be(barSet);
    }

    [Fact]
    public void SetBarWidth()
    {
        var chart = new BarChart();
        chart
            .SetBarWidth(10)
            .BarWidth.Should().Be(10);
    }

    [Fact]
    public void SetBarGap()
    {
        var chart = new BarChart();
        chart
            .SetBarGap(10)
            .BarGap.Should().Be(10);
    }

    [Fact]
    public void SetBarStyle()
    {
        var chart = new BarChart();
        var style = new Style { Foreground = Color.Blue };

        chart
            .SetBarStyle(style)
            .BarStyle.Should().Be(style);
    }

    [Fact]
    public void SetMax()
    {
        var chart = new BarChart();
        chart
            .SetMax(10)
            .Max.Should().Be(10);
    }

    [Fact]
    public void SetLabelStyle()
    {
        var chart = new BarChart();
        var style = new Style { Foreground = Color.Blue };

        chart
            .SetLabelStyle(style)
            .LabelStyle.Should().Be(style);
    }

    [Fact]
    public void SetValueStyle()
    {
        var chart = new BarChart();
        var style = new Style { Foreground = Color.Blue };

        chart
            .SetValueStyle(style)
            .ValueStyle.Should().Be(style);
    }

    [Fact]
    public void AddItem()
    {
        var chart = new BarChart();
        var item = Substitute.For<IBarCharItem>();

        chart
            .AddItem(item)
            .Items.Should().Contain(item);
        
        chart = new BarChart();
        chart
            .AddItem("test", 10)
            .Items.Should().Contain(x => x.Label == "test" && x.Value == 10);
        
        chart = new BarChart();
        chart
            .AddItem("test", 11, "testvalue")
            .Items.Should().Contain(x => x.Label == "test" && x.Value == 11 && x.ValueLabel == "testvalue");
    }

    [Fact]
    public void AddItems()
    {
        var chart = new BarChart();
        var item1 = Substitute.For<IBarCharItem>();
        var item2 = Substitute.For<IBarCharItem>();

        chart = chart
            .AddItems(item1, item2);

        chart.Items.Should().HaveCount(2);
        chart.Items.Should().Contain(item1);
        chart.Items.Should().Contain(item2);

        chart = new BarChart();
        chart
            .AddItems(new List<IBarCharItem> { item1, item2 });

        chart.Items.Should().HaveCount(2);
        chart.Items.Should().Contain(item1);
        chart.Items.Should().Contain(item2);

        chart = new BarChart();
        chart.AddItems(new (string label, int value)[] { ("test", 10), ("test2", 11) });

        chart.Items.Should().HaveCount(2);
        chart.Items.Should().Contain(x => x.Label == "test" && x.Value == 10);
        chart.Items.Should().Contain(x => x.Label == "test2" && x.Value == 11);

        chart = new BarChart();
        chart = chart.AddItems(new (string label, int value, string valueLabel)[]
        {
            ("test", 12, "testValue"), ("test2", 13, "testValue")
        });

        chart.Items.Should().HaveCount(2);
        chart.Items.Should().Contain(x => x.Label == "test" && x.Value == 12 && x.ValueLabel == "testValue");
        chart.Items.Should().Contain(x => x.Label == "test2" && x.Value == 13 && x.ValueLabel == "testValue");

        chart = new BarChart();
        chart = chart.AddItems(new Dictionary<string, int> { { "test", 14 }, { "test2", 15 } },
            x => new BarChartItem(x.Key, x.Value));

        chart.Items.Should().HaveCount(2);
        chart.Items.Should().Contain(x => x.Label == "test" && x.Value == 14);
        chart.Items.Should().Contain(x => x.Label == "test2" && x.Value == 15);
    }

    [Fact]
    public void Render()
    {
        var buffer = new Buffer(new Rect(0, 0, 10, 10));
        var chart = new BarChart()
            .SetBlock(new Block()
                .SetTitle("test"))
            .SetMax(10)
            .SetBarGap(2)
            .SetLabelStyle(new Style { Background = Color.Green})
            .AddItem("t1", 10)
            .AddItem("t2", 11)
            .AddItem("t3", 12);
        
        chart.Render(new Rect(0, 0, 10, 10), buffer);
    }
}
