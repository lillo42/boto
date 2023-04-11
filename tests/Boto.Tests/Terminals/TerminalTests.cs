using Boto.Buffers;
using Boto.Layouts;
using Boto.Terminals;
using FluentAssertions;
using NSubstitute;

namespace Boto.Tests.Terminals;

public class TerminalTests
{
    [Fact]
    public void Buffers_Should_HaveCount2()
    {
        var backend = Substitute.For<IBackend>();
        backend.Size.Returns(new Rect(0, 0, 10, 10));

        var terminal = new Terminal(backend);
        terminal.Buffers.Should().HaveCount(2);
    }

    [Fact]
    public void CursorGetSet_Should_CallBackend()
    {
        var backend = Substitute.For<IBackend>();
        backend.Size.Returns(new Rect(0, 0, 10, 10));

        var terminal = new Terminal(backend);
        terminal.Cursor = new CursorPosition(1, 1);
        backend.Received().CursorPosition = new CursorPosition(1, 1);
    }

    [Fact]
    public void Frame_Should_CreateNewBuffer()
    {
        var backend = Substitute.For<IBackend>();
        backend.Size.Returns(new Rect(0, 0, 10, 10));

        var terminal = new Terminal(backend);
        var frame = terminal.Frame;
        frame.Terminal.Should().Be(terminal);
        frame.CursorPosition.Should().BeNull();
    }

    [Fact]
    public void HideCursor_Should_CallBackend()
    {
        var backend = Substitute.For<IBackend>();
        backend.Size.Returns(new Rect(0, 0, 10, 10));

        var terminal = new Terminal(backend);
        terminal.HideCursor();
        terminal.IsCursorHidden.Should().BeTrue();
        backend.Received().HideCursor();
    }

    [Fact]
    public void ShowCursor_Should_CallBackend()
    {
        var backend = Substitute.For<IBackend>();
        backend.Size.Returns(new Rect(0, 0, 10, 10));

        var terminal = new Terminal(backend);
        terminal.ShowCursor();
        terminal.IsCursorHidden.Should().BeFalse();
        backend.Received().ShowCursor();
    }

    [Fact]
    public void Clear_Should_CallBackend()
    {
        var backend = Substitute.For<IBackend>();
        backend.Size.Returns(new Rect(0, 0, 10, 10));

        var terminal = new Terminal(backend);
        terminal.Clear();
        backend.Received().Clear();
    }

    [Fact]
    public void Resize_Should_CallBuffersResize()
    {
        var backend = Substitute.For<IBackend>();
        backend.Size.Returns(new Rect(0, 0, 10, 10));

        var terminal = new Terminal(backend);
        terminal.Resize(new Rect(0, 0, 20, 20));

        backend.Received().Clear();
    }

    [Fact]
    public void Autoresize_Should_DoNothing_When_ViewportIsFixed()
    {
        var backend = Substitute.For<IBackend>();
        var terminal = new Terminal(backend, new TerminalOptions(Viewport.Fixed(new Rect(0, 0, 10, 10))));
        terminal.Autoresize();
        _ = backend.DidNotReceive().Size;
    }

    [Fact]
    public void Autoresize_Should_DoNothing_When_ViewportIsSameAsSize()
    {
        var backend = Substitute.For<IBackend>();
        backend.Size.Returns(new Rect(0, 0, 10, 10));

        var terminal = new Terminal(backend, new TerminalOptions(new(new(0, 0, 10, 10), ResizeBehavior.Auto)));
        terminal.Autoresize();
        _ = backend.Received(1).Size;
        backend.DidNotReceive().Clear();
    }

    [Fact]
    public void Autosize_Should_Resize_When_ViewportIsDiffThanSize()
    {
        var backend = Substitute.For<IBackend>();
        backend.Size.Returns(new Rect(0, 0, 10, 10));

        var terminal = new Terminal(backend, new TerminalOptions(new(new(0, 0, 20, 20), ResizeBehavior.Auto)));
        terminal.Autoresize();

        _ = backend.Received(1).Size;
        backend.Received().Clear();
    }

    [Fact]
    public void Flush_Should_CallBackendDraw()
    {
        var backend = Substitute.For<IBackend>();
        backend.Size.Returns(new Rect(0, 0, 10, 10));

        var terminal = new Terminal(backend);
        terminal.Flush();
        backend.Received().Draw(Arg.Any<IEnumerable<BufferDiff>>());
    }

    [Fact]
    public void Draw_Should_HideCursor_When_CursorPositionIsNull()
    {
        var backend = Substitute.For<IBackend>();
        backend.Size.Returns(new Rect(0, 0, 10, 10));

        var terminal = new Terminal(backend);
        var completed = terminal.Draw(_ => { });

        backend.Received().HideCursor();
        backend.Received().Flush();
        backend.DidNotReceive().ShowCursor();

        completed.Area.Should().Be(new Rect(0, 0, 10, 10));
    }
    
    [Fact]
    public void Draw_Should_ShowCursor_When_CursorPositionIsNotNull()
    {
        var backend = Substitute.For<IBackend>();
        backend.Size.Returns(new Rect(0, 0, 10, 10));

        var terminal = new Terminal(backend);
        var completed = terminal.Draw(x => x.CursorPosition = new CursorPosition(1, 1));

        backend.DidNotReceive().HideCursor();
        backend.Received().Flush();
        backend.Received().ShowCursor();

        completed.Area.Should().Be(new Rect(0, 0, 10, 10));
    }
}
