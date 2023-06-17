using Boto.Layouts;
using Boto.Widgets;
using FluentAssertions;
using Buffer = Boto.Buffers.Buffer;
using Cell = Boto.Buffers.Cell;

namespace Boto.Tests.Widgets;

public class ClearTest
{
    [Fact]
    public void Render()
    {
        var buffer = new Buffer(new Rect(0, 0, 10, 10));
        var clear = new Clear();
        clear.Render(new Rect(0, 0, 10, 10), buffer);

        for (var i = 0; i < 10; i++)
        {
            for(var j = 0; j < 10; j++)
            {
                buffer[i, j].Should().Be(new Cell());
            }
        }
    }
}
