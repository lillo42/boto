using Boto.Styles;

namespace Boto.Texts;

/// <summary>
/// A string split over multiple lines where each line is composed of several clusters, each with
/// their own style.
/// </summary>
/// <param name="Lines"></param>
public record Text(List<Spans> Lines)
{
    /// <summary>
    /// Initialize Text from a content. 
    /// </summary>
    /// <param name="content">The content.</param>
    public Text(string content)
        : this(content.Split('\n')
            .Select(x => new Spans(x))
            .ToList())
    {
    }

    /// <summary>
    /// Initialize Text from a content and style.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="style">The <see cref="Style"/>.</param>
    public Text(string content, Style style)
        : this(content)
    {
        PatchStyle(style);
    }

    /// <summary>
    /// The max width of the text.
    /// </summary>
    public int Width => Lines.Max(line => line.Width);

    /// <summary>
    /// The height of the text (number of lines).
    /// </summary>
    public int Height => Lines.Count;

    /// <summary>
    /// Apply a style to all spans.
    /// </summary>
    /// <param name="style">The <see cref="Style"/>.</param>
    public void PatchStyle(Style style)
    {
        foreach (var line in Lines)
        {
            for (var spanIndex = 0; spanIndex < line.Value.Count; spanIndex++)
            {
                var span = line.Value[spanIndex];
                line.Value[spanIndex] = span with { Style = span.Style.Merge(style) };
            }
        }
    }

    /// <summary>
    /// Convert a string to a <see cref="Text"/>.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <returns>A new instance of <see cref="Text"/>.</returns>
    public static implicit operator Text(string content)
        => new(content);
}
