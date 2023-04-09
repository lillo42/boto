namespace Boto.Buffers;

/// <summary>
/// Holds the difference between two buffers
/// </summary>
/// <param name="Row">The row.</param>
/// <param name="Column">The column.</param>
/// <param name="Cell">The <see cref="Boto.Buffers.Cell"/>.</param>
public readonly record struct BufferDiff(int Row, int Column, Cell Cell);
