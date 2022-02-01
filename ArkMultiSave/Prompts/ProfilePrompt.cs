namespace ArkMultiSave.Prompts;

public static class ProfilePrompt
{
    public static void SetNamePrompt(this Profile profile, IEnumerable<string> takenNames)
    {
        profile.Name = Prompt.Input<string>("Enter profile name", validators: new[] { Val.UniqueName(takenNames) });
    }

    public static void AddMapPrompt(this Profile profile)
    {
        var existingMaps = profile.GameFolders.ToList().WithoutSavedArksLocal();
        existingMaps.Add(Const.TheIsland);
        var map = Prompt.Input<string>("Enter map", validators: new[] { Val.UniqueName(existingMaps, "Choose another map") });

        map = map.Replace(" ", "");
        var titleCasedMap = char.ToUpper(map[0]) + map[1..];
        titleCasedMap += Const.DefaultGameFolder;
        if (profile.GameFolders.Contains(titleCasedMap))
        {
            "This profile already has the specified map".WriteLine(ConsoleColor.Cyan);
            return;
        }

        profile.GameFolders.Add(titleCasedMap);
    }

    public static void RemoveMapPrompt(this Profile profile)
    {
        var map = Prompt.Select("Select map to remove", profile.GameFolders.WithoutSavedArksLocal());
        profile.GameFolders.Remove(map + Const.DefaultGameFolder);
    }
}
