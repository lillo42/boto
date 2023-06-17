using Boto.Extensions;
using Boto.Layouts;
using FluentAssertions;
using static Boto.Layouts.Constraints;

namespace Boto.Tests.Layouts;

public class LayoutTest
{
    [Fact]
    public void VerticalSplitByHeight()
    {
        var target = new Rect(2, 2, 10, 10);

        var layout = new Layout
        {
            Direction = Boto.Layouts.Direction.Vertical, Constraints = new() { Percentage(10), Max(5), Min(1) }
        };

        var chunks = layout.Split(target);

        chunks.Sum(x => x.Height).Should().Be(target.Height);
        foreach (var pair in chunks.Windows(2))
        {
            pair[0].Y.Should().BeLessOrEqualTo(pair[1].Y);
        }
    }

    [Fact]
    public void HorizontalSplitByHeight()
    {
        var target = new Rect(2, 2, 10, 10);

        var layout = new Layout
        {
            Direction = Boto.Layouts.Direction.Horizontal, Constraints = new() { Percentage(10), Max(5), Min(1) }
        };

        var chunks = layout.Split(target);

        chunks.Sum(x => x.Width).Should().Be(target.Width);
        foreach (var pair in chunks.Windows(2))
        {
            pair[0].X.Should().BeLessOrEqualTo(pair[1].X);
        }
    }

    [Fact]
    public void RectSizeTruncation()
    {
        for (var width = 256; width < 300; width++)
        {
            for (var height = 256; height < 300; height++)
            {
                var rect = new Rect(0, 0, width, height);
                rect.Invoking(r => r.Area)
                    .Should()
                    .NotThrow();

                rect.Width.Should().BeLessThan(width);
                rect.Height.Should().BeLessThan(height);

                // The target dimensions are rounded down so the math will not be too precise
                // but let's make sure the ratios don't diverge crazily.
                Math.Abs(rect.Width / rect.Height - width / height).Should().BeLessThan(1);
            }
        }

        const int Width = 900;
        const int Height = 100;
        var otherRect = new Rect(0, 0, Width, Height);

        otherRect.Width.Should().NotBe(900);
        otherRect.Height.Should().NotBe(100);

        otherRect.Width.Should().BeLessThan(Width);
        otherRect.Height.Should().BeLessThan(Height);
    }

    [Fact]
    public void RectSizePreservation()
    {
        for (var width = 0; width < 256; width++)
        {
            for (var height = 0; height < 256; height++)
            {
                var rect = new Rect(0, 0, width, height);
                rect.Area.Should().Be(width * height);

                rect.Width.Should().Be(width);
                rect.Height.Should().Be(height);
            }
        }

        // One dimension below 255, one above. Area below max u16.
        var otherRect = new Rect(0, 0, 300, 100);
        otherRect.Width.Should().Be(300);
        otherRect.Height.Should().Be(100);
    }

    [Fact]
    public void Direction()
    {
        var layout = new Layout();
        layout.SetDirection(Boto.Layouts.Direction.Horizontal).Direction.Should().Be(Boto.Layouts.Direction.Horizontal);
        layout.SetDirection(Boto.Layouts.Direction.Vertical).Direction.Should().Be(Boto.Layouts.Direction.Vertical);
    }

    [Fact]
    public void Margin()
    {
        var layout = new Layout();
        layout.SetMargin(1).Margin.Should().Be(new Margin(1, 1));
        layout.SetMargin(new Margin(2, 2)).Margin.Should().Be(new Margin(2, 2));
        layout.SetMargin(3, 3).Margin.Should().Be(new Margin(3, 3));
        layout.SetHorizontalMargin(5).Margin.Horizontal.Should().Be(5);
        layout.SetVerticalMargin(6).Margin.Vertical.Should().Be(6);
    }

    [Fact]
    public void ExpandToFill()
    {
        var layout = new Layout();
        layout.EnableExpandToFill().ExpandToFill.Should().BeTrue();
        layout.DisableExpandToFill().ExpandToFill.Should().BeFalse();

        layout.SetExpandToFill(true).ExpandToFill.Should().BeTrue();
    }

    [Fact]
    public void AddConstraints()
    {
        var layout = new Layout();
        layout.AddConstraints(Percentage(10), Max(5), Min(1))
            .Constraints
            .Should()
            .HaveCount(3);
        layout.Constraints.Clear();
        
        layout.AddConstraints(new List<IConstraint> { Percentage(10), Max(5), Min(1) })
            .Constraints.Should()
            .HaveCount(3);
        layout.Constraints.Clear();
        
        layout.AddConstraint(Min(1))
            .Constraints.Should()
            .HaveCount(1);
        layout.Constraints.Clear();
        
        layout.SetConstraints(new List<IConstraint> { Percentage(10), Max(5), Min(1) })
            .Constraints.Should()
            .HaveCount(3);
        layout.Constraints.Clear();
    }
}
