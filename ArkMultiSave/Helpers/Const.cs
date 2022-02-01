namespace ArkMultiSave.Helpers;

public static class Const
{
    public const string DefaultGameFolder = "SavedArksLocal";
    public const string ActiveSettingsFolder = "SaveGames";
    public const string TheIsland = "TheIsland";
    public const string Underscore = "_";
    public const string Slash = "/";
    private static readonly string _appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public static string Data { get; } = Path.Combine(_appData, nameof(ArkMultiSave));
    public static string Profiles { get; } = Path.Combine(Data, "profiles.json");
    public static string Config { get; } = Path.Combine(Data, "config.json");
}