using FindInterfaces.Classes;
using Spectre.Console;

namespace FindInterfaces;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.Title = "Find Interfaces in Solution";

        AnsiConsole.MarkupLine("[green]Working...[/]");

        var folder = "C:\\OED\\DotnetLand\\VS2022\\WorkingWithInterfaces";
        if (!Directory.Exists(folder))
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"[orchid]{folder}[/] [white]does not exists[/]");
            Console.ReadLine();
            return;
        }
        var list = await Helpers.FindInterfacesInSolution(folder);

        if (list.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]None found.[/]");
        }

        foreach (var file in list)
        {
            if (file is null)
                continue;

            var parts = file.SplitStringOnLastBackslash();
            if (parts.Length == 2)
            {
                AnsiConsole.MarkupLine($"    [skyblue3]{parts[0]}[/]\\[orchid]{parts[1]}[/]");
            }
        }

        AnsiConsole.MarkupLine("[green]Done[/]");
        Console.ReadLine();
    }
}
