using System.Globalization;
using Boto.Extensions;
using Boto.Styles;

namespace Boto.Texts;

/// <summary>
/// A string where all graphemes have the same style.
/// </summary>
public record Span(string Content, Style Style)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Span"/> class.
    /// </summary>
    /// <param name="content">The content.</param>
    public Span(string content)
        : this(content, new Style())
    {
    }
    
    /// <summary>
    /// The width of the content held by this span.
    /// </summary>
    public int Width => Content.Width();

    /// <summary>
    /// Create a collection of <see cref="StyledGrapheme"/>s from this span.
    /// </summary>
    /// <param name="style">The <see cref="Styles.Style"/>.</param>
    /// <returns>The <see cref="IEnumerable{T}"/> of <see cref="StyledGrapheme"/>.</returns>
    public IEnumerable<StyledGrapheme> StyledGraphemes(Style style)
    {
        var enumerator = StringInfo.GetTextElementEnumerator(Content);
        while (enumerator.MoveNext())
        {
            var current = enumerator.GetTextElement();
            if (current != "\n")
            {
                yield return new StyledGrapheme(current, style.Merge(Style));
            }
        }
    }

    /// <summary>
    /// Convert a string to a <see cref="Span"/>.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <returns>New <see cref="Span"/>.</returns>
    public static implicit operator Span(string content)
        => new(content);
}
