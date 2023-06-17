using System.Collections;
using System.Globalization;
using System.Text;
using Boto.Layouts;
using Boto.Styles;
using Boto.Symbols;
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
using Block = Boto.Widgets.Block;

var app = new App
{
    Signal1 = new() { Interval = 0.2, Period = 3, Scale = 18 },
    Signal2 = new() { Interval = 0.1, Period = 2, Scale = 10 },
};

app.Data1 = app.Signal1.Take(200).ToList();
app.Data2 = app.Signal2.Take(200).ToList();

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
        .AddConstraints(
            Constraints.Ratio(1, 3),
            Constraints.Ratio(1, 3),
            Constraints.Ratio(1, 3))
        .Split(frame.Size);

    var xLabels = new List<Span>
    {
        new(app.Window[0].ToString(CultureInfo.InvariantCulture), new() { AddModifier = Modifier.Bold }),
        new(((app.Window[0] + app.Window[1]) / 2.0).ToString(CultureInfo.InvariantCulture)),
        new(app.Window[1].ToString(CultureInfo.InvariantCulture), new() { AddModifier = Modifier.Bold }),
    };

    var datasets = new List<Dataset>
    {
        new Dataset()
            .SetName("data2")
            .SetMarker(Marker.Dot)
            .SetStyle(new() { Foreground = Color.Cyan })
            .SetData(app.Data1),
        new Dataset()
            .SetName("data3")
            .SetMarker(Marker.Braille)
            .SetStyle(new() { Foreground = Color.Yellow })
            .SetData(app.Data2),
    };

    frame.Render(new Chart(datasets)
            .SetBlock(new Block()
                .SetTitle("Chart 1", new() { Foreground = Color.Cyan, AddModifier = Modifier.Bold })
                .SetBorders(Borders.All))
            .SetXAxis(new Axis()
                .SetTitle("X Axis")
                .SetStyle(new() { Foreground = Color.Gray })
                .AddLabels(xLabels)
                .SetBounds(app.Window))
            .YAxis(new Axis()
                .SetTitle("Y Axis")
                .SetStyle(new() { Foreground = Color.Gray })
                .SetBounds(-20, 20)
                .AddLabels(
                    new Span("-20", new() { AddModifier = Modifier.Bold }),
                    new Span("0"),
                    new Span("20", new() { AddModifier = Modifier.Bold }))),
        chunks[0]);

    datasets = new List<Dataset>
    {
        new Dataset()
            .SetName("data")
            .SetMarker(Marker.Braille)
            .SetStyle(new() { Foreground = Color.Yellow })
            .SetGraphType(GraphType.Line)
            .SetData(new[] { (0.0, 0.0), (1.0, 1.0), (2.0, 2.0), (3.0, 3.0), (4.0, 4.0) }),
    };

    frame.Render(new Chart()
            .SetDataset(datasets)
            .SetBlock(new Block()
                .SetTitle("Chart 2", new() { Foreground = Color.Cyan, AddModifier = Modifier.Bold })
                .SetBorders(Borders.All))
            .SetXAxis(new Axis()
                .SetTitle("X Axis")
                .SetStyle(new() { Foreground = Color.Gray })
                .AddLabels(
                    new Span("0", new Style { AddModifier = Modifier.Bold }),
                    new Span("2.5"),
                    new Span("5.0", new Style { AddModifier = Modifier.Bold }))
                .SetBounds(0, 5))
            .YAxis(new Axis()
                .SetTitle("Y Axis")
                .SetStyle(new() { Foreground = Color.Gray })
                .SetBounds(0, 5)
                .AddLabels(
                    new Span("0", new() { AddModifier = Modifier.Bold }),
                    new Span("2.5"),
                    new Span("5.0", new() { AddModifier = Modifier.Bold }))),
        chunks[1]);

    datasets = new List<Dataset>
    {
        new Dataset()
            .SetName("data")
            .SetMarker(Marker.Braille)
            .SetStyle(new() { Foreground = Color.Yellow })
            .SetGraphType(GraphType.Line)
            .SetData(new[] { (0.0, 0.0), (10.0, 1.0), (20.0, 0.5), (30.0, 1.5), (40.0, 1.0), (50.0, 2.5), (60.0, 3.0) }),
    };

    frame.Render(new Chart()
            .SetDataset(datasets)
            .SetBlock(new Block()
                .SetTitle("Chart 3", new() { Foreground = Color.Cyan, AddModifier = Modifier.Bold })
                .SetBorders(Borders.All))
            .SetXAxis(new Axis()
                .SetTitle("X Axis")
                .SetStyle(new() { Foreground = Color.Gray })
                .AddLabels(
                    new Span("0", new Style { AddModifier = Modifier.Bold }),
                    new Span("25"),
                    new Span("50", new Style { AddModifier = Modifier.Bold }))
                .SetBounds(0, 50))
            .YAxis(new Axis()
                .SetTitle("Y Axis")
                .SetStyle(new() { Foreground = Color.Gray })
                .SetBounds(0, 5)
                .AddLabels(
                    new Span("0", new() { AddModifier = Modifier.Bold }),
                    new Span("2.5"),
                    new Span("5.0", new() { AddModifier = Modifier.Bold }))),
        chunks[2]);
}

public record App
{
    public SinSignal Signal1 { get; set; } = new();
    public SinSignal Signal2 { get; set; } = new();
    public List<(double, double)> Data1 { get; set; } = new();
    public List<(double, double)> Data2 { get; set; } = new();
    public double[] Window { get; set; } = new double[] { 0, 20 };

    public void OnTick()
    {
        for (var i = 0; i < 5; i++)
        {
            Data1.RemoveAt(0);
        }

        Data1.AddRange(Signal1.Take(5));
        for (var i = 0; i < 10; i++)
        {
            Data2.RemoveAt(0);
        }

        Data2.AddRange(Signal2.Take(10));
        Window[0] += 1;
        Window[1] += 1;
    }
}

public record SinSignal : IEnumerable<(double, double)>
{
    public double X { get; set; }
    public double Interval { get; set; }
    public double Period { get; set; }
    public double Scale { get; set; }

    public IEnumerator<(double, double)> GetEnumerator()
    {
        while (true)
        {
            var point = (X, Math.Sin(X * 1 / Period) * Scale);
            X += Interval;
            yield return point;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
