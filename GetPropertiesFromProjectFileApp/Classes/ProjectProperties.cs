namespace GetPropertiesFromProjectFileApp.Classes;

/// <summary>
/// Represents the properties of a .NET project file, such as PackageId, Version, Authors, and Company.
/// </summary>
/// <remarks>
/// This class is used to store and manage metadata extracted from a .NET project file.
/// It provides a structured way to access and manipulate project properties.
/// </remarks>
public class ProjectProperties
{
    public string PackageId { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public List<string> Authors { get; set; } = [];
    public string Company { get; set; } = string.Empty;

    public override string ToString() =>
        $"PackageId: {PackageId}, Version: {Version}, " +
        $"Authors: [{string.Join(", ", Authors)}], Company: {Company}";
}

