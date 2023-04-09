using System.Collections;
using Boto.Extensions;
using Boto.Texts;

namespace Boto.Widget.Reflow;

internal class WordWrapper : IEnumerable<(List<StyledGrapheme>, int)>,
    IEnumerator<(List<StyledGrapheme>, int)>
{
    public WordWrapper(IEnumerable<StyledGrapheme> symbols, int maxLineWidth = 1, bool isTrim = false)
    {
        _symbols = symbols.GetEnumerator();
        _maxLineWidth = maxLineWidth;
        _isTrim = isTrim;
    }

    private List<StyledGrapheme> _currentLine = new();
    private List<StyledGrapheme> _nextLine = new();
    private readonly IEnumerator<StyledGrapheme> _symbols; 
    private readonly int _maxLineWidth;
    private readonly bool _isTrim; 


    public IEnumerator<(List<StyledGrapheme>, int)> GetEnumerator() => this;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public bool MoveNext()
    {
        if (_maxLineWidth == 0)
        {
            return false;
        }

        (_currentLine, _nextLine) = (_nextLine, _currentLine);
        _nextLine.Clear();

        var currentLineWidth = _currentLine
            .Sum(x => x.Symbol.Width());

        var symbolsToLastWordEnd = 0;
        var widthToLastWordEnd = 0;
        var isPrevWhitespace = false;
        var isSymbolExhausted = true;

        while (_symbols.MoveNext())
        {
            var (symbol, style) = _symbols.Current;
            isSymbolExhausted = false;
            var hasSymbolWhitespace = string.IsNullOrWhiteSpace(symbol) && symbol != Const.NBSP;

            // Ignore characters wider that the total max width.
            if (symbol.Width() > _maxLineWidth ||
                _isTrim && hasSymbolWhitespace && symbol != "\n" && currentLineWidth == 0)
            {
                continue;
            }

            // Break on newline and discard it.
            if (symbol == "\n")
            {
                if (isPrevWhitespace)
                {
                    currentLineWidth = widthToLastWordEnd;
                    _currentLine.RemoveRange(symbolsToLastWordEnd, _currentLine.Count - symbolsToLastWordEnd);
                }

                break;
            }

            // Mark the previous symbol as word end.
            if (hasSymbolWhitespace && !isPrevWhitespace)
            {
                symbolsToLastWordEnd = _currentLine.Count;
                widthToLastWordEnd = currentLineWidth;
            }

            _currentLine.Add(new(symbol, style));
            currentLineWidth += symbol.Width();

            if (currentLineWidth > _maxLineWidth)
            {
                // If there was no word break in the text, wrap at the end of the line.
                var (truncateAt, truncateWidth) = symbolsToLastWordEnd != 0
                    ? (symbolsToLastWordEnd, widthToLastWordEnd)
                    : (_currentLine.Count - 1, _maxLineWidth);

                // Push the remainder to the next line but strip leading whitespace:
                var remainder = _currentLine.Skip(truncateAt).ToList();
                var indexOf = remainder.FindIndex(x => !string.IsNullOrWhiteSpace(x.Symbol));
                if (indexOf >= 0)
                {
                    _nextLine.AddRange(remainder.Skip(indexOf));
                }

                _currentLine.RemoveRange(truncateAt, _currentLine.Count - truncateAt);
                currentLineWidth = truncateWidth;
                break;
            }

            isPrevWhitespace = hasSymbolWhitespace;
        }


        // Even if the iterator is exhausted, pass the previous remainder.
        if (isSymbolExhausted && _currentLine.Count == 0)
        {
            return false;
        }
        
        Current = (_currentLine, currentLineWidth);
        return true;
    }

    public void Reset()
    {
        _symbols.Reset();
        _currentLine = new();
        _nextLine = new();
    }

    public (List<StyledGrapheme>, int) Current { get; private set; }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        _symbols.Dispose();
    }
}
