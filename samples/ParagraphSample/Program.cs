using System.Text;
using Boto.Layouts;
using Boto.Styles;
using Boto.Terminals;
using Boto.Texts;
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
    frame.Render(new Block { Style = new() { Background = Color.White, Foreground = Color.Black } }, frame.Size);

    var chuncks = new Layout()
        .Direction(Direction.Vertical)
        .Margin(5)
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
        .Title(title, new() { AddModifier = Modifier.Bold })
        .Borders(Borders.All)
        .Style(new() { Background = Color.White, Foreground = Color.Black });

    frame.Render(
        new Paragraph(new(text.ToList()))
            .Style(new() { Background = Color.White, Foreground = Color.Black })
            .Block(createBlock("Left, no wrap"))
            .Alignment(Alignment.Left),
        chuncks[0]);

    frame.Render(
        new Paragraph()
            .Text(text.ToList())
            .Block(createBlock("Left, no wrap"))
            .Style(new(){ Background = Color.White, Foreground = Color.Black })
            .Alignment(Alignment.Left)
            .EnableTrim(), 
        chuncks[1]);
    
     frame.Render(
            new Paragraph()
                .Text(text.ToList())
                .Block(createBlock("Center, no wrap"))
                .Style(new(){ Background = Color.White, Foreground = Color.Black })
                .Alignment(Alignment.Center),
            chuncks[2]);

     frame.Render(
            new Paragraph()
                .Text(text.ToList())
                .Block(createBlock("Right, wrap"))
                .Style(new(){ Background = Color.White, Foreground = Color.Black })
                .Alignment(Alignment.Center)
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
