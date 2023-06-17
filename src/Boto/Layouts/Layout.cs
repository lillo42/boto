using Boto.Extensions;
using Cassowary;
using static Cassowary.Strength;
using static Cassowary.WeightedRelation;
using CassowaryConstraint = Cassowary.Constraint;

namespace Boto.Layouts;

/// <summary>
/// The layout.
/// </summary>
public class Layout
{
    private static readonly ThreadLocal<Dictionary<(Rect, Layout), List<Rect>>> s_cache = new(() =>
        new Dictionary<(Rect, Layout), List<Rect>>());

    /// <summary>
    /// Initializes a new instance of the <see cref="Layout"/> class.
    /// </summary>
    public Layout()
        : this(Direction.Vertical, new(0, 0), new List<IConstraint>(), true)
    {
    }

  
    /// <summary>
    /// Initializes a new instance of the <see cref="Layout"/> class.
    /// </summary>
    /// <param name="direction">The <see cref="Boto.Layouts.Direction"/>.</param>
    /// <param name="margin">The <see cref="Layouts.Margin"/>.</param>
    /// <param name="constraints">The collection of <see cref="IConstraint"/>.</param>
    /// <param name="expandToFill">Flags indicating should expand.</param>
    public Layout(Direction direction, Margin margin, List<IConstraint> constraints, bool expandToFill)
    {
        Direction = direction;
        Margin = margin;
        Constraints = constraints;
        ExpandToFill = expandToFill;
    }

    /// <summary>
    /// The <see cref="Boto.Layouts.Direction"/>.
    /// </summary>
    public Direction Direction { get; set; }
    
    /// <summary>
    /// The <see cref="Layouts.Margin"/>.
    /// </summary>
    public Margin Margin { get; set; }
    
    /// <summary>
    /// The collection of <see cref="IConstraint"/>.
    /// </summary>
    public List<IConstraint> Constraints { get; set; }
    
    /// <summary>
    /// Flags indicating should expand.
    /// </summary>
    public bool ExpandToFill { get; set; }

    /// <summary>
    /// Split the area.
    /// </summary>
    /// <param name="area">The area to be split.</param>
    /// <returns>The collection of <see cref="Rect"/>.</returns>
    public List<Rect> Split(Rect area)
    {
        // TODO: Maybe use a fixed size cache?
        if (!s_cache.Value!.TryGetValue((area, this), out var cache))
        {
            s_cache.Value![(area, this)] = cache = Split(area, this);
        }

        return cache;
    }

