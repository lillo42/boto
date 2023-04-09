using System.Text;
using Boto.Layouts;
using Boto.Styles;
using Boto.Terminals;
using Boto.Tutu;
using Boto.Widget;
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

var backend = new TutuBackend(stdout);
var terminal = new Terminal<TutuBackend>(backend);

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


static void RunApp<T>(ITerminal<T> terminal)
    where T : class, IBackend
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


static void Ui<T>(Frame<T> frame)
    where T : class, IBackend
{
    // Wrapping block for a group
    // Just draw the block and the group on the same area and build the group
    // with at least a margin of 1
    var size = frame.Size;

    // Surrounding block

    frame.Render(new Block()
        .Title("Main block with round corners")
        .CenterTitleAlignment()
        .RoundedBorderType(), size);

    var chunks = new Layout()
        .VerticalDirection()
        .Margin(4)
        .AddConstraints(Constraints.Percentage(50), Constraints.Percentage(50))
        .Split(size);

    // Top two inner blocks
    var topChunks = new Layout()
        .HorizontalDirection()
        .AddConstraints(Constraints.Percentage(50), Constraints.Percentage(50))
        .Split(chunks[0]);


    // Top left inner block with green background
    frame.Render(new Block()
            .Title(
                new("With ", new() { Foreground = Color.Yellow }),
                " background")
            .Style(new() { Background = Color.Green }),
        topChunks[0]);

    // Top right inner block with styled title aligned to the right
    frame.Render(new Block()
            .Title("Styled title",
                new Style { Foreground = Color.White, Background = Color.Red, AddModifier = Modifier.Bold })
            .TitleAlignment(Alignment.Right)
            .Style(new() { Background = Color.Blue }),
        topChunks[1]);


    // Bottom two inner blocks
    var bottomChunks = new Layout()
        .HorizontalDirection()
        .AddConstraints(Constraints.Percentage(50), Constraints.Percentage(50))
        .Split(chunks[1]);

    // Bottom left block with all default borders
    frame.Render(new Block()
        .Title("With borders")
        .AllBorders(), bottomChunks[0]);


    // Bottom right block with styled left and right border
    frame.Render(new Block()
            .Title("Styled boarders")
            .Borders(Borders.Left | Borders.Right)
            .DoubleBorderType()
            .BorderStyle(new() { Foreground = Color.Cyan }),
        bottomChunks[1]);
}
