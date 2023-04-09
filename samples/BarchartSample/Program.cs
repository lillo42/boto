using System.Text;
using Boto.Layouts;
using Boto.Styles;
using Boto.Terminals;
using Boto.Tutu;
using Boto.Widget;
using NodaTime;
using Tutu.Events;
using Tutu.Extensions;
using Tutu.Terminal;
using static Tutu.Commands.Cursor;
using static Tutu.Commands.Events;
using static Tutu.Commands.Terminal;

Console.OutputEncoding = Encoding.UTF8;

var app = new App(new()
{
    ("B1", 9),
    ("B2", 12),
    ("B3", 5),
    ("B4", 8),
    ("B5", 2),
    ("B6", 4),
    ("B7", 5),
    ("B8", 9),
    ("B9", 14),
    ("B10", 15),
    ("B11", 1),
    ("B12", 0),
    ("B13", 4),
    ("B14", 6),
    ("B15", 4),
    ("B16", 6),
    ("B17", 4),
    ("B18", 7),
    ("B19", 13),
    ("B20", 8),
    ("B21", 11),
    ("B22", 9),
    ("B23", 3),
    ("B24", 5)
});

Console.OutputEncoding = Encoding.UTF8;

var stdout = Console.Out;
SystemTerminal.Instance.EnableRawMode();
stdout.Execute(EnterAlternateScreen, EnableMouseCapture);

var backend = new TutuBackend(stdout);
var terminal = new Terminal<TutuBackend>(backend);

var error = string.Empty;
try
{
    RunApp(terminal, app, Duration.FromMilliseconds(250));
}
catch (Exception e)
{
    error = e.ToString();
}

SystemTerminal.Instance.DisableRawMode();
stdout.Execute(LeaveAlternateScreen, DisableMouseCapture, Show);
Console.WriteLine(error);

static void RunApp<T>(ITerminal<T> terminal, App app, Duration tickRate)
    where T : class, IBackend
{
    var lastTick = SystemClock.Instance.GetCurrentInstant();
    while (true)
    {
        terminal.Draw(frame => Ui(frame, app));

        var elaspse = SystemClock.Instance.GetCurrentInstant() - lastTick;
        var timeout = elaspse - tickRate;
        if (SystemEventReader.Instance.Poll(timeout))
        {
            var @event = SystemEventReader.Instance.Read();
            if (@event is Event.KeyEventEvent { Event.Code: KeyCode.CharKeyCode { Character: "q" } })
            {
                break;
            }
        }

        if (elaspse >= tickRate)
        {
            app.OnTick();
            lastTick = SystemClock.Instance.GetCurrentInstant();
        }
    }
}


static void Ui<T>(Frame<T> frame, App app)
    where T : class, IBackend
{
    var chunks = new Layout() 
        .VerticalDirection()
        .Margin(2)
        .AddConstraints(Constraints.Percentage(50), Constraints.Percentage(50))
        .Split(frame.Size);

    frame.Render(new BarChart()
            .Block(new Block()
                .Title("Data1")
                .Borders(Borders.All))
            .AddItems(app.Data)
            .BarWidth(9)
            .BarStyle(new() { Foreground = Color.Yellow })
            .ValueStyle(new() { Foreground = Color.Black, Background = Color.Yellow }),
        chunks[0]);

    chunks = new Layout
    {
        Direction = Direction.Horizontal,
        Constraints = new() { Constraints.Percentage(50), Constraints.Percentage(50) }
    }.Split(chunks[1]);

    frame.Render(new BarChart()
            .Block(new Block()
                .Title("Data2")
                .Borders(Borders.All))
            .AddItems(app.Data)
            .BarWidth(5)
            .BarGap(3)
            .BarStyle(new() { Foreground = Color.Green })
            .ValueStyle(new() { Foreground = Color.Green, AddModifier = Modifier.Bold }),
        chunks[0]);

    frame.Render(new BarChart()
            .Block(new Block()
                .Title("Data3")
                .Borders(Borders.All))
            .AddItems(app.Data)
            .BarWidth(7)
            .BarGap(0)
            .BarStyle(new() { Foreground = Color.Red })
            .ValueStyle(new() { Foreground = Color.Red })
            .LabelStyle(new() { Foreground = Color.Cyan, AddModifier = Modifier.Italic }),
        chunks[1]);
}

public record App(List<(string, int)> Data)
{
    public void OnTick()
    {
        var value = Data[^1];
        Data.RemoveAt(Data.Count - 1);
        Data.Insert(0, value);
    }
}
