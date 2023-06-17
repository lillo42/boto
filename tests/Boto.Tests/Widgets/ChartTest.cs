using Boto.Widgets;
using Boto.Widgets.Extensions;
using FluentAssertions;

namespace Boto.Tests.Widgets;

public class ChartTest
{
    [Fact]
    public void SetBlock()
    {
        var chart = new Chart();
        var block = new Block()
            .SetTitle("test");

        chart.SetBlock(block)
            .Block.Should().Be(block);
    }
    
    [Fact]
    public void SetXAxis()
    {
        var chart = new Chart();
        var axis = new Axis()
            .SetTitle("test");

        chart.SetXAxis(axis)
            .XAxis.Should().Be(axis);
    }
}
