namespace ArkMultiSave.Extensions;
public static class EnumerableExtensions
{
    public static string ToInlineList(this IEnumerable<string> items) => $" [ {string.Join(", ", items)} ] ";
}
