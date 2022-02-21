namespace MemorySignal.Core.Helpers;

public static class Guard
{
    public static TItem Null<TItem, TId>(TItem item, TId id, string itemName = "item")
    {
        _ = item ?? throw new KeyNotFoundException($"{itemName} with id: '{id}' does not exist");
        return item;
    }

    public static IEnumerable<TItem> Empty<TItem, TId>(IEnumerable<TItem> items, TId id, string itemName = "item")
    {
        Null(items, id, itemName);
        return !items.Any() ? throw new KeyNotFoundException($"{itemName} with id: '{id}' is empty") : items;
    }
}