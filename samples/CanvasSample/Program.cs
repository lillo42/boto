using System.Text;
using Boto.Layouts;
using Boto.Styles;
using Boto.Terminals;
using Boto.Tutu;
using Boto.Widget;
using Boto.Widget.Canvas;
using NodaTime;
using Tutu.Events;
using Tutu.Extensions;
using Tutu.Terminal;
using static Tutu.Commands.Cursor;
using static Tutu.Commands.Events;
using static Tutu.Commands.Terminal;

var app = new App
{
    X = 0,
    Y = 0,
    Ball = new Rectangle
    {
        X = 10,
        Y = 30,
        Width = 10,
        Height = 10,
        Color = Color.Yellow
    },
    Playground = new(10, 10, 100, 100),
    Vx = 1,
    Vy = 1,
    DirX = true,
    DirY = true
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
                app.Y += 1;
            }

            if (@event is Event.KeyEventEvent { Event.Code: KeyCode.UpKeyCode, Event.Kind: KeyEventKind.Press })
            {
                app.Y -= 1;
            }

            if (@event is Event.KeyEventEvent { Event.Code: KeyCode.RightKeyCode, Event.Kind: KeyEventKind.Press })
            {
                app.X += 1;
            }

            if (@event is Event.KeyEventEvent { Event.Code: KeyCode.LeftKeyCode, Event.Kind: KeyEventKind.Press })
            {
                app.X -= 1;
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
        .Direction(Direction.Horizontal)
        .AddConstraints(Constraints.Percentage(50), Constraints.Percentage(50))
        .Split(frame.Size);

    var canvas = new Canvas()
        .Block(new Block().Title("World").Borders(Borders.All))
        .XBounds(-180, 180)
        .YBounds(-90, 90);

    canvas.Painter = context =>
    {
        context.Draw(new Map { Color = Color.White, Resolution = MapResolution.High });
        context.Print(app.X, -app.Y, new("You are here", new Style() { Foreground = Color.Yellow }));
    };

    frame.Render(canvas, chunks[0]);


    canvas = new Canvas()
        .Block(new Block().Title("Pong").Borders(Borders.All))
        .XBounds(10, 110)
        .YBounds(10, 110);

    canvas.Painter = context => context.Draw(app.Ball);

    frame.Render(canvas, chunks[1]);
}

public record App
{
    public double X { get; set; }
    public double Y { get; set; }
    public Rectangle Ball { get; set; } = new();
    public Rect Playground { get; set; } = new();
    public double Vx { get; set; }
    public double Vy { get; set; }
    public bool DirX { get; set; }
    public bool DirY { get; set; }

    public void OnTick()
    {
        if (Ball.X < Playground.Left || Ball.X > Playground.Right)
        {
            DirX = !DirX;
        }

        if (Ball.Y < Playground.Top || Ball.Y + Ball.Height > Playground.Bottom)
        {
            DirY = !DirY;
        }

        if (DirX)
        {
            Ball.X += Vx;
        }
        else
        {
            Ball.X -= Vx;
        }


        if (DirY)
        {
            Ball.Y += Vy;
        }
        else
        {
            Ball.Y -= Vy;
        }
    }
}
