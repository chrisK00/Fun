namespace ArkMultiSave.Models;

public class Profile
{
    public string Name { get; set; }
    public List<string> GameFolders { get; set; } = new();
    public bool IsActive { get; set; }

    public static Profile CreateDefault()
    {
        var profile = new Profile();
        profile.GameFolders.Add(Const.DefaultGameFolder);

        return profile;
    }
}
