namespace Boto.Layouts;

/// <summary>
/// The constraint for a control.
/// </summary>
public static class Constraints
{
    /// <summary>
    /// Create a <see cref="PercentageConstraint"/> constraint.
    /// </summary>
    /// <param name="percentage">The current percentage </param>
    /// <returns>New instance of <see cref="PercentageConstraint"/>.</returns>
    public static IConstraint Percentage(int percentage) => new PercentageConstraint(percentage);

    /// <summary>
    /// Create a <see cref="RatioConstraint"/> constraint.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="density">The density.</param>
    /// <returns>New instance of <see cref="RatioConstraint"/>.</returns>
    public static IConstraint Ratio(int value, int density) => new RatioConstraint(value, density);

    /// <summary>
    /// Create a <see cref="LengthConstraint"/>. constraint.
    /// </summary>
    /// <param name="value">The length.</param>
    /// <returns>New instance of <see cref="LengthConstraint"/>.</returns>
    public static IConstraint Length(int value) => new LengthConstraint(value);

    /// <summary>
    /// Create a <see cref="MaxConstraint"/> constraint.
    /// </summary>
    /// <param name="value">The value to be compared.</param>
    /// <returns>New instance of <see cref="MaxConstraint"/>.</returns>
    public static IConstraint Max(int value) => new MaxConstraint(value);

    /// <summary>
    /// Create a <see cref="MinConstraint"/> constraint.
    /// </summary>
    /// <param name="value">The value to be compared.</param>
    /// <returns>New instance of <see cref="MinConstraint"/>.</returns>
    public static IConstraint Min(int value) => new MinConstraint(value);
}

/// <summary>
/// An interface for constraints.
/// </summary>
public interface IConstraint
{
    /// <summary>
    /// Apply the constraint.
    /// </summary>
    /// <param name="lenght">The screen lenght.</param>
    int Apply(int lenght);
}

/// <summary>
/// The ratio constraint. 
/// </summary>
/// <param name="Value">The value.</param>
/// <param name="Density">The density.</param>
public record RatioConstraint(int Value, int Density) : IConstraint
{
    /// <inheritdoc cref="IConstraint.Apply"/>
    public int Apply(int lenght) => Value * lenght / Density;
}

/// <summary>
/// The length constraint.
/// </summary>
/// <param name="Length">The length.</param>
public record LengthConstraint(int Length) : IConstraint
{
    /// <inheritdoc cref="IConstraint.Apply"/>
    public int Apply(int lenght) => Math.Min(Length, lenght);
}

/// <summary>
/// The max constraint.
/// </summary>
/// <param name="Max">The value to be compared.</param>
public record MaxConstraint(int Max) : IConstraint
{
    /// <inheritdoc cref="IConstraint.Apply"/>
    public int Apply(int lenght) => Math.Max(Max, lenght);
}

/// <summary>
/// The min constraint.
/// </summary>
/// <param name="Min">The value to be compared.</param>
public record MinConstraint(int Min) : IConstraint
{
    /// <inheritdoc cref="IConstraint.Apply"/>
    public int Apply(int lenght) => Math.Min(Min, lenght);
}

/// <summary>
/// The percentage constraint.
/// </summary>
public record PercentageConstraint : IConstraint
{
    /// <summary>
    /// The current percentage.
    /// </summary>
    public int Percentage { get; }

    /// <summary>
    /// Initialize a new instance of <see cref="PercentageConstraint"/>.
    /// </summary>
    /// <param name="percentage">The percentage.</param>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="percentage"/> is less than 0 and greater than 100</exception>
    public PercentageConstraint(int percentage)
    {
        if (percentage is < 0 or > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(percentage), "Percentage must be between 0 and 100.");
        }

        Percentage = percentage;
    }

    /// <inheritdoc cref="IConstraint.Apply"/>
    public int Apply(int lenght) => lenght * Percentage / 100;
}
