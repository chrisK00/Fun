namespace ArkMultiSave.Services;

public class SaveFacade : ISaveFacade
{
    private readonly Profile _profile;
    private readonly Config _config;
    private readonly ISaveService _save;
    private readonly string _currentSettingsPath;
    public SaveFacade(Profile profile, Config config, ISaveService save)
    {
        _save = save;
        _profile = profile;
        _config = config;
        _currentSettingsPath = config.SavedFolderPath + Const.Slash + Const.ActiveSettingsFolder;
    }

    public List<string> FilesFailed { get; set; } = new();
    public string Message { get; set; }
    public bool HasErrors => FilesFailed.Count > 0;
    private string SettingsProfilePath => _currentSettingsPath + Const.Underscore + _profile.Name;

    public void SaveToProfile()
    {
        _profile.GameFolders.ForEach(f =>
        {
            var success = _save.MoveTo(CurrentGamePath(f), GameProfilePath(f));
            if (!success) FilesFailed.Add(f.WithoutSavedArksLocal());
        });

        if (HasErrors) Message = "Some games were not moved";
        _save.MoveTo(_currentSettingsPath, SettingsProfilePath);
    }

    public void Load()
    {
        _profile.GameFolders.ForEach(f =>
        {
            var success = _save.MoveTo(GameProfilePath(f), CurrentGamePath(f));
            if (!success) FilesFailed.Add(f.WithoutSavedArksLocal());
        });

        if (HasErrors) Message = "Some games were not loaded";
        _save.MoveTo(SettingsProfilePath, _currentSettingsPath);
    }

    public IEnumerable<GameSave> GetActiveGames() => _save.GetSaves(_config.SavedFolderPath, $"*{Const.DefaultGameFolder}");

    private string CurrentGamePath(string map) => _config.SavedFolderPath + Const.Slash + map;

    private string GameProfilePath(string map) => _config.SavedFolderPath + Const.Slash + map + Const.Underscore + _profile.Name;
}