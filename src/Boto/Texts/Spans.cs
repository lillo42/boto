using System.Text;
using Boto.Styles;

namespace Boto.Texts;

public record Spans(List<Span> Value)
{
    public Spans(string text)
        : this(new List<Span> { new(text) })
    {
    }

    public Spans(string text, Style style)
        : this(new List<Span> { new(text, style) })
    {
    }

    public Spans(Span span)
        : this(new List<Span> { span })
    {
    }

    public int Width => Value.Sum(span => span.Width);

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var span in Value)
        {
            sb.Append(span.Content);
        }

        return sb.ToString();
    }

    public static implicit operator Spans(string text)
        => new(text);
}
