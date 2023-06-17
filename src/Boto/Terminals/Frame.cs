using Boto.Layouts;
using Boto.Widgets;

namespace Boto.Terminals;

/// <summary>
/// A consistent terminal interface for rendering.
/// </summary>
/// <param name="Terminal">The <see cref="ITerminal"/>.</param>
public record Frame(ITerminal Terminal)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Frame"/> class.
    /// </summary>
    /// <param name="terminal">The <see cref="ITerminal"/>.</param>
    /// <param name="position">The cursor position.</param>
    public Frame(ITerminal terminal, CursorPosition? position)
        : this(terminal)
    {
        _cursorPosition = position;
    }
    
    /// <summary>
    /// Terminal size, guaranteed not to change when rendering.
    /// </summary>
    public Rect Size => Terminal.Viewport.Area;

    private CursorPosition? _cursorPosition;

    /// <summary>
    /// Where should the cursor be after drawing this frame?
    /// </summary>
    /// <remarks>
    /// If <see langword="null"/>, the cursor is hidden and its position is controlled by the backend. If `(x,
    /// y)`, the cursor is shown and placed at `(x, y)` after the call to <see cref="ITerminal.Draw"/>.
    /// </remarks>
    public CursorPosition? CursorPosition
    {
        get => _cursorPosition;
        set
        {
            _cursorPosition = value;
            if (_cursorPosition is { } position)
            {
                Terminal.Cursor = position;
            }
        }
    }
    
    /// <summary>
    /// Render a <see cref="IWidget"/> to the current buffer using <see cref="IWidget.Render"/>.
    /// </summary>
    /// <param name="widget">The <see cref="IWidget"/>.</param>
    /// <param name="area">The area to render.</param>
    /// <typeparam name="TWidget">The <see cref="IWidget"/>.</typeparam>
    public void Render<TWidget>(TWidget widget, Rect area)
        where TWidget : IWidget
        => widget.Render(area, Terminal.CurrentBuffer);

    /// <summary>
    /// Render a <see cref="IStateWidget{T}"/> to the current buffer using <see cref="IStateWidget{T}.Render"/>.
    /// </summary>
    /// <param name="widget">The <see cref="IStateWidget{T}"/>.</param>
    /// <param name="area">The area to render.</param>
    /// <param name="state">The current <see cref="IStateWidget{T}"/> state</param>
    /// <typeparam name="TWidget">The <see cref="IStateWidget{T}"/>.</typeparam>
    /// <typeparam name="TState">The <see cref="IStateWidget{T}"/> state.</typeparam>
    public void Render<TWidget, TState>(TWidget widget, Rect area, TState state)
        where TState : notnull
        where TWidget : IStateWidget<TState>
        => widget.Render(area, Terminal.CurrentBuffer, state);

}
