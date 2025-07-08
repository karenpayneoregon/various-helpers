using GetGlobalNuGetPackages.Classes;
using NuGetLibrary;
using NuGetLibrary.Models;


namespace GetGlobalNuGetPackages;

internal partial class Program
{
    static async Task Main(string[] args)
    {

        PackageWork.Packages();

        if (Directory.Exists(PackageSettings.Instance.Path))
        {
            var packages = PackageWork.AvailablePackages();
            AnsiConsole.MarkupLine($"[yellow]Local package count[/] {packages.Count:N0}");
            Console.WriteLine();

            ExtractPackagesByName(packages, "serilog.aspnetcore");

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

        await PackageWork.GetVersionsForSeriLog();

        Console.ReadLine();
    }

    /// <summary>
    /// Extracts and processes NuGet packages with the specified name from the provided list.
    /// </summary>
    /// <param name="packages">A list of <see cref="NuGetLibrary.Models.Package"/> objects to be filtered and processed.</param>
    /// <param name="packageName">Name of package</param>
    /// <remarks>
    /// In this example the method filters the provided list of packages by the name "bogus" and processes the results.
    /// </remarks>
    private static void ExtractPackagesByName(List<Package> packages, string packageName)
    {

        SpectreConsoleHelpers.PrintCyan();

        List<Package> packageList = PackageWork.GetPackagesByName(packages, packageName);

        if (packageList.Count > 0)
        {
            AnsiConsole.MarkupLine($"[lime]{packageName} packages[/] {packageList.Count:N0}");
            var versions = string.Join(",", packageList.Select(x => x.Version).ToList());
            versions.ColorizeComma();
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]No {packageName} packages found[/]");
        }
    }



}