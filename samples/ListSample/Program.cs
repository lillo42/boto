using System.Text;
using Boto.Layouts;
using Boto.Styles;
using Boto.Terminals;
using Boto.Texts;
using Boto.Tutu;
using Boto.Widgets;
using Boto.Widgets.Extensions;
using ListSample;
using NodaTime;
using Tutu.Events;
using Tutu.Extensions;
using Tutu.Terminal;
using static Tutu.Commands.Events;
using static Tutu.Commands.Terminal;

Console.OutputEncoding = Encoding.UTF8;
var items = new StatefulList<(string, int)>();
var events = new List<(string, string)>();

items.Items.AddRange(new[]
{
    ("Item0", 1), ("Item1", 2), ("Item2", 1), ("Item3", 3), ("Item4", 1), ("Item5", 4), ("Item6", 1), ("Item7", 3),
    ("Item8", 1), ("Item9", 6), ("Item10", 1), ("Item11", 3), ("Item12", 1), ("Item13", 2), ("Item14", 1),
    ("Item15", 1), ("Item16", 4), ("Item17", 1), ("Item18", 5), ("Item19", 4), ("Item20", 1), ("Item21", 2),
    ("Item22", 1), ("Item23", 3), ("Item24", 1),
});

events.AddRange(new[]
{
    ("Event1", "INFO"), ("Event2", "INFO"), ("Event3", "CRITICAL"), ("Event4", "ERROR"), ("Event5", "INFO"),
    ("Event6", "INFO"), ("Event7", "WARNING"), ("Event8", "INFO"), ("Event9", "INFO"), ("Event10", "INFO"),
    ("Event11", "CRITICAL"), ("Event12", "INFO"), ("Event13", "INFO"), ("Event14", "INFO"), ("Event15", "INFO"),
    ("Event16", "INFO"), ("Event17", "ERROR"), ("Event18", "ERROR"), ("Event19", "INFO"), ("Event20", "INFO"),
    ("Event21", "WARNING"), ("Event22", "INFO"), ("Event23", "INFO"), ("Event24", "WARNING"), ("Event25", "INFO"),
    ("Event26", "INFO"),
});

SystemTerminal.Instance.EnableRawMode();

var stdout = Console.Out;
stdout.Execute(EnterAlternateScreen, EnableMouseCapture);

var terminal = new Terminal(new TutuBackend(stdout));

var error = string.Empty;
try
{
    RunApp(terminal, new App(items, events), Duration.FromMilliseconds(250));
}
catch (Exception ex)
{
    error = ex.ToString();
}

SystemTerminal.Instance.DisableRawMode();
stdout.Execute(LeaveAlternateScreen, DisableMouseCapture);

Console.WriteLine(error);

static void RunApp(Boto.Terminals.ITerminal terminal, App app, Duration tickRate)
{
    var lastTick = SystemClock.Instance.GetCurrentInstant();
    while (true)
    {
        terminal.Draw(x => Ui(x, app));

        var elaspse = SystemClock.Instance.GetCurrentInstant() - lastTick;
        var timeout = elaspse - tickRate;
        if (SystemEventReader.Instance.Poll(timeout))
        {
            var @event = SystemEventReader.Instance.Read();
            if (@event is Event.KeyEventEvent keyEvent)
            {
                if (keyEvent.Event.Code is KeyCode.CharKeyCode { Character: "q" })
                {
                    break;
                }

                if (keyEvent.Event is { Code: KeyCode.LeftKeyCode, Kind: KeyEventKind.Press })
                {
                    app.Items.Unselect();
                }

                if (keyEvent.Event is { Code: KeyCode.DownKeyCode, Kind: KeyEventKind.Press })
                {
                    app.Items.Next();
                }

                if (keyEvent.Event is { Code: KeyCode.UpKeyCode, Kind: KeyEventKind.Press })
                {
                    app.Items.Previous();
                }
            }
        }

        if (elaspse >= tickRate)
        {
            OnTick(app);
            lastTick = SystemClock.Instance.GetCurrentInstant();
        }
    }
}

static void Ui(Frame frame, App app)
{
    // Create two chunks with equal horizontal screen space
    var chunks = new Layout()
        .SetDirection(Direction.Horizontal)
        .AddConstraints(Constraints.Percentage(50), Constraints.Percentage(50))
        .Split(frame.Size);

    // Iterate through all elements in the `items` app and append some debug text to it.
    var items = app.Items.Items.Select(i =>
    {
        var lines = new List<Spans>(i.Item2 + 1) { i.Item1 };
        lines.AddRange(Enumerable.Repeat(
            new Spans(new List<Span>
            {
                new("Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    new() { AddModifier = Modifier.Italic })
            }), i.Item2));

        return new ListItem(lines) { Style = new() { Foreground = Color.Black, Background = Color.White } };
    }).ToList();

    // Create a List from all list items and highlight the currently selected one
    // We can now render the item list
    frame.Render(new List()
            .AddItems(items)
            .SetBlock(new Block()
                .SetTitle("List")
                .SetBorders(Borders.All))
            .SetHighlightStyle(new() { Background = Color.LightGreen, AddModifier = Modifier.Bold })
            .SetHighlightSymbol(">> "), chunks[0], app.Items.State);

    // Let's do the same for the events.
    // The event list doesn't have any state and only displays the current state of the list
    var events = app.Events.Select(x =>
    {
        var (@event, level) = x;
        // Colorcode the level depending on its type
        var s = level switch
        {
            "CRITICAL" => new Style { Foreground = Color.Red },
            "ERROR" => new Style { Foreground = Color.Magenta },
            "WARNING" => new Style { Foreground = Color.Yellow },
            "INFO" => new Style { Foreground = Color.Blue },
            _ => new Style()
        };

        // Add a example datetime and apply proper spacing between them
        var header = new Spans(new List<Span>
        {
            new($"{level}", s), " ", new("2020-01-01 10:00:00", new() { AddModifier = Modifier.Italic })
        });

        // The event gets its own line
        var log = new Spans(new List<Span> { @event });

        // Here several things happen:
        // 1. Add a `---` spacing line above the final list entry
        // 2. Add the Level + datetime
        // 3. Add a spacer line
        // 4. Add the actual event
        return new ListItem(new List<Spans> { new(new string('-', chunks[1].Width)), header, "", log });
    }).Reverse().ToList();

    frame.Render(new List()
        .AddItems(events)
        .SetBlock(new Block()
            .SetTitle("Event List")
            .SetBorders(Borders.All))
        .SetStartCorner(Corner.BottomLeft), chunks[1]);
}


static void OnTick(App app)
{
    var @event = app.Events[0];
    app.Events.RemoveAt(0);
    app.Events.Add(@event);
}


public record App(StatefulList<(string, int)> Items, List<(string, string)> Events);
