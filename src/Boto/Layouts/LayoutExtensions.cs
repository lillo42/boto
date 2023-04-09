namespace Boto.Layouts;

public static class LayoutExtensions
{
    public static Layout Direction(this Layout layout, Direction direction)
    {
        layout.Direction = direction;
        return layout;
    }

    public static Layout HorizontalDirection(this Layout layout)
    {
        layout.Direction = Layouts.Direction.Horizontal;
        return layout;
    }

    public static Layout VerticalDirection(this Layout layout)
    {
        layout.Direction = Layouts.Direction.Vertical;
        return layout;
    }

    public static Layout Margin(this Layout layout, Margin margin)
    {
        layout.Margin = margin;
        return layout;
    }

    public static Layout Margin(this Layout layout, int margin)
    {
        layout.Margin = new(margin, margin);
        return layout;
    }

    public static Layout Margin(this Layout layout, int horizontal, int vertical)
    {
        layout.Margin = new(horizontal, vertical);
        return layout;
    }

    public static Layout HorizontalMargin(this Layout layout, int margin)
    {
        layout.Margin = layout.Margin with { Horizontal = margin };
        return layout;
    }

    public static Layout VerticalMargin(this Layout layout, int margin)
    {
        layout.Margin = layout.Margin with { Vertical = margin };
        return layout;
    }
    
    public static Layout ExpandToFill(this Layout layout, bool expandToFill)
    {
        layout.ExpandToFill = expandToFill;
        return layout;
    }
    
    public static Layout EnableExpandToFill(this Layout layout)
    {
        layout.ExpandToFill = true;
        return layout;
    }
    
    public static Layout DisableExpandToFill(this Layout layout)
    {
        layout.ExpandToFill = false;
        return layout;
    }
    
    public static Layout AddConstraint(this Layout layout, IConstraint constraint)
    {
        layout.Constraints.Add(constraint);
        return layout;
    }
    
    public static Layout AddConstraints(this Layout layout, IEnumerable<IConstraint> constraints)
    {
        layout.Constraints.AddRange(constraints);
        return layout;
    }
    
    public static Layout AddConstraints(this Layout layout, params IConstraint[] constraints)
    {
        layout.Constraints.AddRange(constraints);
        return layout;
    }
    
    public static Layout Constraints(this Layout layout, IEnumerable<IConstraint> constraint)
    {
        layout.Constraints = constraint.ToList();
        return layout;
    }
}
