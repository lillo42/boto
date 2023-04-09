using Boto.Layouts;
using FluentAssertions;

namespace Boto.Tests.Layouts;

public class ConstraintTest
{
    [Theory]
    [InlineData(101)]
    [InlineData(-1)]
    public void Percentage_Should_Throw_When_ValueIsLargeThan100OrLessThan0(int percetange)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Constraints.Percentage(percetange));
    }
    
    [Fact]
    public void Percentage_Should_Return_The_Correct_Value()
    {
        var constraint = Constraints.Percentage(50);
        constraint.Should().BeOfType<PercentageConstraint>();
        constraint.Apply(100).Should().Be(50);
    }
    
    [Fact]
    public void Min_Should_Return_The_Correct_Value()
    {
        var constraint = Constraints.Min(50);
        constraint.Should().BeOfType<MinConstraint>();
        constraint.Apply(100).Should().Be(50);
    }
    
    [Fact]
    public void Max_Should_Return_The_Correct_Value()
    {
        var constraint = Constraints.Max(50);
        constraint.Should().BeOfType<MaxConstraint>();
        constraint.Apply(100).Should().Be(100);
    }
    
    [Fact]
    public void Length_Should_Return_The_Correct_Value()
    {
        var constraint = Constraints.Length(50);
        constraint.Should().BeOfType<LengthConstraint>();
        constraint.Apply(100).Should().Be(50);
    }
    
    [Fact]
    public void Ratio_Should_Return_The_Correct_Value()
    {
        var constraint = Constraints.Ratio(50, 100);
        constraint.Should().BeOfType<RatioConstraint>();
        constraint.Apply(100).Should().Be(50);
    }
}
