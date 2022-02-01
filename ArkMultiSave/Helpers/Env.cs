using System.Diagnostics;

namespace ArkMultiSave.Helpers;
public static class Env
{
    public static void StartInDefaultApp(string path)
    {
        new Process
        {
            StartInfo = new ProcessStartInfo(path) { UseShellExecute = true }
        }.Start();
    }

    public static void Exit() => Environment.Exit(0);

    public static void StartInExplorer(string path) => Process.Start("explorer.exe", path);
}
