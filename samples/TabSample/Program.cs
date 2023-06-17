using System.Text;
using Boto.Layouts;
using Boto.Styles;
using Boto.Terminals;
using Boto.Texts;
using Boto.Tutu;
using Boto.Widgets;
using Boto.Widgets.Extensions;
using NodaTime;
using Tutu.Events;
using Tutu.Extensions;
using Tutu.Terminal;
using static Tutu.Commands.Cursor;
using static Tutu.Commands.Events;
using static Tutu.Commands.Terminal;

var app = new App { Title = new() { "Tab0", "Tab1", "Tab2", "Tab3" } };

Console.OutputEncoding = Encoding.UTF8;
var stdout = Console.Out;
SystemTerminal.Instance.EnableRawMode();
stdout.Execute(EnterAlternateScreen, EnableMouseCapture);

var terminal = new Terminal(new TutuBackend(stdout));

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

static void RunApp(Boto.Terminals.ITerminal terminal, App app, Duration tickRate)
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

            if (@event is Event.KeyEventEvent { Event.Code: KeyCode.RightKeyCode, Event.Kind: KeyEventKind.Press })
            {
                app.Next();
            }

            if (@event is Event.KeyEventEvent { Event.Code: KeyCode.LeftKeyCode, Event.Kind: KeyEventKind.Press })
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


static void Ui(Frame frame, App app)
{
    var chunks = new Layout()
        .SetDirection(Direction.Vertical)
        .SetMargin(5)
        .AddConstraints(Constraints.Length(3), Constraints.Min(0))
        .Split(frame.Size);

    frame.Render(new Block { Style = new() { Background = Color.White, Foreground = Color.Black } },
        frame.Size);

    var titles = app.Title.Select(title =>
    {
        var (first, rest) = (title[0].ToString(), title[1..]);
        return new Spans(new List<Span>
        {
            new(first, new() { Foreground = Color.Yellow }), new(rest, new() { Foreground = Color.Green })
        });
    }).ToList();

    frame.Render(new Tabs(titles)
            .SetBlock(new Block()
                .SetTitle("Tabs") 
                .SetBorders(Borders.All))
            .SetSelected(app.Index)
            .SetStyle(new() { Foreground = Color.Cyan })
            .SetHighlightStyle(new() { AddModifier = Modifier.Bold, Background = Color.Black }),
        chunks[0]);

    frame.Render(new Block { Title = $"Inner {app.Index}", Borders = Borders.All }, chunks[1]);
}

public record App
{
    public List<string> Title { get; set; } = new();
    public int Index { get; set; }

    public void Next()
    {
        Index = (Index + 1) % Title.Count;
    }

    public void Previous()
    {
        Index = (Index > 0 ? Index : Title.Count) - 1;
    }
}
