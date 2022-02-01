using System.Text.Json;

namespace ArkMultiSave.Helpers;
public static class Json
{
    public static JsonSerializerOptions Options { get; set; } = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public static T FromFile<T>(string path)
    {
        if (!File.Exists(path)) return default;

        var json = File.ReadAllText(path);
        if(string.IsNullOrWhiteSpace(json)) return default;

        return JsonSerializer.Deserialize<T>(json, Options);
    }

    public static bool ToFile<T>(string path, T value)
    {
        var json = JsonSerializer.Serialize(value, Options);
        File.WriteAllText(path, json);

        return true;
    }
}
