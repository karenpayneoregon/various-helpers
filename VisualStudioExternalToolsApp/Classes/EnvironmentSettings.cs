namespace VisualStudioExternalToolsApp.Classes;

public sealed class EnvironmentSettings
{
    private static readonly Lazy<EnvironmentSettings> Lazy = new(() => new EnvironmentSettings());
    public static EnvironmentSettings Instance => Lazy.Value;

    public string FilePath { get; set; }
    public bool DirectoryExists { get; set; }
    public bool FileExists { get; set; }
    public string FileName { get; set; }
    private EnvironmentSettings()
    {
        var userName = Environment.UserName;
        // C:\Users\Payne\AppData\Local\Microsoft\VisualStudio\18.0_370e5cf8\Settings
        // FilePath = $"C:\\Users\\{userName}\\AppData\\Local\\Microsoft\\VisualStudio\\17.0_f56beab6\\Settings";
        FilePath = $"C:\\Users\\{userName}\\AppData\\Local\\Microsoft\\VisualStudio\\18.0_370e5cf8\\Settings";
        DirectoryExists = Directory.Exists(FilePath);
        //FileName = Path.Combine(FilePath, "CurrentSettings.vssettings");
        FileName = Path.Combine(FilePath, "CurrentSettings.vssettings");
        FileExists = File.Exists(FileName);
    }
}