namespace ArkMultiSave.Interfaces;

public interface ISaveFacade
{
    List<string> FilesFailed { get; set; }
    string Message { get; set; }
    bool HasErrors { get; }

    IEnumerable<GameSave> GetActiveGames();

    /// <summary>
    /// Makes the current profile games active
    /// </summary>
    void Load();

    /// <summary>
    /// Moves the current game and settings to folders with the profile name
    /// </summary>
    void SaveToProfile();
}