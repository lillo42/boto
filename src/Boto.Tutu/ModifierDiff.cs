using Boto.Styles;
using Tutu;
using static Tutu.Commands.Style;
using TutuModifier = Tutu.Style.Types.Attribute;

namespace Boto.Tutu;

internal readonly record struct ModifierDiff(Modifier From, Modifier To)
{
    public void Queue(IQueueExecutor queue)
    {
        // use tutu attribute
        var removed = From & ~To;
        if (removed.HasFlag(Modifier.Reversed))
        {
            queue.Enqueue(SetAttribute(TutuModifier.NoReverse));
        }

        if (removed.HasFlag(Modifier.Bold))
        {
            queue.Enqueue(SetAttribute(TutuModifier.NormalIntensity));
            if (To.HasFlag(Modifier.Dim))
            {
                queue.Enqueue(SetAttribute(TutuModifier.Dim));
            }
        }

        if (removed.HasFlag(Modifier.Italic))
        {
            queue.Enqueue(SetAttribute(TutuModifier.NoItalic));
        }
        
        if (removed.HasFlag(Modifier.Underlined))
        {
            queue.Enqueue(SetAttribute(TutuModifier.NoUnderline));
        }

        if (removed.HasFlag(Modifier.Dim))
        {
            queue.Enqueue(SetAttribute(TutuModifier.NormalIntensity));
        }

        if (removed.HasFlag(Modifier.CrossedOut))
        {
            queue.Enqueue(SetAttribute(TutuModifier.NotCrossedOut));
        }

        if (removed.HasFlag(Modifier.SlowBlink) || removed.HasFlag(Modifier.RapidBlink))
        {
            queue.Enqueue(SetAttribute(TutuModifier.NoBlink));
        }

        var added = To & ~From;
        if (added.HasFlag(Modifier.Reversed))
        {
            queue.Enqueue(SetAttribute(TutuModifier.Reverse));
        }
        
        if (added.HasFlag(Modifier.Bold))
        {
            queue.Enqueue(SetAttribute(TutuModifier.Bold));
        }
        
        if (added.HasFlag(Modifier.Italic))
        {
            queue.Enqueue(SetAttribute(TutuModifier.Italic));
        }
        
        if (added.HasFlag(Modifier.Underlined))
        {
            queue.Enqueue(SetAttribute(TutuModifier.Underlined));
        }

        if (added.HasFlag(Modifier.Dim))
        {
            queue.Enqueue(SetAttribute(TutuModifier.Dim));
        }
        
        if (added.HasFlag(Modifier.CrossedOut))
        {
            queue.Enqueue(SetAttribute(TutuModifier.CrossedOut));
        }
        
        if (added.HasFlag(Modifier.SlowBlink))
        {
            queue.Enqueue(SetAttribute(TutuModifier.SlowBlink));
        }
        
        if (added.HasFlag(Modifier.RapidBlink))
        {
            queue.Enqueue(SetAttribute(TutuModifier.RapidBlink));
        }
    }
}
