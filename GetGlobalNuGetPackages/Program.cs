using GetGlobalNuGetPackages.Classes;
using GetGlobalNuGetPackages.Models;
using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;


namespace GetGlobalNuGetPackages;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        Work.Packages();

        if (Directory.Exists(PackageSettings.Instance.Path))
        {
            var packages = Work.AvailablePackages();
            AnsiConsole.MarkupLine($"[yellow]Local package count[/] {packages.Count:N0}");
            Console.WriteLine();

            ExtractPackagesByName(packages, "bogus");

            Console.WriteLine();

            var rule = new Rule("[yellow]Package sources:[/]")
            {
                Justification = Justify.Left
            };
            AnsiConsole.Write(rule);
            foreach (var p in PackageSettings.Instance.NuGetPackages)
            {
                AnsiConsole.MarkupLine($"[white]{p.Name}[/]");
                AnsiConsole.MarkupLine($"        [green]{p.Source}[/]");
                AnsiConsole.MarkupLine($"        Enabled   [lime]{p.Enabled.ToText()}[/]");
                AnsiConsole.MarkupLine($"Has Credentials   [lime]{p.HasCredentials.ToText()}[/]");
            }
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Not found[/] {PackageSettings.Instance.Path}");
        }

        await Work.HowToGetVersionsForSeriLog();

        Console.ReadLine();
    }

    /// <summary>
    /// Extracts and processes NuGet packages with the specified name from the provided list.
    /// </summary>
    /// <param name="packages">A list of <see cref="Package"/> objects to be filtered and processed.</param>
    /// <param name="packageName">Name of package</param>
    /// <remarks>
    /// In this example the method filters the provided list of packages by the name "bogus" and processes the results.
    /// </remarks>
    private static void ExtractPackagesByName(List<Package> packages, string packageName)
    {

        SpectreConsoleHelpers.PrintCyan();

        var bogusList = packages.Where(x => x.Name == packageName).ToList();

        if (bogusList.Count > 0)
        {
            AnsiConsole.MarkupLine($"[lime]Bogus packages[/] {bogusList.Count:N0}");
            var versions = string.Join(",", bogusList.Select(x => x.Version).ToList());
            versions.ColorizeComma();
        }
        else
        {
            AnsiConsole.MarkupLine("[red]No bogus packages found[/]");
        }
    }



}