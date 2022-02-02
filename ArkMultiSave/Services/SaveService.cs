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
       return Directory.GetDirectories(folderPath, searchPattern)
                       .Select(f => new GameSave(Path.GetFileName(f), Directory.GetCreationTime(f), Directory.GetLastWriteTime(f)))
                       .ToArray();
    }

    public bool Exists(string path) => Directory.Exists(path);
}