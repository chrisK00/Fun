namespace ArkMultiSave.Extensions;
public static class ProfilesExtensions
{
    public static IEnumerable<string> Names(this IEnumerable<Profile> profiles) => profiles.Select(p => p.Name);

    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
    {
        foreach (var item in items)
        {
            action.Invoke(item);
        }
    }

    public static List<string> WithoutSavedArksLocal(this IEnumerable<string> gameFolders)
    {
        var formattedFolders = new List<string>();
        gameFolders.ForEach(f => formattedFolders.Add(f.WithoutSavedArksLocal()));

        return formattedFolders;
    }

    public static string WithoutSavedArksLocal(this string folder)
    {
        if (folder == Const.DefaultGameFolder || !folder.Contains(Const.DefaultGameFolder)) return folder;

        return folder.Replace(Const.DefaultGameFolder, "");
    }
}
