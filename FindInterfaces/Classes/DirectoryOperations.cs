#nullable disable

namespace FindInterfaces.Classes;
internal class DirectoryOperations
{
    /// <summary>
    /// Retrieves the <see cref="DirectoryInfo"/> object representing the solution folder.
    /// </summary>
    /// <param name="path">
    /// The starting directory path to search for the solution folder. If <c>null</c>, the current working directory is used.
    /// </param>
    /// <returns>
    /// A <see cref="DirectoryInfo"/> object representing the solution folder, or <c>null</c> if no solution folder is found.
    /// </returns>
    public static DirectoryInfo GetSolutionInfo(string path = null!)
    {
        DirectoryInfo directory = new(path ?? Directory.GetCurrentDirectory());

        while (directory is not null && directory.GetFiles("*.sln").Length == 0)
        {
            directory = directory.Parent;
        }

        return directory;
    }
    /// <summary>
    /// Retrieves the name of the current solution.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> representing the name of the current solution, or <c>null</c> if no solution file is found.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the solution folder cannot be determined.
    /// </exception>
    public static string SolutionName() =>
        Path.GetFileName(Directory
            .GetFiles(GetSolutionInfo().FullName, "*.sln")
            .FirstOrDefault()!);
}