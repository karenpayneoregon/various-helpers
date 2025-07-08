using System.Text;
using System.Text.RegularExpressions;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using NuGetLibrary.Models;

namespace NuGetLibrary;

public partial class PackageWork
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

            packages.AddRange(
                from folder in Directory.GetDirectories(packageFolder) 
                select Path.GetFileName(folder) into version 
                where VersionRegex().IsMatch(version) 
                select new Package { Name = packageName, Version = version });
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

        ISettings settings = Settings.LoadDefaultSettings(null);

        PackageSourceProvider packageSourceProvider = new PackageSourceProvider(settings);
        var packageSources = packageSourceProvider.LoadPackageSources();

        foreach (var source in packageSources)
        {
            
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


    /// <summary>
    /// Asynchronously retrieves and displays all available versions of the Serilog NuGet package.
    /// </summary>
    /// <remarks>
    /// This method connects to the NuGet V3 API to fetch version information for the Serilog package.
    /// </remarks>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task GetVersionsForSeriLog()
    {

        ILogger logger = NullLogger.Instance;
        CancellationToken cancellationToken = CancellationToken.None;

        SourceCacheContext cache = new SourceCacheContext();
        SourceRepository repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
        FindPackageByIdResource resource = await repository.GetResourceAsync<FindPackageByIdResource>(cancellationToken);

        IEnumerable<NuGetVersion> versions = await resource.GetAllVersionsAsync(
            "Serilog",
            cache,
            logger,
            cancellationToken);

        StringBuilder stringBuilder = new();
        foreach (NuGetVersion version in versions)
        {
            
            stringBuilder.AppendLine(version.ToString());
        }

        await File.WriteAllTextAsync("SerilogVersions.txt", stringBuilder.ToString(), cancellationToken);

    }

    /// <summary>
    /// Filters the provided list of NuGet packages to return only those that match the specified package name.
    /// </summary>
    /// <param name="packages">The list of <see cref="Package"/> objects to search through.</param>
    /// <param name="packageName">The name of the package to filter by.</param>
    /// <returns>A list of <see cref="Package"/> objects whose names match the specified <paramref name="packageName"/>.</returns>
    public static List<Package> GetPackagesByName(List<Package> packages, string packageName) 
        => packages.Where(x => x.Name == packageName).ToList();

    [GeneratedRegex(@"^(?=(.*\.){2,})(?=(.*\d){2,}).*$")]
    private static partial Regex VersionRegex();
}

