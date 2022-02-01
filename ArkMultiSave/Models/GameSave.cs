namespace ArkMultiSave.Models;

public class GameSave
{
    public string Map { get; }
    public DateTime LastWrite { get; }
    public DateTime Created { get; }

    public GameSave(string map, DateTime created, DateTime lastWrite)
    {
        Map = map == Const.DefaultGameFolder ? $"{Const.DefaultGameFolder} ({Const.TheIsland})" : map.WithoutSavedArksLocal();
        Created = created;
        LastWrite = lastWrite;
    }
}