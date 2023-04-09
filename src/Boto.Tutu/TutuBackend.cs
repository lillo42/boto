using Boto.Buffers;
using Boto.Layouts;
using Boto.Styles;
using Boto.Terminals;
using Boto.Tutu.Extensions;
using Tutu;
using Tutu.Cursor;
using Tutu.Extensions;
using Tutu.Terminal;
using static Tutu.Commands.Cursor;
using static Tutu.Commands.Style;
using Attribute = Tutu.Style.Types.Attribute;
using CursorPosition = Boto.Terminals.CursorPosition;
using TerminalCommand = Tutu.Commands.Terminal;

namespace Boto.Tutu;

public class TutuBackend : IBackend
{
    private readonly TextWriter _buffer;
    private readonly IQueueExecutor _queue;

    public TutuBackend(TextWriter buffer)
    {
        _buffer = buffer;
        _queue = _buffer.Enqueue();
    }

    public void Draw(IEnumerable<BufferDiff> content)
    {
        var fg = Color.Reset;
        var bg = Color.Reset;
        var modifier = Modifier.Empty;
        (int x, int y)? lastPos = null;

        foreach (var diff in content)
        {
            // Move the cursor if the previous location was not (x - 1, y)
            if (lastPos is not { } pos || !(diff.Column == pos.x + 1 && diff.Row == pos.y + 1))
            {
                _queue.Enqueue(MoveTo(diff.Column, diff.Row));
            }

            lastPos = (diff.Column, diff.Row);
            if (diff.Cell.Modifier != modifier)
            {
                var dff = new ModifierDiff(modifier, diff.Cell.Modifier);
                dff.Queue(_queue);
                modifier = diff.Cell.Modifier;
            }

            if (diff.Cell.ForegroundColor != fg)
            {
                var color = diff.Cell.ForegroundColor.MapToTutuColor();
                _queue.Enqueue(SetForegroundColor(color));
                fg = diff.Cell.ForegroundColor;
            }

            if (diff.Cell.BackgroundColor != bg)
            {
                var color = diff.Cell.BackgroundColor.MapToTutuColor();
                _queue.Enqueue(SetBackgroundColor(color));
                bg = diff.Cell.BackgroundColor;
            }

            _queue.Enqueue(Print(diff.Cell.Symbol));
        }

        _queue.Enqueue(
            SetForegroundColor(Color.Reset.MapToTutuColor()),
            SetBackgroundColor(Color.Reset.MapToTutuColor()),
            SetAttribute(Attribute.Reset)
        );
    }

    public void HideCursor()
        => _buffer.Execute(Hide);

    public void ShowCursor()
        => _buffer.Execute(Show);

    public void Clear()
        => _buffer.Execute(TerminalCommand.Clear(ClearType.All));

    public void Flush()
        => _queue.Flush();

    public Rect Size
    {
        get
        {
            var size = SystemTerminal.Size;
            return new Rect(0, 0, size.Width, size.Height);
        }
    }

    public CursorPosition CursorPosition
    {
        get
        {
            var position = SystemCursor.Instance.Position;
            return new CursorPosition(position.Row, position.Column);
        }

        set => _buffer.Execute(MoveTo(value.Column, value.Row));
    }
}
