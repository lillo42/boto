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

var app = new App
{
    Items =
    {
        new List<string> { "Row11", "Row12", "Row13" },
        new List<string> { "Row21", "Row22", "Row23" },
        new List<string> { "Row31", "Row32", "Row33" },
        new List<string> { "Row41", "Row42", "Row43" },
        new List<string> { "Row51", "Row52", "Row53" },
        // TODO: Review why isn't breaking line
        new List<string> { "Row61", "Row62\nTest", "Row63" },
        new List<string> { "Row71", "Row72", "Row73" },
        new List<string> { "Row81", "Row82", "Row83" },
        new List<string> { "Row91", "Row92", "Row93" },
        new List<string> { "Row101", "Row102", "Row103" },
        new List<string> { "Row111", "Row112", "Row113" },
        new List<string> { "Row121", "Row122", "Row123" },
        new List<string> { "Row131", "Row132", "Row133" },
        new List<string> { "Row141", "Row142", "Row143" },
        new List<string> { "Row151", "Row152", "Row153" },
        new List<string> { "Row161", "Row162", "Row163" },
        new List<string> { "Row171", "Row172", "Row173" },
        new List<string> { "Row181", "Row182", "Row183" },
        new List<string> { "Row191", "Row192", "Row193" },
    }
};

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

            if (@event is Event.KeyEventEvent { Event.Code: KeyCode.DownKeyCode, Event.Kind: KeyEventKind.Press })
            {
                app.Next();
            }

            if (@event is Event.KeyEventEvent { Event.Code: KeyCode.UpKeyCode, Event.Kind: KeyEventKind.Press })
            {
                app.Previous();
            }
        }

        if (elaspse >= tickRate)
        {
            lastTick = SystemClock.Instance.GetCurrentInstant();
        }
    }
}


static void Ui<T>(Frame<T> frame, App app)
    where T : class, IBackend
{
    var chunks = new Layout()
        .Margin(5)
        .AddConstraints(Constraints.Percentage(100))
        .Split(frame.Size);

    var headersCells = new List<string> { "Header1", "Header2", "Header3" }
        .Select(x => new Cell(x) { Style = new() { Foreground = Color.Red } })
        .ToList();

    var rows = app.Items.Select(item =>
    {
        var height = item.Max(x => x.Count(y => y == '\n')) + 1;
        return new Row(item.Select(x => new Cell(x))) { Height = height, BottomMargin = 1 };
    });

    frame.Render(
        new Table(rows.ToList())
            .Block(new Block()
                .Title("Table")
                .AllBorders())
            .HighlightStyle(new() { AddModifier = Modifier.Reversed })
            .HighlightSymbol(">> ")
            .Headers(new(headersCells)
            {
                Style = new() { Background = Color.Blue },
                Height = 1,
                BottomMargin = 1
            })
            .AddConstraints(
                Constraints.Percentage(50), 
                Constraints.Length(30), 
                Constraints.Min(10))
        , chunks[0], app.State);
}

public record App
{
    public TableState State { get; set; } = new();
    public List<List<string>> Items { get; } = new();

    public void Next()
    {
        if (State.Selected is { } selected && selected < Items.Count - 1)
        {
            State.Selected = selected + 1;
        }
        else
        {
            State.Selected = 0;
        }
    }

    public void Previous()
    {
        if (State.Selected is { } selected and > 0)
        {
            if (selected == 0)
            {
                State.Selected = Items.Count - 1;
            }
            else
            {
                State.Selected = selected - 1;
            }
        }
        else
        {
            State.Selected = 0;
        }
    }
}
