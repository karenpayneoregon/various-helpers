using Microsoft.Extensions.FileSystemGlobbing;
using SolutionHelpers.Models;

namespace SolutionHelpers.Classes;
internal class GlobbingOperations
{
    public delegate void OnTraverseFileMatch(FileMatchItem sender);
    public static event OnTraverseFileMatch? TraverseFileMatch;

    public delegate void OnTraverseSolutionMatch(FileMatchItem sender);
    public static event OnTraverseSolutionMatch? TraverseSolutionMatch;

    /// <summary>
    /// Get all project files in a solution
    /// </summary>
    /// <param name="folder">Solution folder</param>
    public static async Task GetProjectFilesAsync(string folder)
    {

        Matcher matcher = new();
        matcher.AddIncludePatterns(["**/*.csproj"]);

        await Task.Run(() =>
        {
            foreach (var file in matcher.GetResultsInFullPath(folder))
            {
                TraverseFileMatch?.Invoke(new FileMatchItem(file));
            }
        });
    }

    /// <summary>
    /// Get all solution names in a folder
    /// </summary>
    /// <param name="folder">Base folder</param>
    public static async Task GetSolutionFilesAsync(string folder)
    {

        Matcher matcher = new();
        matcher.AddIncludePatterns(["**/*.sln"]);

        await Task.Run(() =>
        {
            foreach (var file in matcher.GetResultsInFullPath(folder))
            {
                TraverseSolutionMatch?.Invoke(new FileMatchItem(file));
            }
        });
    }

}