using SolutionHelpers.Interfaces;
using SolutionHelpers.Models;
using static SolutionHelpers.Classes.DirectoryOperations;
using static SolutionHelpers.Classes.GlobbingOperations;

namespace SolutionHelpers;
/// <summary>
/// As coded will get a list of project files in the current solution
/// but you can replace GetSolutionInfo().FullName with the path to any
/// existing solution.
/// </summary>

internal class Program
{
    private static List<string> _projectNames = [];
    private static List<string> _solutionNames = [];

    static async Task Main(string[] args)
    {
        TraverseFileMatch += GlobbingTraverseFileMatch;
        TraverseSolutionMatch += GlobbingTraverseSolutionMatch;

        // write all project files to a file
        await GetProjectFilesAsync(GetSolutionInfo().FullName);
        await GetProjectFilesAsync("C:\\DotnetLand\\VS2022\\closet-code");
        await File.WriteAllLinesAsync("Projects.txt", _projectNames);

        // write all solution names to a file
        await GetSolutionFilesAsync("C:\\DotnetLand\\VS2022");
        await File.WriteAllLinesAsync("Solutions.txt", _solutionNames);

    }

    private static void GlobbingTraverseSolutionMatch(FileMatchItem sender)
    {
        _solutionNames.Add(Path.GetFileNameWithoutExtension(sender.FileName));
    }

    private static void GlobbingTraverseFileMatch(FileMatchItem sender)
    {
        _projectNames.Add(Path.GetFileNameWithoutExtension(sender.FileName));
    }
}
