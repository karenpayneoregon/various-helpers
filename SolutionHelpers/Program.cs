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
    private static List<string> _projectNames = new();
    static async Task Main(string[] args)
    {
        TraverseFileMatch += GlobbingTraverseFileMatch;
        await GetProjectFiles(GetSolutionInfo().FullName);
        await File.WriteAllLinesAsync("Projects.txt", _projectNames);
    }

    private static void GlobbingTraverseFileMatch(FileMatchItem sender)
    {
        _projectNames.Add(Path.GetFileNameWithoutExtension(sender.FileName));
    }
}
