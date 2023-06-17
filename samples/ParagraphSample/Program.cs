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
    frame.Render(new Block { Style = new() { Background = Color.White, Foreground = Color.Black } }, frame.Size);

    var chuncks = new Layout()
        .SetDirection(Direction.Vertical)
        .SetMargin(5)
        .AddConstraints(
            Constraints.Percentage(25), 
            Constraints.Percentage(25), 
            Constraints.Percentage(25),
            Constraints.Percentage(25))
        .Split(frame.Size);


    // Words made "loooong" to demonstrate line breaking.
    var s = "Veeeeeeeeeeeeeeeery    loooooooooooooooooong   striiiiiiiiiiiiiiiiiiiiiiiiiing.   ";
    var sb = new StringBuilder();
    for (var i = 0; i < frame.Size.Width / s.Length; i++)
    {
        sb.Append(s);
    }

    sb.Append('\n');

    var text = new List<Spans>
    {
        "This is a line ",
        new(new Span("This is a line ", new() { Foreground = Color.Red })),
        new(new Span("This is a line", new() { Background = Color.Blue })),
        new(new Span("This is a longer line", new() { AddModifier = Modifier.CrossedOut })),
        new(new Span(sb.ToString(), new() { Background = Color.Green })),
        new(new Span("This is a line ", new() { Foreground = Color.Green, AddModifier = Modifier.Italic }))
    };

    var createBlock = (string title) => new Block()
        .SetTitle(title, new() { AddModifier = Modifier.Bold })
        .SetBorders(Borders.All)
        .SetStyle(new() { Background = Color.White, Foreground = Color.Black });

    frame.Render(
        new Paragraph(new(text.ToList()))
            .SetStyle(new() { Background = Color.White, Foreground = Color.Black })
            .SetBlock(createBlock("Left, no wrap"))
            .SetAlignment(Alignment.Left),
        chuncks[0]);

    frame.Render(
        new Paragraph()
            .SetText(text.ToList())
            .SetBlock(createBlock("Left, no wrap"))
            .SetStyle(new(){ Background = Color.White, Foreground = Color.Black })
            .SetAlignment(Alignment.Left)
            .EnableTrim(), 
        chuncks[1]);
    
     frame.Render(
            new Paragraph()
                .SetText(text.ToList())
                .SetBlock(createBlock("Center, no wrap"))
                .SetStyle(new(){ Background = Color.White, Foreground = Color.Black })
                .SetAlignment(Alignment.Center),
            chuncks[2]);

     frame.Render(
            new Paragraph()
                .SetText(text.ToList())
                .SetBlock(createBlock("Right, wrap"))
                .SetStyle(new(){ Background = Color.White, Foreground = Color.Black })
                .SetAlignment(Alignment.Center)
                .EnableTrim(), 
            chuncks[3]);
}

public record App
{
    public void OnTick()
    {
        Scroll += 1;
        Scroll %= 10;
    }

    public int Scroll { get; set; }
}
