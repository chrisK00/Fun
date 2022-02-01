using ArkMultiSave.Printers;

namespace ArkMultiSave;

public static class Init
{
    public static Config DataFolder()
    {
        if (!Directory.Exists(Const.Data)) Directory.CreateDirectory(Const.Data);

        Config config = null;
        if (!File.Exists(Const.Config))
        {
            config = new Config();
            var savedFolderPath = Prompt.Input<string>("Paste full path to folder containing ark saves",
                validators: new[] { Validators.MinLength(8) });
            config.SavedFolderPath = savedFolderPath.Trim();

            Json.ToFile(Const.Config, config);
        }
        else
        {
            config = Json.FromFile<Config>(Const.Config);
        }

        var result = config.Validate();
        if (result.Count > 0)
        {
            "INVALID CONFIG".WriteLine(ConsoleColor.Cyan);
            result.ToInlineList().WriteLine(ConsoleColor.Cyan);
            Env.StartInDefaultApp(Const.Config);
            Env.Exit();
        }

        return config;
    }

    public static ICollection<Profile> GetProfiles()
    {
        var profiles = Json.FromFile<ICollection<Profile>>(Const.Profiles);

        if (profiles != null) return profiles;

        var replaceProfiles = Prompt.Confirm("No Profiles exist, would you like to create new ones? (y/n)");
        if (!replaceProfiles) Environment.Exit(0);

        return new List<Profile>();
    }

    public static Profile GetActiveProfile(ICollection<Profile> profiles)
    {
        var activeProfile = profiles.FirstOrDefault(p => p.IsActive)!;
        if (activeProfile != null) return activeProfile;

        return NoActiveProfile(profiles);
    }

    private static Profile NoActiveProfile(ICollection<Profile> profiles)
    {
        bool setActiveProfile = false;
        Profile profile = null;

        if (profiles.Count > 0) setActiveProfile = Prompt.Confirm("No active profile found, set an existing one to active? (y/n)");

        if (setActiveProfile)
        {
            profile = Prompt.Select("Select profile", profiles, textSelector: p => p.Name);
            profile.IsActive = true;
        }
        else
        {
            profile = ProfileFactory.Default();
            profile.IsActive = true;
            profile.SetNamePrompt(profiles.Names());
            profiles.Add(profile);
        }

        var result = Json.ToFile(Const.Profiles, profiles);
        if (!result) "Failed to save profiles".WriteLine(ConsoleColor.Red);

        return profile;
    }
}