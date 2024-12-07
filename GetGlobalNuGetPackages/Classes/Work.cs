using System.Text.RegularExpressions;

namespace GetGlobalNuGetPackages.Classes;

public partial class Work
{
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

    [GeneratedRegex(@"^(?=(.*\.){2,})(?=(.*\d){2,}).*$")]
    private static partial Regex VersionRegex();
}