using Boto.Buffers;
using Boto.Layouts;

namespace Boto.Terminals;

/// <summary>
/// The framework/lib responsible for drawing the content to the terminal.
/// </summary>
public interface IBackend
{
    /// <summary>
    /// Draw the content to the terminal.
    /// </summary>
    /// <param name="content">The content.</param>
    void Draw(IEnumerable<BufferDiff> content);

    /// <summary>
    /// Hide the cursor.
    /// </summary>
    void HideCursor();

    /// <summary>
    /// Show the cursor.
    /// </summary>
    void ShowCursor();

    /// <summary>
    /// Clear the terminal.
    /// </summary>
    void Clear();

    /// <summary>
    /// Flush the terminal info.
    /// </summary>
    void Flush();

    /// <summary>
    /// Current terminal size.
    /// </summary>
    Rect Size { get; }

    /// <summary>
    /// Current cursor position.
    /// </summary>
    CursorPosition CursorPosition { get; set; }
}

/// <summary>
/// The cursor position.
/// </summary>
/// <param name="Row">Cursor row.</param>
/// <param name="Column">Cursor column.</param>
public readonly record struct CursorPosition(int Row, int Column);