    private static List<Rect> Split(Rect area, Layout layout)
    {
        var solver = new Solver();
        var vars = new Dictionary<Variable, (int, uint)>();

        var elements = layout.Constraints.Select(_ => new Element()).ToList();
        var result = layout.Constraints.Select(_ => new Rect()).ToList();

        var destArea = area.Inner(layout.Margin);

        for (var index = 0; index < elements.Count; index++)
        {
            var element = elements[index];
            vars.Add(element.X, (index, 0));
            vars.Add(element.Y, (index, 1));
            vars.Add(element.Width, (index, 2));
            vars.Add(element.Height, (index, 3));
        }

        var ccs = new List<CassowaryConstraint>(elements.Count * 4 + layout.Constraints.Count * 6);
        foreach (var element in elements)
        {
            ccs.Add(element.Width | GreaterOrEq(Required) | 0);
            ccs.Add(element.Height | GreaterOrEq(Required) | 0);
            ccs.Add(element.Left | GreaterOrEq(Required) | destArea.Left);
            ccs.Add(element.Top | GreaterOrEq(Required) | destArea.Top);
            ccs.Add(element.Right | LessOrEq(Required) | destArea.Right);
            ccs.Add(element.Bottom | LessOrEq(Required) | destArea.Bottom);
        }

        if (elements.Count > 0)
        {
            var first = elements[0];
            ccs.Add(layout.Direction switch
            {
                Direction.Horizontal => first.Left | Eq(Required) | destArea.Left,
                Direction.Vertical => first.Top | Eq(Required) | destArea.Top,
                _ => throw new ArgumentOutOfRangeException(nameof(layout.Direction), "Invalid direction.")
            });
        }

        if (layout.ExpandToFill && elements.Count > 0)
        {
            var last = elements[^1];
            ccs.Add(layout.Direction switch
            {
                Direction.Horizontal => last.Right | Eq(Required) | destArea.Right,
                Direction.Vertical => last.Bottom | Eq(Required) | destArea.Bottom,
                _ => throw new ArgumentOutOfRangeException(nameof(layout.Direction), "Invalid direction.")
            });
        }

        if (layout.Direction == Direction.Horizontal)
        {
            ccs.AddRange(elements.Windows(2)
                .Select(pair => (pair[0].X + pair[0].Width) | Eq(Required) | pair[1].X));

            for (var index = 0; index < layout.Constraints.Count; index++)
            {
                var constraint = layout.Constraints[index];
                ccs.Add(elements[index].Y | Eq(Required) | destArea.Y);
                ccs.Add(elements[index].Height | Eq(Required) | destArea.Height);

                ccs.Add(constraint switch
                {
                    LengthConstraint length => elements[index].Width | Eq(Weak) | length.Length,
                    PercentageConstraint percentage => elements[index].Width
                                                       | Eq(Weak)
                                                       | (destArea.Width * percentage.Percentage / 100d),
                    RatioConstraint ratio => elements[index].Width
                                             | Eq(Weak)
                                             | (destArea.Width * ratio.Value / (double)ratio.Density),
                    MaxConstraint max => elements[index].Width | LessOrEq(Weak) | max.Max,
                    MinConstraint min => elements[index].Width | GreaterOrEq(Weak) | min.Min,
                    _ => throw new ArgumentException("Invalid constraint.")
                });
            }
        }
        else if (layout.Direction == Direction.Vertical)
        {
            ccs.AddRange(elements.Windows(2)
                .Select(pair => (pair[0].Y + pair[0].Height) | Eq(Required) | pair[1].Y));

            for (var index = 0; index < layout.Constraints.Count; index++)
            {
                var constraint = layout.Constraints[index];
                ccs.Add(elements[index].X | Eq(Required) | destArea.X);
                ccs.Add(elements[index].Width | Eq(Required) | destArea.Width);

                ccs.Add(constraint switch
                {
                    LengthConstraint length => elements[index].Height | Eq(Weak) | length.Length,
                    PercentageConstraint percentage => elements[index].Height
                                                       | Eq(Weak)
                                                       | (percentage.Percentage * destArea.Height / 100d),
                    RatioConstraint ratio => elements[index].Height
                                             | Eq(Weak)
                                             | (destArea.Height * ratio.Value / (double)ratio.Density),
                    MaxConstraint max => elements[index].Height | LessOrEq(Weak) | max.Max,
                    MinConstraint min => elements[index].Height | GreaterOrEq(Weak) | min.Min,
                    _ => throw new ArgumentException("Invalid constraint.")
                });
            }
        }

        solver.AddConstraints(ccs);

        foreach (var (variable, value) in solver.FetchChanges())
        {
            var (index, attr) = vars[variable];
            var val = (int)Math.Max(value, 0);

            result[index] = attr switch
            {
                0 => result[index] with { X = val },
                1 => result[index] with { Y = val },
                2 => result[index] with { Width = val },
                3 => result[index] with { Height = val },
                _ => result[index]
            };
        }

        // Fix imprecision by extending the last item a bit if necessary
        if (layout.ExpandToFill && result.Count > 0)
        {
            var last = result[^1];
            result[^1] = layout.Direction switch
            {
                Direction.Vertical => last with { Height = destArea.Bottom - last.Y },
                Direction.Horizontal => last with { Width = destArea.Right - last.X },
                _ => throw new ArgumentOutOfRangeException(nameof(layout.Direction), "Invalid direction.")
            };
        }

        return result;
    }
}
