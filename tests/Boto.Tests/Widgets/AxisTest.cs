using AutoFixture;
using Boto.Layouts;
using Boto.Styles;
using Boto.Texts;
using Boto.Widgets;
using Boto.Widgets.Extensions;
using FluentAssertions;

namespace Boto.Tests.Widgets;

public class AxisTest
{
    [Fact]
    public void SetTitle()
    {
        var fixture = new Fixture();
        
        var title = fixture.Create<string>();
        var axis = new Axis();
        axis.SetTitle(title)
            .Title.Should().BeEquivalentTo(new Spans(title));
        
        axis = new Axis();
        title = fixture.Create<string>();
        var style = new Style{ Background = Color.Cyan };
        axis.SetTitle(title, style)
            .Title.Should().BeEquivalentTo(new Spans(title, style));

        axis = new Axis();
        var spans = new Spans(fixture.Create<string>());
        
        axis.SetTitle(spans)
            .Title.Should().Be(spans);
    }
    
    [Fact]
    public void SetBounds()
    {
        var fixture = new Fixture();
        
        var min = fixture.Create<double>();
        var max = fixture.Create<double>();
        
        var axis = new Axis();
        axis.SetBounds(min, max)
            .Bounds.Should().BeEquivalentTo(new[] { min, max });
        
        axis = new Axis();
        var bounds = fixture.Create<double[]>();
        axis.SetBounds(bounds)
            .Bounds.Should().BeEquivalentTo(bounds);
    }

    [Fact]
    public void SetLabelAlignment()
    {
        var axis = new Axis();
        axis.SetLabelsAlignment(Alignment.Center)
            .LabelsAlignment.Should().Be(Alignment.Center);
        
        axis = new Axis();
        axis.SetLabelsAlignment(Alignment.Right)
            .LabelsAlignment.Should().Be(Alignment.Right);
        
        axis = new Axis();
        axis.SetLabelsAlignment(Alignment.Left)
            .LabelsAlignment.Should().Be(Alignment.Left);
    }
}
