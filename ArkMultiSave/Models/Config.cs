namespace ArkMultiSave.Models;
public class Config
{
    public string SavedFolderPath { get; set; }

    public IReadOnlyCollection<string> Validate()
    {
        var errors = new List<string>();
        if (string.IsNullOrWhiteSpace(SavedFolderPath))
        {
            errors.Add(@"Missing path to folder containing ARK saves, fe: E:\SteamLibrary\steamapps\common\ARK\ShooterGame\Saved");
        }

        if (!Directory.Exists(SavedFolderPath)) errors.Add("This folder does not exist");

        return errors;
    }
}
