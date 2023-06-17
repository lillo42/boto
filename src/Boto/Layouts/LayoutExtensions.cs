namespace Boto.Layouts;

/// <summary>
/// The <see cref="Layout"/> extensions method.
/// </summary>
public static class LayoutExtensions
{
    /// <summary>
    /// Change the <see cref="Layout.Direction"/>
    /// </summary>
    /// <param name="layout">The <see cref="Layout"/>.</param>
    /// <param name="direction">The <see cref="Direction"/>.</param>
    /// <returns>The <paramref name="layout"/> with <see cref="Layout.Direction"/> as <paramref name="direction"/>.</returns>
    public static Layout SetDirection(this Layout layout, Direction direction)
    {
        layout.Direction = direction;
        return layout;
    }

    /// <summary>
    /// Change the <see cref="Layout.Margin"/>.
    /// </summary>
    /// <param name="layout">The <see cref="Layout"/>.</param>
    /// <param name="margin">The <see cref="Margin"/>.</param>
    /// <returns>The <paramref name="layout"/> with <see cref="Layout.Margin"/> as <paramref name="margin"/>.</returns>
    public static Layout SetMargin(this Layout layout, Margin margin)
    {
        layout.Margin = margin;
        return layout;
    }

    /// <summary>
    /// Change the <see cref="Layout.Margin"/>.
    /// </summary>
    /// <param name="layout">The <see cref="Layout"/>.</param>
    /// <param name="margin">The margin value.</param>
    /// <returns>The <paramref name="layout"/> with <see cref="Layout.Margin"/> as <paramref name="margin"/>.</returns>
    public static Layout SetMargin(this Layout layout, int margin)
    {
        layout.Margin = new(margin, margin);
        return layout;
    }

    /// <summary>
    /// Change the <see cref="Layout.Margin"/>.
    /// </summary>
    /// <param name="layout">The <see cref="Layout"/>.</param>
    /// <param name="horizontal">The horizontal value.</param>
    /// <param name="vertical">The vertical value.</param>
    /// <returns>The <paramref name="layout"/> with <see cref="Layout.Margin"/> as <paramref name="horizontal"/>, <paramref name="vertical"/>.</returns>
    public static Layout SetMargin(this Layout layout, int horizontal, int vertical)
    {
        layout.Margin = new(vertical, horizontal);
        return layout;
    }

    /// <summary>
    /// Change the <see cref="Layout.Margin"/> horizontal value.
    /// </summary>
    /// <param name="layout">The <see cref="Layout"/>.</param>
    /// <param name="horizontal">The horizontal value.</param>
    /// <returns>The <paramref name="layout"/> with <see cref="Layout.Margin"/>.<see cref="Margin.Horizontal"/> as <paramref name="horizontal"/>.</returns>
    public static Layout SetHorizontalMargin(this Layout layout, int horizontal)
    {
        layout.Margin = layout.Margin with { Horizontal = horizontal };
        return layout;
    }

    /// <summary>
    /// Change the <see cref="Layout.Margin"/> vertical value.
    /// </summary>
    /// <param name="layout">The <see cref="Layout"/>.</param>
    /// <param name="vertical">The vertical value.</param>
    /// <returns>The <paramref name="layout"/> with <see cref="Layout.Margin"/>.<see cref="Margin.Vertical"/> as <paramref name="vertical"/>.</returns>
    public static Layout SetVerticalMargin(this Layout layout, int vertical)
    {
        layout.Margin = layout.Margin with { Vertical = vertical };
        return layout;
    }

    /// <summary>
    /// Change the <see cref="Layout.ExpandToFill"/>.
    /// </summary>
    /// <param name="layout">The <see cref="Layout"/>.</param>
    /// <param name="expandToFill">Flags indicating should expand.</param>
    /// <returns>The <paramref name="layout"/> with <see cref="Layout.ExpandToFill"/> as <paramref name="expandToFill"/>.</returns>
    public static Layout SetExpandToFill(this Layout layout, bool expandToFill)
    {
        layout.ExpandToFill = expandToFill;
        return layout;
    }

    /// <summary>
    /// Enable <see cref="Layout.ExpandToFill"/>.
    /// </summary>
    /// <param name="layout">The <see cref="Layout"/>.</param>
    /// <returns>The <paramref name="layout"/> with <see cref="Layout.ExpandToFill"/> as true.</returns>
    public static Layout EnableExpandToFill(this Layout layout)
    {
        layout.ExpandToFill = true;
        return layout;
    }

    /// <summary>
    /// Disable <see cref="Layout.ExpandToFill"/>.
    /// </summary>
    /// <param name="layout">The <see cref="Layout"/>.</param>
    /// <returns>The <paramref name="layout"/> with <see cref="Layout.ExpandToFill"/> as false.</returns>
    public static Layout DisableExpandToFill(this Layout layout)
    {
        layout.ExpandToFill = false;
        return layout;
    }

    /// <summary>
    /// Add a <see cref="IConstraint"/> to the <see cref="Layout.Constraints"/>.
    /// </summary>
    /// <param name="layout">The <see cref="Layout"/>.</param>
    /// <param name="constraint">The <see cref="IConstraint"/>.</param>
    /// <returns>The <paramref name="layout"/> with <see cref="Layout.Constraints"/> plus <paramref name="constraint"/>.</returns>
    public static Layout AddConstraint(this Layout layout, IConstraint constraint)
    {
        layout.Constraints.Add(constraint);
        return layout;
    }

    /// <summary>
    /// Add a collection <see cref="IConstraint"/> to the <see cref="Layout.Constraints"/>.
    /// </summary>
    /// <param name="layout">The <see cref="Layout"/>.</param>
    /// <param name="constraints">The <see cref="IEnumerable{T}"/> of <see cref="IConstraint"/>.</param>
    /// <returns>The <paramref name="layout"/> with <see cref="Layout.Constraints"/> plus <paramref name="constraints"/>.</returns>
    public static Layout AddConstraints(this Layout layout, IEnumerable<IConstraint> constraints)
    {
        layout.Constraints.AddRange(constraints);
        return layout;
    }

    /// <summary>
    /// Add a collection <see cref="IConstraint"/> to the <see cref="Layout.Constraints"/>.
    /// </summary>
    /// <param name="layout">The <see cref="Layout"/>.</param>
    /// <param name="constraints">The <see cref="IEnumerable{T}"/> of <see cref="IConstraint"/>.</param>
    /// <returns>The <paramref name="layout"/> with <see cref="Layout.Constraints"/> plus <paramref name="constraints"/>.</returns>
    public static Layout AddConstraints(this Layout layout, params IConstraint[] constraints)
    {
        layout.Constraints.AddRange(constraints);
        return layout;
    }

    /// <summary>
    /// Change the <see cref="Layout.Constraints"/>. 
    /// </summary>
    /// <param name="layout">The <see cref="Layout"/>.</param>
    /// <param name="constraints">The <see cref="IEnumerable{T}"/> of <see cref="IConstraint"/>.</param>
    /// <returns>The <paramref name="layout"/> with <see cref="Layout.Constraints"/> as <paramref name="constraints"/>.</returns>
    public static Layout SetConstraints(this Layout layout, IEnumerable<IConstraint> constraints)
    {
        layout.Constraints = constraints.ToList();
        return layout;
    }
}
