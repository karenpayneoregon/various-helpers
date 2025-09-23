using System.Xml.Linq;

namespace GetPropertiesFromProjectFileApp.Classes;


/// <summary>
/// Provides functionality to read and extract properties from a .NET project file.
/// </summary>
/// <remarks>
/// This class is designed to parse .csproj files and retrieve specific properties such as
/// PackageId, Version, Authors, and Company. It assumes the project file is in XML format
/// and adheres to the standard structure of .NET project files.
/// </remarks>
public static class ProjectFileReader
{
    public static ProjectProperties GetProperties(string projectFilePath)
    {
        if (!File.Exists(projectFilePath))
            throw new FileNotFoundException("Project file not found.", projectFilePath);

        var doc = XDocument.Load(projectFilePath);

        var propertyGroups = doc.Root?.Elements("PropertyGroup");

        var authorsRaw = propertyGroups?.Elements("Authors").FirstOrDefault()?.Value ?? string.Empty;
        var authorsList = authorsRaw
            .Split([','], StringSplitOptions.RemoveEmptyEntries)
            .Select(a => a.Trim())
            .ToList();

        return new()
        {
            PackageId = propertyGroups?.Elements("PackageId").FirstOrDefault()?.Value ?? string.Empty,
            Version = propertyGroups?.Elements("Version").FirstOrDefault()?.Value ?? string.Empty,
            Authors = authorsList,
            Company = propertyGroups?.Elements("Company").FirstOrDefault()?.Value ?? string.Empty
        };
    }
}


