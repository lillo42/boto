using System.Text;
using Boto.Styles;

namespace Boto.Texts;

/// <summary>
/// A Span collection.
/// </summary>
/// <param name="Value">The <see cref="List{T}"/> of <see cref="Span"/>.</param>
public record Spans(List<Span> Value)
{
    /// <summary>
    /// Initialize Spans from a content.
    /// </summary>
    /// <param name="text">The text.</param>
    public Spans(string text)
        : this(new List<Span> { new(text) })
    {
    }

    /// <summary>
    /// Initialize Spans from a content and style.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    public Spans(string text, Style style)
        : this(new List<Span> { new(text, style) })
    {
    }

    /// <summary>
    /// Initialize Spans from a <see cref="Span"/>.
    /// </summary>
    /// <param name="span">The <see cref="List{T}"/> of <see cref="Span"/>.</param>
    public Spans(Span span)
        : this(new List<Span> { span })
    {
    }

    /// <summary>
    /// Sum of all spans width.
    /// </summary>
    public int Width => Value.Sum(span => span.Width);

    /// <inheritdoc />
    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var span in Value)
        {
            sb.Append(span.Content);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Convert a string to a <see cref="Spans"/>.
    /// </summary>
    /// <param name="text">The content.</param>
    /// <returns>New instance <see cref="Span"/>.</returns>
    public static implicit operator Spans(string text)
        => new(text);
}
