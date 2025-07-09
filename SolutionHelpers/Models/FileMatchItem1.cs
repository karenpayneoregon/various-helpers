namespace SolutionHelpers.Models;

/// <summary>
/// Represents a matched file item, typically used to store information about files
/// found during file system operations, such as globbing or searching within a directory.
/// </summary>
public class FileMatchItem1
{
    /// <summary>
    /// Gets or sets the file path of the matched file. This property typically
    /// contains the full path to the file found during file system operations.
    /// </summary>
    public string? FilePath { get; set; }
}