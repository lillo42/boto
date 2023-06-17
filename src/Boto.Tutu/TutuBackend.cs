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
using ITerminal = Tutu.Terminal.ITerminal;
using TerminalCommand = Tutu.Commands.Terminal;

namespace Boto.Tutu;

/// <summary>
/// The tutu implement of <see cref="IBackend"/>.
/// </summary>
public class TutuBackend : IBackend
{
    private readonly TextWriter _buffer;
    private readonly IQueueExecutor _queue;
    private readonly ITerminal _terminal;
    private readonly ICursor _cursor;

    /// <summary>
    /// Initialize a new instance of <see cref="TutuBackend"/>.
    /// </summary>
    public TutuBackend()
        : this(Console.Out, SystemTerminal.Instance, SystemCursor.Instance)
    {
    }

    /// <summary>
    /// Initialize a new instance of <see cref="TutuBackend"/>.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    public TutuBackend(TextWriter buffer)
        : this(buffer, SystemTerminal.Instance, SystemCursor.Instance)
    {
    }

    /// <summary>
    /// Initialize a new instance of <see cref="TutuBackend"/>.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <param name="terminal">The <see cref="ITerminal"/>.</param>
    public TutuBackend(TextWriter buffer, ITerminal terminal)
        : this(buffer, terminal, SystemCursor.Instance)
    {
    }

    /// <summary>
    /// Initialize a new instance of <see cref="TutuBackend"/>.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <param name="terminal">The <see cref="ITerminal"/>.</param>
    /// <param name="cursor">The <see cref="ICursor"/>.</param>
    public TutuBackend(TextWriter buffer, ITerminal terminal, ICursor cursor)
    {
        _buffer = buffer;
        _queue = _buffer.Enqueue();
        _terminal = terminal;
        _cursor = cursor;
    }

    /// <inheritdoc cref="IBackend.Draw"/>
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

            if (diff.Cell.Foreground != fg)
            {
                var color = diff.Cell.Foreground.MapToTutuColor();
                _queue.Enqueue(SetForegroundColor(color));
                fg = diff.Cell.Foreground;
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

    /// <inheritdoc cref="IBackend.HideCursor"/>
    public void HideCursor()
        => _buffer.Execute(Hide);

    /// <inheritdoc cref="IBackend.ShowCursor"/>
    public void ShowCursor()
        => _buffer.Execute(Show);

    /// <inheritdoc cref="IBackend.Clear"/>
    public void Clear()
        => _buffer.Execute(TerminalCommand.Clear(ClearType.All));

    /// <inheritdoc cref="IBackend.Flush"/>
    public void Flush()
        => _queue.Flush();

    /// <inheritdoc cref="IBackend.Size"/>
    public Rect Size
    {
        get
        {
            var size = _terminal.Size;
            return new Rect(0, 0, size.Width, size.Height);
        }
    }

    /// <inheritdoc cref="IBackend.CursorPosition"/>
    public CursorPosition CursorPosition
    {
        get
        {
            var position = _cursor.Position;
            return new CursorPosition(position.Row, position.Column);
        }

        set => _buffer.Execute(MoveTo(value.Column, value.Row));
    }
}
