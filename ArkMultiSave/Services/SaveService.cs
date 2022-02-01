namespace ArkMultiSave.Services;

public class SaveService : ISaveService
{
    public bool MoveTo(string source, string dest)
    {
        if (!Exists(source) || Exists(dest)) return false;
        Directory.Move(source, dest);

        return true;
    }

    public IEnumerable<GameSave> GetSaves(string folderPath, string searchPattern)
    {
        var saves = new List<GameSave>();

        Directory.GetDirectories(folderPath, searchPattern)
            .ForEach(f => saves.Add(new GameSave(Path.GetFileName(f), Directory.GetCreationTime(f), Directory.GetLastWriteTime(f))));

        return saves;
    }

    public bool Exists(string path) => Directory.Exists(path);
}