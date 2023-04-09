using System.Collections;
using System.Globalization;
using Boto.Extensions;
using Boto.Texts;

namespace Boto.Widget.Reflow;

public class LineTruncator : IEnumerable<(List<StyledGrapheme>, int)>, IEnumerator<(List<StyledGrapheme>, int)>
{
    public LineTruncator(IEnumerable<StyledGrapheme> symbols, int maxLineWidth)
    {
        _symbols = symbols.GetEnumerator();
        _maxLineWidth = maxLineWidth;
        HorizontalOffset = 0;
    }

    private readonly IEnumerator<StyledGrapheme> _symbols;
    private readonly int _maxLineWidth;
    public int HorizontalOffset { get; set; }

    public IEnumerator<(List<StyledGrapheme>, int)> GetEnumerator() => this;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool MoveNext()
    {
        if (_maxLineWidth == 0)
        {
            return false;
        }

        var currentLine = new List<StyledGrapheme>();
        var currentLineWidth = 0;

        var isSkipRest = false;
        var isSymbolExhasted = true;
        var horizontalOffset = HorizontalOffset;
        while(_symbols.MoveNext())
        {
            var styledGrapheme = _symbols.Current;
            var (symbol, _) = styledGrapheme;
            isSymbolExhasted = false;

            // Ignore characters wider that the total max width.
            if (symbol.Width() > _maxLineWidth)
            {
                continue;
            }

            // Break on newline and discard it.
            if (symbol == "\n")
            {
                break;
            }

            if (currentLineWidth + symbol.Width() > _maxLineWidth)
            {
                isSkipRest = true;
                break;
            }

            if (horizontalOffset != 0)
            {
                var w = symbol.Width();
                if (w > horizontalOffset)
                {
                    var t = TrimOffset(symbol, horizontalOffset);
                    horizontalOffset = 0;
                    symbol = t;
                }
                else
                {
                    horizontalOffset -= w;
                    symbol = "";
                }
            }

            currentLineWidth += symbol.Width();
            currentLine.Add(styledGrapheme);
        }

        if (isSkipRest)
        {
            while(_symbols.MoveNext())
            {
                var symbol = _symbols.Current;
                if (symbol.Symbol == "\n")
                {
                    break;
                }
            }
        }

        if (isSymbolExhasted && currentLine.Count == 0)
        {
            return false;
        }

        Current = (currentLine, currentLineWidth);
        return true;

        static string TrimOffset(string src, int offset)
        {
            var start = 0;
            var text = StringInfo.GetTextElementEnumerator(src);
            while (text.MoveNext())
            {
                var c = text.GetTextElement();
                var w = c.Width();
                if (w > offset)
                {
                    break;
                }

                offset -= w;
                start += c.Length;
            }

            return src[start..];
        }
    }

    public void Reset()
    {
        _symbols.Reset();
    }

    public (List<StyledGrapheme>, int) Current { get; private set; }

    object IEnumerator.Current => Current;

    public void Dispose() => _symbols.Dispose();
}
