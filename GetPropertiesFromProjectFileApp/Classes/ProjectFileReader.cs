using System.Xml.Linq;

namespace GetPropertiesFromProjectFileApp.Classes;


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


