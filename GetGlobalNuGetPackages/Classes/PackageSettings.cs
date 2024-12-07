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
    private PackageSettings()
    {
        NuGetSettings = Settings.LoadDefaultSettings(null);
        Path = SettingsUtility.GetGlobalPackagesFolder(NuGetSettings);
    }
}