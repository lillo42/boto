using Cassowary;

namespace Boto.Layouts;

/// <summary>
/// A container used by the solver inside split.
/// </summary>
internal record Element(Variable X, Variable Y, Variable Width, Variable Height)
{
    public Element()
        : this(new(), new(), new(), new())
    {
    }

    public Variable Left => X;
    public Variable Top => Y;
    public Expression Right => X + Width;
    public Expression Bottom => Y + Height;
}
