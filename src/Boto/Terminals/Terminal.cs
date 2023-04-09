using Boto.Layouts;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Terminals;

/// <summary>
/// Default implementation of <see cref="ITerminal"/>.
/// </summary>
public class Terminal : Terminal<IBackend>, ITerminal
{
    public Terminal(IBackend backend)
        : base(backend)
    {
    }

    public Terminal(IBackend backend, TerminalOptions options)
        : base(backend, options)
    {
    }
}

/// <summary>
/// Default implementation of <see cref="ITerminal{T}"/>.
/// </summary>
/// <typeparam name="T">The <see cref="IBackend"/>.</typeparam>
public class Terminal<T> : ITerminal<T>
    where T : class, IBackend
{
    private int _current;

    /// <summary>
    /// Initializes a new instance of the <see cref="Terminal{T}"/> class.
    /// </summary>
    /// <param name="backend">The <see cref="IBackend"/>.</param>
    public Terminal(T backend)
        : this(backend, new TerminalOptions(new Viewport(backend.Size, ResizeBehavior.Auto)))
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Terminal{T}"/> class.
    /// </summary>
    /// <param name="backend">The <see cref="IBackend"/>.</param>
    /// <param name="options">The <see cref="TerminalOptions"/>.</param>
    public Terminal(T backend, TerminalOptions options)
    {
        Backend = backend;
        Viewport = options.Viewport;
        Buffers = new List<Buffer> { new(options.Viewport.Area), new(options.Viewport.Area), };
    }

    /// <inheritdoc cref="ITerminal{T}.Buffers"/>
    public List<Buffer> Buffers { get; }


    /// <inheritdoc cref="ITerminal{T}.IsCursorHidden"/>
    public bool IsCursorHidden { get; private set; }

    /// <inheritdoc cref="ITerminal{T}.Cursor"/>
    public CursorPosition Cursor
    {
        get => Backend.CursorPosition;
        set => Backend.CursorPosition = value;
    }

    /// <inheritdoc cref="ITerminal{T}.Frame"/>
    public Frame<T> Frame => new(this, null);

    /// <inheritdoc cref="ITerminal{T}.CurrentBuffer"/>
    public Buffer CurrentBuffer => Buffers[_current];

    /// <inheritdoc cref="ITerminal{T}.Viewport"/>
    public Viewport Viewport { get; private set; }

    /// <inheritdoc cref="ITerminal{T}.Size"/>
    public Rect Size => Backend.Size;

    /// <inheritdoc cref="ITerminal{T}.Backend"/>
    public T Backend { get; }

    /// <inheritdoc cref="ITerminal{T}.Flush"/>
    public void Flush()
    {
        var previousBuffer = Buffers[1 - _current];
        var currentBuffer = Buffers[_current];

        var updates = previousBuffer.Diff(currentBuffer);
        Backend.Draw(updates);
    }

    /// <inheritdoc cref="ITerminal{T}.Resize"/>
    public void Resize(Rect area)
    {
        Buffers[_current].Resize(area);
        Buffers[1 - _current].Resize(area);
        Viewport = Viewport with { Area = area };
        Clear();
    }

    /// <inheritdoc cref="ITerminal{T}.Autoresize"/>
    public void Autoresize()
    {
        if (Viewport.ResizeBehavior == ResizeBehavior.Auto)
        {
            var size = Size;
            if (size != Viewport.Area)
            {
                Resize(size);
            }
        }
    }

    /// <inheritdoc cref="ITerminal{T}.Clear"/>
    public void Clear()
    {
        Backend.Clear();
        Buffers[1 - _current].Reset();
    }

    /// <inheritdoc cref="ITerminal{T}.HideCursor"/>
    public void HideCursor()
    {
        Backend.HideCursor();
        IsCursorHidden = true;
    }

    /// <inheritdoc cref="ITerminal{T}.ShowCursor"/>
    public void ShowCursor()
    {
        Backend.ShowCursor();
        IsCursorHidden = false;
    }

    /// <inheritdoc cref="ITerminal{T}.Draw"/>
    public CompletedFrame Draw(Action<Frame<T>> draw)
    {
        // Autoresize - otherwise we get glitches if shrinking or potential desync between widgets
        // and the terminal (if growing), which may OOB.
        Autoresize();

        var frame = Frame;
        draw(frame);

        // We can't change the cursor position right away because we have to flush the frame to
        // stdout first. But we also can't keep the frame around, since it holds a &mut to
        // Terminal. Thus, we're taking the important data out of the Frame and dropping it.
        var cursorPosition = frame.CursorPosition;

        Flush();

        if (cursorPosition == null)
        {
            HideCursor();
        }
        else
        {
            ShowCursor();
            Cursor = cursorPosition.Value;
        }

        // Swap buffers
        Buffers[1 - _current].Reset();
        _current = 1 - _current;

        Backend.Flush();
        return new CompletedFrame(Buffers[1 - _current], Viewport.Area);
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    public void Dispose()
    {
        if (IsCursorHidden)
        {
            ShowCursor();
        }
    }
}
