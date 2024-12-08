using GetGlobalNuGetPackages.Models;
using NuGet.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GetGlobalNuGetPackages.Classes;

public partial class Work
{
    /// <summary>
    /// Retrieves a list of available NuGet packages from the global packages folder.
    /// </summary>
    /// <returns>A list of <see cref="Package"/> objects representing the available packages.</returns>
    public static List<Package> AvailablePackages()
    {
        List<Package> packages = [];

        var nugetPath = PackageSettings.Instance.Path;

        foreach (string packageFolder in Directory.GetDirectories(nugetPath))
        {
            string packageName = Path.GetFileName(packageFolder);

            foreach (string folder in Directory.GetDirectories(packageFolder))
            {
                string version = Path.GetFileName(folder);

                if (!VersionRegex().IsMatch(version)) continue;
                packages.Add(new Package { Name = packageName, Version = version });
            }
        }

        return packages;
    }

    /// <summary>
    /// Retrieves a list of NuGet package sources, including their names, sources, and enabled statuses.
    /// </summary>
    /// <returns>A list of <see cref="NuGetPackage"/> objects representing the package sources.</returns>
    public static List<NuGetPackage> Packages()
    {
        List<NuGetPackage> list = [];

        string configFilePath = null;

        // Load NuGet settings
        ISettings settings = Settings.LoadDefaultSettings(configFilePath);

        // Get the list of all package sources
        PackageSourceProvider packageSourceProvider = new PackageSourceProvider(settings);
        var packageSources = packageSourceProvider.LoadPackageSources();

        // Enable a specific package source
        string sourceNameToEnable = "nuget"; // Replace with your desired source name
        foreach (var source in packageSources)
        {
            if (source.Name.Equals(sourceNameToEnable, StringComparison.OrdinalIgnoreCase))
            {
                var isEnabled = source.IsEnabled;
            }

            list.Add(new NuGetPackage()
            {
                Name = source.Name, 
                Source = source.Source,
                Enabled = source.IsEnabled,
                HasCredentials = source.Credentials != null
            });
        }

        return list;
    }

    [GeneratedRegex(@"^(?=(.*\.){2,})(?=(.*\d){2,}).*$")]
    private static partial Regex VersionRegex();
}