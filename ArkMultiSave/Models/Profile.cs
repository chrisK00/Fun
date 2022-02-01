namespace ArkMultiSave.Models;
public class Profile
{
    public string Name { get; set; }
    public List<string> GameFolders { get; set; } = new();
    public bool IsActive { get; set; }
}
