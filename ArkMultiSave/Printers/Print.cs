namespace ArkMultiSave.Printers;

public static class Print
{
    public static void WriteLine(this string text, ConsoleColor color = ConsoleColor.Yellow)
    {
        var oldColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = oldColor;
    }

    public static void PrintList(this IEnumerable<string> items)
    {
        var sb = new StringBuilder();
        items.ForEach(p => sb.Append('\t').AppendLine(p));

        sb.ToString().WriteLine();
    }
}