using System.Text.Json;
using System.Xml.Linq;

namespace VisualStudioExternalToolsApp.Classes;

/// <summary>
/// Provides operations for handling external tools configurations in Visual Studio.
/// </summary>
/// <remarks>
/// This class includes methods to read and process external tool configurations from Visual Studio settings files.
/// It parses the settings file to extract details about user-created external tools, such as their index, title,
/// command, arguments, initial directory, and additional properties.
/// </remarks>
public class ExternalToolsOperations
{
    /// <summary>
    /// Reads the external tools configuration from a specified Visual Studio settings file.
    /// </summary>
    /// <param name="vsSettingsPath">
    /// The full path to the Visual Studio settings file (e.g., "CurrentSettings.vssettings") 
    /// containing the external tools configuration.
    /// </param>
    /// <returns>
    /// An enumerable collection of <see cref="ExternalTool"/> objects representing the external tools
    /// defined in the specified settings file. If no tools are found, an empty collection is returned.
    /// </returns>
    /// <remarks>
    /// This method parses the Visual Studio settings file to extract details about user-created external tools.
    /// Each tool's configuration includes properties such as its index, title, command, arguments, initial directory,
    /// and additional flags indicating whether it is a GUI application and whether it should close on exit.
    /// </remarks>
    /// <exception cref="FileNotFoundException">
    /// Thrown if the specified settings file does not exist.
    /// </exception>
    /// <exception cref="System.Xml.XmlException">
    /// Thrown if the specified settings file is not a valid XML document.
    /// </exception>
    public static IEnumerable<ExternalTool> ReadExternalTools(string vsSettingsPath)
    {
        var doc = XDocument.Load(vsSettingsPath);

        var toolsRoot = doc
            .Descendants("Category")
            .FirstOrDefault(c => (string)c.Attribute("name") == "Environment_ExternalTools")
            ?.Element("ExternalTools");

        if (toolsRoot == null)
            yield break;   // Nothing there – bail out.

        foreach (var tool in toolsRoot.Elements("UserCreatedTool"))
        {
            yield return new ExternalTool
            {
                Index = (int?)tool.Element("Index") ?? -1,
                Title = (string?)tool.Element("Title") ?? "",
                Command = (string?)tool.Element("Command") ?? "",
                Arguments = (string?)tool.Element("Arguments") ?? "(none)",
                InitialDirectory = (string?)tool.Element("InitialDirectory") ?? "(none)",
                IsGuiApp = (bool?)tool.Element("IsGUIapp") ?? false,
                CloseOnExit = (bool?)tool.Element("CloseOnExit") ?? false
            };
        }
    }

    /// <summary>
    /// Writes a collection of external tools to a JSON file at the specified output path.
    /// </summary>
    /// <param name="outputPath">
    /// The file path where the JSON representation of the external tools will be written.
    /// </param>
    /// <param name="tools">
    /// A collection of <see cref="ExternalTool"/> objects representing the external tools to be serialized.
    /// </param>
    /// <remarks>
    /// This method serializes the provided collection of external tools into a JSON format and writes it to the specified file.
    /// The JSON output is formatted with indentation for better readability.
    /// </remarks>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="outputPath"/> or <paramref name="tools"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="IOException">
    /// Thrown if an I/O error occurs while writing to the file.
    /// </exception>
    public static void WriteToolsJson(string outputPath, IEnumerable<ExternalTool> tools)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true 
        };

        File.WriteAllText(outputPath, JsonSerializer.Serialize(tools, options));
    }
}
