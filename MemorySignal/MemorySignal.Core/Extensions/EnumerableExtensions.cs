namespace MemorySignal.Core.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
    {
        var rnd = new Random();

        return source
           .Select(item => new { order = rnd.Next(), item })
           .OrderBy(x => x.order)
           .Select(x => x.item)
           .ToArray();
    }
}