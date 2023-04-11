using Boto.Widget;

namespace ListSample;

public class StatefulList<T>
{
    public ListState State { get; } = new();
    public List<T> Items { get; } = new();

    public void Next()
    {
        var i = 0;
        if (State.Selected is { } selected && selected < Items.Count - 1)
        {
            i = selected + 1;
        }

        State.Selected = i;
    }

    public void Previous()
    {
        var i = 0;
        if (State.Selected is { } selected)
        {
            if (selected == 0)
            {
                i = Items.Count - 1;
            }
            else
            {
                i = selected - 1;
            }
        }

        State.Selected = i;
    }
    
    public void Unselect() 
        => State.Selected = null;
}
