namespace MemorySignal.Core.Extensions;

public static class ReadonlyListExtensions
{
    public static int LastIndex<T>(this IReadOnlyCollection<T> source) => source.Count - 1;
}