using System.Text;
using Boto.Layouts;
using Boto.Styles;
using Boto.Terminals;
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

var app = new App
{
    Progress1 = 0, Progress2 = 0, Progress3 = 0.45f, Progress4 = 0,
};

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
        }

        if (elaspse >= tickRate)
        {
            app.OnTick();
            lastTick = SystemClock.Instance.GetCurrentInstant();
        }
    }
}


static void Ui(Frame frame, App app)
{
    var chunks = new Layout()
        .SetDirection(Direction.Vertical)
        .SetMargin(2)
        .AddConstraints(Constraints.Percentage(25),
            Constraints.Percentage(25),
            Constraints.Percentage(25),
            Constraints.Percentage(25))
        .Split(frame.Size);

    frame.Render(new Gauge()
        .SetBlock(new Block().SetTitle("Gauge1").SetBorders(Borders.All))
        .SetGaugeStyle(new() { Foreground = Color.Yellow })
        .SetPercent(app.Progress1), chunks[0]);

    frame.Render(new Gauge()
        .SetBlock(new Block().SetTitle("Gauge2").SetBorders(Borders.All))
        .SetGaugeStyle(new() { Foreground = Color.Magenta, Background = Color.Green })
        .SetLabel($"{app.Progress2}/100")
        .SetPercent(app.Progress2), chunks[1]);

    frame.Render(new Gauge()
        .SetBlock(new Block().SetTitle("Gauge3").SetBorders(Borders.All))
        .SetGaugeStyle(new() { Foreground = Color.Yellow })
        .SetRatio(app.Progress3)
        .SetLabel($"{app.Progress3 * 100:00.00}%",
            new() { Foreground = Color.Red, AddModifier = Modifier.Italic | Modifier.Bold })
        .EnableUnicode(), chunks[2]);

    frame.Render(new Gauge()
        .SetBlock(new Block().SetTitle("Gauge4").SetBorders(Borders.All))
        .SetGaugeStyle(new() { Foreground = Color.Cyan, AddModifier = Modifier.Italic })
        .SetPercent(app.Progress4)
        .SetLabel($"{app.Progress4} / 100"), chunks[3]);
}

public record App
{
    public void OnTick()
    {
        Progress1 = (Progress1 + 1) % 100;
        Progress2 = (Progress2 + 2) % 100;
        Progress3 = (Progress3 + 0.001f) % 1;
        Progress4 = (Progress4 + 1) % 100;
    }

    public int Progress1 { get; set; }
    public int Progress2 { get; set; }
    public double Progress3 { get; set; }
    public int Progress4 { get; set; }
}
