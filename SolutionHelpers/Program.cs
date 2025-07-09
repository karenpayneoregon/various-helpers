using SolutionHelpers.Models;
using Spectre.Console;
using static SolutionHelpers.Classes.GlobbingOperations;
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

namespace SolutionHelpers;
/// <summary>
/// As coded will get a list of project files in the current solution
/// , but you can replace GetSolutionInfo().FullName with the path to any
/// existing solution.
/// </summary>

internal class Program
{


    static async Task Main(string[] args)
    {

        var selectedSolutionPath = "C:\\OED\\DotnetLand\\VS2022\\HowToSeriesSolution";

        if (Directory.Exists(selectedSolutionPath))
        {
            var projects = GetProjectFiles(selectedSolutionPath);

            if (projects.Count > 0)
            {
                await File.WriteAllLinesAsync("Projects.txt", projects);
            }
        }


        var mainPath = "C:\\OED\\DotnetLand\\VS2022";
        if (Directory.Exists(mainPath))
        {
            List<FileMatchItem1> solutions = [];

            AnsiConsole.Status().Start("[cyan]Getting solutions...[/]",
                ctx =>
                {
                    solutions = FindSolutionFiles(mainPath);

                });

            if (solutions.Count > 0)
            {
                await File.WriteAllLinesAsync("Solutions.txt", solutions.Select(x => x.FilePath).ToArray());
            }
        }


        Console.ReadLine();

    }




}
