using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SolutionHelpers.Models;

namespace SolutionHelpers.Classes;


public static class ProjectFileReader
{
    public static ProjectProperties GetProperties(string projectFilePath)
    {
        if (!File.Exists(projectFilePath))
            throw new FileNotFoundException("Project file not found.", projectFilePath);

        var doc = XDocument.Load(projectFilePath);

        // Looks at all <PropertyGroup> elements, not just the first
        var propertyGroups = doc.Root?.Elements("PropertyGroup");

        var props = new ProjectProperties
        {
            PackageId = propertyGroups?.Elements("PackageId").FirstOrDefault()?.Value ?? string.Empty,
            Version = propertyGroups?.Elements("Version").FirstOrDefault()?.Value ?? string.Empty,
            Authors = propertyGroups?.Elements("Authors").FirstOrDefault()?.Value ?? string.Empty,
            Company = propertyGroups?.Elements("Company").FirstOrDefault()?.Value ?? string.Empty
        };

        return props;
    }
}

