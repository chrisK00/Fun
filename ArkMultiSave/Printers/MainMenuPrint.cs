namespace ArkMultiSave.Printers;
public static class MainMenuPrint
{
    public static void Profiles(IEnumerable<Profile> profiles)
    {
        var table = new ConsoleTable(" Name", "Maps", "");

        Console.WriteLine();
        profiles.ForEach(p => table.AddRow($" {p.Name}", p.GameFolders.WithoutSavedArksLocal().ToInlineList(), p.IsActive ? "-ACTIVE-" : ""));

        table.Write(Format.Minimal);
    }

    public static void GameSaves(IEnumerable<GameSave> gameSaves) => ConsoleTable.From(gameSaves).Write(Format.MarkDown);

    public static void FileErrors(string message, IEnumerable<string> filesFailed)
    {
        $"\t{message}\t{filesFailed.ToInlineList()}\n".WriteLine(ConsoleColor.Cyan);
    }
}
