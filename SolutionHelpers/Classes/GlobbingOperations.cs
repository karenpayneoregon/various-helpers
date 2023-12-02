using Microsoft.Extensions.FileSystemGlobbing;
using SolutionHelpers.Models;

namespace SolutionHelpers.Classes;
internal class GlobbingOperations
{
    public delegate void OnTraverseFileMatch(FileMatchItem sender);
    public static event OnTraverseFileMatch? TraverseFileMatch;

    public static async Task GetProjectFiles(string folder)
    {

        Matcher matcher = new();
        matcher.AddIncludePatterns(new[] { "**/*.csproj" });

        await Task.Run(() =>
        {
            foreach (var file in matcher.GetResultsInFullPath(folder))
            {
                TraverseFileMatch?.Invoke(new FileMatchItem(file));
            }
        });
    }

}