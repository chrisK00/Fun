namespace ArkMultiSave.Menus;

public class MainMenu
{
    private readonly ICollection<Profile> _profiles;
    private readonly Config _config;
    private readonly IFactory _factory;
    private readonly Dictionary<string, Action> _actions;
    private Profile _activeProfile;
    private ISaveFacade _save;

    public MainMenu(Profile activeProfile, ICollection<Profile> profiles, Config config, IFactory factory)
    {
        _activeProfile = activeProfile;
        _profiles = profiles;
        _config = config;
        _factory = factory;
        _save = factory.GetSaveFacade(activeProfile, config);

        _actions = new()
        {
            { "Load Profile", LoadProfile },
            { "List Profiles", () => MainMenuPrint.Profiles(_profiles) },
            { "List Games", () => MainMenuPrint.GameSaves(_save.GetActiveGames()) },
            { "Save To Profile", () => _save.SaveToProfile() },
            { "Add Map", AddMap },
            { "New Profile", NewProfile },
            { "Remove Map", RemoveMap },
            { "Remove Profile", RemoveProfile },
            { "Open Folder", () => Env.StartInExplorer(_config.SavedFolderPath) },
            { "Open Profiles", () => Env.StartInDefaultApp(Const.Profiles) },
        };
    }

    private void RemoveProfile()
    {
        if (_profiles.Count < 2) return;
        var profileToRemove = Prompt.Select("Select profile to remove", _profiles.Where(p => !p.IsActive), textSelector: p => p.Name);
        _profiles.Remove(profileToRemove);

        SaveProfilesToFile();
    }

    private void RemoveMap()
    {
        _activeProfile.RemoveMapPrompt();
        SaveProfilesToFile();
    }

    private void AddMap()
    {
        _activeProfile.AddMapPrompt();
        SaveProfilesToFile();
    }

    private void NewProfile()
    {
        var profile = Profile.CreateDefault();
        profile.SetNamePrompt(_profiles.Names());
        var addMap = Prompt.Confirm("Add a map (the island is added by default)? (y/n)");
        if (addMap) profile.AddMapPrompt();

        _profiles.Add(profile);

        var setToActive = Prompt.Confirm("Make new profile active? (y/n)");
        if (setToActive) SwapCurrentProfile(profile);
        else SaveProfilesToFile();
    }

    private void SwapCurrentProfile(Profile newProfile)
    {
        _save.SaveToProfile();
        PrintAnyErrors();

        _activeProfile.IsActive = false;
        newProfile.IsActive = true;
        _activeProfile = newProfile;

        _save = _factory.GetSaveFacade(_activeProfile, _config);
        _save.Load();

        PrintAnyErrors();
        SaveProfilesToFile();
    }

    public void Run()
    {
        while (true)
        {
            $"\tProfile: {_activeProfile.Name}".WriteLine(ConsoleColor.Cyan);
            var option = Prompt.Select("Select an option", _actions.Keys);
            _actions[option].Invoke();
            Console.WriteLine();
        }
    }

    private void LoadProfile()
    {
        var profileToLoad = Prompt.Select("Select profile", _profiles.Where(p => p.Name != _activeProfile.Name), textSelector: p => p.Name);
        SwapCurrentProfile(profileToLoad);
    }

    private void SaveProfilesToFile()
    {
        var result = Json.ToFile(Const.Profiles, _profiles);
        if (!result) "Failed to save profiles".WriteLine(ConsoleColor.Red);
    }

    public void PrintAnyErrors()
    {
        if (!_save.HasErrors) return;

        MainMenuPrint.FileErrors(_save.Message, _save.FilesFailed);
        _save.FilesFailed.Clear();
    }
}