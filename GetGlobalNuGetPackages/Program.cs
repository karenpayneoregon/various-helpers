using GetGlobalNuGetPackages.Classes;

namespace GetGlobalNuGetPackages;

internal partial class Program
{
    static void Main(string[] args)
    {
        if (Directory.Exists(PackageSettings.Instance.Path))
        {
            var packages = Work.AvailablePackages();
            AnsiConsole.MarkupLine($"[yellow]Package count[/] {packages.Count:N0}");
        }
        else
        {
            AnsiConsole.MarkupLine($"[red]Not found[/] {PackageSettings.Instance.Path}");
        }
        
        Console.ReadLine();
    }
}