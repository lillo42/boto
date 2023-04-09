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

var app = new App();

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
    var chuncks = new Layout()
        .VerticalDirection()
        .AddConstraints(
            Constraints.Length(3),
            Constraints.Length(3),
            Constraints.Length(7),
            Constraints.Min(0)
        )
        .Split(frame.Size);

    frame.Render(new Sparkline()
            .Block(new Block()
                .Title("Data1")
                .Borders(Borders.Left | Borders.Right))
            .Style(new() { Foreground = Color.Yellow })
            .AddItems(app.Data1),
        chuncks[0]);

    frame.Render(new Sparkline()
            .Block(new Block()
                .Title("Data2")
                .Borders(Borders.Left | Borders.Right))
            .Style(new() { Foreground = Color.Green })
            .AddItems(app.Data2),
        chuncks[1]);

    frame.Render(new Sparkline()
            .Block(new Block()
                .Title("Data3")
                .Borders(Borders.Left | Borders.Right))
            .Style(new() { Foreground = Color.Red })
            .AddItems(app.Data3),
        chuncks[2]);
}

public record App
{
    public App()
    {
        for (var i = 0; i < 200; i++)
        {
            Data1.Add(_random.Next(0, 100));
            Data2.Add(_random.Next(0, 100));
            Data3.Add(_random.Next(0, 100));
        }
    }

    private readonly Random _random = new();
    public List<int> Data1 { get; } = new();
    public List<int> Data2 { get; } = new();
    public List<int> Data3 { get; } = new();

    public void OnTick()
    {
        var value = _random.Next(0, 100);
        Data1.RemoveAt(Data1.Count - 1);
        Data1.Insert(0, value);

        value = _random.Next(0, 100);
        Data2.RemoveAt(Data2.Count - 1);
        Data2.Insert(0, value);

        value = _random.Next(0, 100);
        Data3.RemoveAt(Data3.Count - 1);
        Data3.Insert(0, value);
    }
}
