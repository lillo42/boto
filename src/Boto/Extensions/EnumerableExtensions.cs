namespace Boto.Extensions;

internal static class EnumerableExtensions
{
    public static IEnumerable<T[]> Windows<T>(this IEnumerable<T> source, int size)
    {
        var queue = new Queue<T>(size);
        foreach (var item in source)
        {
            queue.Enqueue(item);
            if (queue.Count >= size)
            {
                yield return queue.ToArray();
                queue.Dequeue();
            }
        }
    }
    
    public static IEnumerable<(int, T)> WithIndex<T>(this IEnumerable<T> source, int? startAt = null)
    {
        var index = startAt ?? 0;
        foreach (var item in source)
        {
            yield return (index++, item);
        }
    }

    public static IEnumerable<T> StepBy<T>(this IEnumerable<T> source, int step)
    {
        var index = 0;
        foreach (var item in source)
        {
            if (index % step == 0)
            {
                yield return item;
            }

            index++;
        }
    }
}
