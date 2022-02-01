namespace ArkMultiSave.Interfaces;

public interface ISaveService
{
    bool Exists(string path);
    IEnumerable<GameSave> GetSaves(string folderPath, string searchPattern);
    bool MoveTo(string source, string dest);
}