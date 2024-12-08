using GetGlobalNuGetPackages.Classes;

namespace GetGlobalNuGetPackages;

internal partial class Program
{
    static void Main(string[] args)
    {
        Work.Packages();

        if (Directory.Exists(PackageSettings.Instance.Path))
        {
            var packages = Work.AvailablePackages();
            AnsiConsole.MarkupLine($"[yellow]Local package count[/] {packages.Count:N0}");
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

        SpectreConsoleHelpers.ExitPrompt();
    }
}