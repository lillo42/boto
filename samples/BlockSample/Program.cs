using System.Text;
using Boto.Layouts;
using Boto.Styles;
using Boto.Terminals;
using Boto.Tutu;
using Boto.Widgets;
using Boto.Widgets.Extensions;
using Tutu.Events;
using Tutu.Extensions;
using Tutu.Terminal;
using static Tutu.Commands.Cursor;
using static Tutu.Commands.Events;
using static Tutu.Commands.Terminal;

Console.OutputEncoding = Encoding.UTF8;
var stdout = Console.Out;
SystemTerminal.Instance.EnableRawMode();
stdout.Execute(EnterAlternateScreen, EnableMouseCapture);

var terminal = new Terminal(new TutuBackend(stdout));

var error = string.Empty;
try
{
    RunApp(terminal);
}
catch (Exception e)
{
    error = e.ToString();
}

SystemTerminal.Instance.DisableRawMode();
stdout.Execute(LeaveAlternateScreen, DisableMouseCapture, Show);
Console.WriteLine(error);


static void RunApp(Boto.Terminals.ITerminal terminal)
{
    while (true)
    {
        terminal.Draw(Ui);
        var @event = SystemEventReader.Instance.Read();
        if (@event is Event.KeyEventEvent { Event.Code: KeyCode.CharKeyCode { Character: "q" } })
        {
            break;
        }
    }
}


static void Ui(Frame frame)
{
    // Wrapping block for a group
    // Just draw the block and the group on the same area and build the group
    // with at least a margin of 1
    var size = frame.Size;

    // Surrounding block

    frame.Render(new Block()
        .SetTitle("Main block with round corners")
        .SetTitleAlignment(Alignment.Center)
        .SetBorderType(BorderType.Rounded), size);

    var chunks = new Layout()
        .SetDirection(Direction.Vertical)
        .SetMargin(4)
        .AddConstraints(Constraints.Percentage(50), Constraints.Percentage(50))
        .Split(size);

    // Top two inner blocks
    var topChunks = new Layout()
        .SetDirection(Direction.Horizontal)
        .AddConstraints(Constraints.Percentage(50), Constraints.Percentage(50))
        .Split(chunks[0]);


    // Top left inner block with green background
    frame.Render(new Block()
            .SetTitle(
                new("With ", new() { Foreground = Color.Yellow }),
                " background")
            .SetStyle(new() { Background = Color.Green }),
        topChunks[0]);

    // Top right inner block with styled title aligned to the right
    frame.Render(new Block()
            .SetTitle("Styled title",
                new Style { Foreground = Color.White, Background = Color.Red, AddModifier = Modifier.Bold })
            .SetTitleAlignment(Alignment.Right)
            .SetStyle(new() { Background = Color.Blue }),
        topChunks[1]);


    // Bottom two inner blocks
    var bottomChunks = new Layout()
        .SetDirection(Direction.Horizontal)
        .AddConstraints(Constraints.Percentage(50), Constraints.Percentage(50))
        .Split(chunks[1]);

    // Bottom left block with all default borders
    frame.Render(new Block()
        .SetTitle("With borders")
        .SetBorders(Borders.All), bottomChunks[0]);


    // Bottom right block with styled left and right border
    frame.Render(new Block()
            .SetTitle("Styled boarders")
            .SetBorders(Borders.Left | Borders.Right)
            .SetBorderType(BorderType.Double)
            .SetBorderStyle(new() { Foreground = Color.Cyan }),
        bottomChunks[1]);
}
