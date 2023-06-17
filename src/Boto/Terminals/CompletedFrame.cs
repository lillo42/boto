using Boto.Layouts;
using Buffer = Boto.Buffers.Buffer;

namespace Boto.Terminals;

/// <summary>
/// CompletedFrame represents the state of the terminal after all changes performed in the last
/// <see cref="Terminal.Draw"/> call have been applied. Therefore, it is only valid until the next call to
/// <see cref="Terminal.Draw"/>.
/// </summary>
/// <param name="Buffer">The <see cref="Buffers.Buffer"/>.</param>
/// <param name="Area">The <see cref="Rect"/>.</param>
public record CompletedFrame(Buffer Buffer, Rect Area);
