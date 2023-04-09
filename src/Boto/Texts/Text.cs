using Boto.Styles;

namespace Boto.Texts;

/// <summary>
/// A string split over multiple lines where each line is composed of several clusters, each with
/// their own style.
/// </summary>
/// <param name="Lines"></param>
public record Text(List<Spans> Lines)
{
    public Text(string content)
        : this(content.Split('\n')
            .Select(x => new Spans(x))
            .ToList())
    {
    }


    public Text(string content, Style style)
        : this(content)
    {
        PatchStyle(style);
    }

    public int Width => Lines.Max(line => line.Width);

    public int Height => Lines.Count;

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
}
