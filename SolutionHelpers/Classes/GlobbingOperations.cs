using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using SolutionHelpers.Models;

namespace SolutionHelpers.Classes;
/// <summary>
/// Provides utility methods for performing file system globbing operations, 
/// such as locating project files (*.csproj) and solution files (*.sln) 
/// within specified directories and their subdirectories.
/// </summary>
/// <remarks>
/// This class leverages the <see cref="Microsoft.Extensions.FileSystemGlobbing"/> 
/// library to perform pattern-based file matching.
/// </remarks>
internal class GlobbingOperations
{

    /// <summary>
    /// Retrieves all project files (*.csproj) within the specified solution folder and its subdirectories.
    /// </summary>
    /// <param name="folder">The root folder of the solution to search for project files.</param>
    /// <returns>A list of file paths representing the project files found in the solution folder.</returns>
    /// <remarks>
    /// This method uses globbing patterns to locate project files within the directory structure.
    /// </remarks>
    public static List<string> GetProjectFiles(string folder)
    {
        List<string> names = [];

        var matcher = new Matcher();
        matcher.AddInclude("**/*.csproj");

        var result = matcher.Execute(new DirectoryInfoWrapper(new DirectoryInfo(folder)));

        foreach (var file in result.Files)
        {
            names.Add(Path.Combine(folder, file.Path));
        }


        return names;

    }

    /// <summary>
    /// Finds all solution files (*.sln) within the specified root directory and its subdirectories.
    /// </summary>
    /// <param name="rootDirectory">The root directory to search for solution files.</param>
    /// <returns>A list of <see cref="FileMatchItem1"/> objects representing the matched solution files.</returns>
    /// <remarks>
    /// This method uses globbing patterns to locate solution files within the directory structure.
    /// </remarks>
    public static List<FileMatchItem1> FindSolutionFiles(string rootDirectory)
    {
        var matcher = new Matcher(StringComparison.OrdinalIgnoreCase);
        matcher.AddInclude("**/*.sln");

        var result = new List<FileMatchItem1>();
        var directoryInfo = new DirectoryInfo(rootDirectory);
        var directoryInfoWrapper = new DirectoryInfoWrapper(directoryInfo);
        var matchResults = matcher.Execute(directoryInfoWrapper);

        foreach (var file in matchResults.Files)
        {
            string fullPath = Path.Combine(rootDirectory, file.Path);
            result.Add(new FileMatchItem1 { FilePath = fullPath });
        }

        return result;
    }

}