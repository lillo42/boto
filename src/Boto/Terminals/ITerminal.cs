using Boto.Layouts;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Terminals;

/// <summary>
/// Terminal abstraction.
/// </summary>
public interface ITerminal : IDisposable
{
    /// <summary>
    /// The backend.
    /// </summary>
    IBackend Backend { get; }

    /// <summary>
    /// Holds the results of the current and previous draw calls. The two are compared at the end
    /// of each draw pass to output the necessary updates to the terminal
    /// </summary>
    List<Buffer> Buffers { get; }

    /// <summary>
    /// The current buffer.
    /// </summary>
    Buffer CurrentBuffer { get; }

    /// <summary>
    /// 
    /// </summary>
    CursorPosition Cursor { get; set; }

    /// <summary>
    /// Get a Frame object which provides a consistent view into the terminal state for rendering.
    /// </summary>
    Frame Frame { get; }

    /// <summary>
    /// Whether the cursor is currently hidden
    /// </summary>
    bool IsCursorHidden { get; }

    /// <summary>
    /// Current size of the terminal.
    /// </summary>
    Rect Size { get; }

    /// <summary>
    /// The viewport of the terminal.
    /// </summary>
    Viewport Viewport { get; }

    /// <summary>
    /// Obtains a difference between the previous and the current buffer and passes it to the
    /// current backend for drawing.
    /// </summary>
    void Flush();

    /// <summary>
    /// Updates the Terminal so that internal buffers match the requested size. Requested size will
    /// be saved so the size can remain consistent when rendering.
    /// This leads to a full clear of the screen.
    /// </summary>
    /// <param name="area">The new area.</param>
    void Resize(Rect area);

    /// <summary>
    /// Queries the backend for size and resizes if it doesn't match the previous size.
    /// </summary>
    void Autoresize();

    /// <summary>
    /// Clear the terminal.
    /// </summary>
    void Clear();

    /// <summary>
    /// Show the cursor.
    /// </summary>
    void ShowCursor();

    /// <summary>
    /// Hide the cursor.
    /// </summary>
    void HideCursor();

    /// <summary>
    /// Synchronizes terminal size, calls the rendering closure, flushes the current internal state
    /// and prepares for the next draw call.
    /// </summary>
    /// <param name="draw">The action.</param>
    /// <returns>The <see cref="CompletedFrame"/>.</returns>
    CompletedFrame Draw(Action<Frame> draw);
}
