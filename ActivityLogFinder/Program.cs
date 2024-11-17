using ActivityLogFinder.Classes;

namespace ActivityLogFinder;

internal partial class Program
{
    static void Main(string[] args)
    {
        AnsiConsole.MarkupLine("[yellow]Working[/]");
        var (lines, success) = ActivityOperations.ReadLog();
        if (success)
        {
            AnsiConsole.MarkupLine($"[cyan]{ActivityOperations.FileName()}[/] has [cyan]{lines.Length:n0}[/] lines");
            Console.WriteLine();
            foreach (var (index, line) in lines.Index())
            {
                if (!line.LineHasWarningOrError()) continue;
                var trimStart = line.TrimStart();
                
                AnsiConsole.MarkupLine(line.IndexOf("error", StringComparison.OrdinalIgnoreCase) > 0
                    ? $"{index + 1,-6}[magenta3_2]{trimStart}[/]"
                    : $"{index + 1,-6}[lightcyan3]{trimStart}[/]");
            }

        }
        else
        {
            AnsiConsole.MarkupLine("[red]Failed[/]");
        }
        SpectreConsoleHelpers.ExitPrompt();
    }
}