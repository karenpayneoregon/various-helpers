using System.Text.Json;
using System.Xml.Linq;

namespace VisualStudioExternalToolsApp.Classes;

/// <summary>
/// Provides operations for reading and writing Visual Studio external-tool
/// configurations.
/// </summary>
public static class ExternalToolsOperations
{
    private const string ExternalToolsCategoryName = "Environment_ExternalTools";

    /// <summary>
    /// Reads user-created external tools from a Visual Studio settings file.
    /// </summary>
    /// <param name="vsSettingsPath">
    /// The complete path to the Visual Studio .vssettings file.
    /// </param>
    /// <returns>
    /// The user-created external tools found in the settings file.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="vsSettingsPath"/> is empty.
    /// </exception>
    /// <exception cref="FileNotFoundException">
    /// Thrown when the settings file does not exist.
    /// </exception>
    /// <exception cref="System.Xml.XmlException">
    /// Thrown when the settings file does not contain valid XML.
    /// </exception>
    public static IEnumerable<ExternalTool> ReadExternalTools(string vsSettingsPath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(vsSettingsPath);

        var document = XDocument.Load(vsSettingsPath);

        /*
         * VS2026 can export more than one Environment_ExternalTools
         * category. The first category may contain only
         * ExcludeRegisteredTool elements.
         *
         * Therefore, do not use FirstOrDefault(). Process every matching
         * category and select its UserCreatedTool elements.
         *
         * Name.LocalName also allows the code to work if a Visual Studio
         * version adds an XML namespace to the settings document.
         */
        var toolElements = document
            .Descendants()
            .Where(IsExternalToolsCategory)
            .SelectMany(category => category
                .Elements()
                .Where(element => HasName(element, "ExternalTools")))
            .SelectMany(externalTools => externalTools
                .Elements()
                .Where(element => HasName(element, "UserCreatedTool")));

        foreach (var toolElement in toolElements)
        {
            yield return new ExternalTool
            {
                Index = ReadInt(toolElement, "Index", -1),
                Title = ReadString(toolElement, "Title"),
                Command = ReadString(toolElement, "Command"),
                Arguments = ReadString(toolElement, "Arguments","(none)"),
                InitialDirectory = ReadString(toolElement, "InitialDirectory", "(none)"),
                IsGuiApp = ReadBoolean(toolElement, "IsGUIapp"),
                CloseOnExit = ReadBoolean(toolElement, "CloseOnExit")
            };
        }
    }

    /// <summary>
    /// Writes external-tool configurations to a JSON file.
    /// </summary>
    /// <param name="outputPath">
    /// The complete path of the output JSON file.
    /// </param>
    /// <param name="tools">
    /// The external tools to serialize.
    /// </param>
    public static void WriteToolsJson(string outputPath, IEnumerable<ExternalTool> tools)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(outputPath);
        ArgumentNullException.ThrowIfNull(tools);

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(tools, options);

        File.WriteAllText(outputPath, json);
    }

    private static bool IsExternalToolsCategory(XElement element)
    {
        if (!HasName(element, "Category"))
        {
            return false;
        }

        var categoryName = element
            .Attributes()
            .FirstOrDefault(attribute =>
                string.Equals(
                    attribute.Name.LocalName,
                    "name",
                    StringComparison.OrdinalIgnoreCase))
            ?.Value;

        return string.Equals(categoryName, ExternalToolsCategoryName, StringComparison.OrdinalIgnoreCase);
    }

    private static bool HasName(XElement element, string localName) =>
        string.Equals(
            element.Name.LocalName,
            localName,
            StringComparison.OrdinalIgnoreCase);

    private static XElement? FindChild(XElement parent, string localName) =>
        parent
            .Elements()
            .FirstOrDefault(element =>
                HasName(element, localName));

    private static string ReadString(XElement parent, string elementName, string defaultValue = "")
    {
        var value = FindChild(parent, elementName)?.Value;

        return string.IsNullOrWhiteSpace(value)
            ? defaultValue
            : value;
    }

    private static int ReadInt(XElement parent, string elementName, int defaultValue)
    {
        var value = FindChild(parent, elementName)?.Value;

        return int.TryParse(value, out var result)
            ? result
            : defaultValue;
    }

    private static bool ReadBoolean(XElement parent, string elementName)
    {
        var value = FindChild(parent, elementName)?.Value;

        if (bool.TryParse(value, out var booleanResult))
        {
            return booleanResult;
        }

        // Some Visual Studio settings use 0 and 1 for Boolean values.
        return int.TryParse(value, out var integerResult) &&
               integerResult != 0;
    }
}