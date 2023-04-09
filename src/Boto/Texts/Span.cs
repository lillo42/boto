using System.Globalization;
using Boto.Extensions;
using Boto.Styles;

namespace Boto.Texts;

/// <summary>
/// A string where all graphemes have the same style.
/// </summary>
public record Span(string Content, Style Style)
{
    public Span(string content)
        : this(content, new Style())
    {
    }
    
    /// <summary>
    /// The width of the content held by this span.
    /// </summary>
    public int Width => Content.Width();

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

    public static implicit operator Span(string text)
        => new(text);
}
