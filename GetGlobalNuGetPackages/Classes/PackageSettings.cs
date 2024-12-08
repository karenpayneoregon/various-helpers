using NuGet.Configuration;

namespace GetGlobalNuGetPackages.Classes;

/// <summary>
/// Represents the settings for managing NuGet packages, including configuration
/// and the global packages folder path.
/// </summary>
public sealed class PackageSettings
{
    private static readonly Lazy<PackageSettings> Lazy = new(() => new PackageSettings());
    public static PackageSettings Instance => Lazy.Value;

    public ISettings NuGetSettings { get; set; }
    /// <summary>
    /// Gets the path to the global NuGet packages folder.
    /// </summary>
    /// <value>
    /// A string representing the path to the global NuGet packages folder.
    /// </value>
    public string Path { get; init; }

    public SettingSection ActivePackageSources { get; init; }
    public SettingSection DisabledPackageSources { get; init; }
    public List<NuGetPackage> NuGetPackages { get; set; }

    private PackageSettings()
    {
        NuGetSettings = Settings.LoadDefaultSettings(null);
        Path = SettingsUtility.GetGlobalPackagesFolder(NuGetSettings);
        ActivePackageSources = NuGetSettings.GetSection("packageSources");

        var test = NuGetSettings.GetSection("packageSources");
        var test1 = NuGetSettings.GetSection("disabledPackageSources");
        var names = new List<string>();
        foreach (SettingItem ss in test.Items)
        {
            IReadOnlyDictionary<string, string> dict = ss.GetAttributes();
            foreach (var (key, value) in dict)
            {
                //Console.WriteLine($"{key} - {value}");
                names.Add(value);
            }

            

        }

        DisabledPackageSources = NuGetSettings.GetSection("disabledPackageSources");
        NuGetPackages = Work.GetPackages();
    }

}