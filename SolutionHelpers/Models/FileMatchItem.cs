namespace SolutionHelpers.Models;

/// <summary>
/// Represents an item that matches a file, containing its folder and file name information.
/// </summary>
public class FileMatchItem
{
    public FileMatchItem(string sender)
    {
        Folder = Path.GetDirectoryName(sender);
        FileName = Path.GetFileName(sender);
    }
    public string? Folder { get; init; }
    public string FileName { get; init; }
    public override string ToString() => $"{Folder}\\{FileName}";
}