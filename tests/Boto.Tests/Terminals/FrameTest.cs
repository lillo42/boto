using Boto.Layouts;
using Boto.Terminals;
using Boto.Widgets;
using FluentAssertions;
using NSubstitute;

namespace Boto.Tests.Terminals;

public class FrameTest
{
    [Fact]
    public void Size_Should_Return_TerminalViewportArea()
    {
        var terminal = Substitute.For<ITerminal>();
        var frame = new Frame(terminal, null);

        var area = new Rect(0, 0, 10, 10);
        terminal.Viewport.Returns(new Viewport(area, ResizeBehavior.Auto));
        frame.Size.Should().Be(area);
    }

    [Fact]
    public void CursorPosition_Should_Set_CursorPosition()
    {
        var terminal = Substitute.For<ITerminal>();
        var frame = new Frame(terminal, new CursorPosition(1, 1));

        frame.CursorPosition.Should().NotBeNull();
        frame.CursorPosition = new CursorPosition(2, 2);

        terminal
            .Received()
            .Cursor = new CursorPosition(2, 2);
    }

    [Fact]
    public void RenderWidget_Should_CallWidgetRender()
    {
        var terminal = Substitute.For<ITerminal>();
        var frame = new Frame(terminal, null);
        var widget = Substitute.For<IWidget>();
        var area = new Rect(0, 0, 10, 10);

        frame.Render(widget, area);

        widget
            .Received()
            .Render(area, terminal.CurrentBuffer);
    }

    [Fact]
    public void RenderWidgetState_Should_CallWidgetRender()
    {
        var terminal = Substitute.For<ITerminal>();
        var frame = new Frame(terminal, null);
        var widget = Substitute.For<IStateWidget<object>>();
        var area = new Rect(0, 0, 10, 10);

        var state = new object();
        frame.Render(widget, area, state);

        widget
            .Received()
            .Render(area, terminal.CurrentBuffer, state);
    }
}
