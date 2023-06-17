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

var app = new App();

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

            if (@event is Event.KeyEventEvent { Event.Code: KeyCode.CharKeyCode { Character: "p" }, Event.Kind: KeyEventKind.Release })
            {
                app.ShowPopup = !app.ShowPopup;
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
    var chuncks = new Layout()
        .AddConstraints(Constraints.Percentage(20), Constraints.Percentage(80))
        .Split(frame.Size);

    var text = app.ShowPopup ? "Press p to close the popup" : "Press p to show the popup";
    frame.Render(new Paragraph()
            .SetText(text)
            .SetStyle(new() { AddModifier = Modifier.SlowBlink})
            .EnableTrim(), 
        chuncks[0]);

    frame.Render(new Block { Title = "Content", Borders = Borders.All, Style = new() { Background = Color.Blue } },
        chuncks[1]);

    if (app.ShowPopup)
    {
        var popupArea = CenteredRect(60, 20, frame.Size);
        frame.Render(new Clear(), popupArea);
        frame.Render(new Block { Title = "Popup", Borders = Borders.All, }, popupArea);
    }
}

static Rect CenteredRect(int percentX, int percentY, Rect area)
{
    var popup = new Layout
    {
        Direction = Direction.Vertical,
        Constraints = new List<IConstraint>
        {
            Constraints.Percentage((100 - percentY) / 2),
            Constraints.Percentage(percentY),
            Constraints.Percentage((100 - percentY) / 2),
        }
    }.Split(area);
    
    return new Layout
    {
        Direction = Direction.Horizontal,
        Constraints = new List<IConstraint>
        {
            Constraints.Percentage((100 - percentX) / 2),
            Constraints.Percentage(percentX),
            Constraints.Percentage((100 - percentX) / 2),
        }
    }.Split(popup[1])[1];
}

public record App
{
    public bool ShowPopup { get; set; }
}
