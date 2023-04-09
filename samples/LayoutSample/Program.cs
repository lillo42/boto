using System.Text;
using Boto.Layouts;
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
    var chunks = new Layout()
        .Direction(Direction.Vertical)
        .AddConstraints(
            Constraints.Percentage(10),
            Constraints.Percentage(80),
            Constraints.Percentage(10))
        .Split(frame.Size);

    frame.Render(new Block { Title = "Block", Borders = Borders.All }, chunks[0]);
    frame.Render(new Block { Title = "Block2", Borders = Borders.All }, chunks[2]);
}
