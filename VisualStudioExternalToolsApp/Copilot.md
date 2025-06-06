Initial prompt: Using C# read nodes for ExternalTools

- Added CurrentSettings.vssettings


---

Below is a stripped-down, no-nonsense way to pull the *External Tools* definitions out of a **.vssettings** file.
The XML fragment you’re interested in looks like this – note the enclosing **Category name="Environment\_ExternalTools"** element and its *UserCreatedTool* children:

---

### 1. Create a DTO to hold each tool

```csharp
public sealed record ExternalTool
{
    public int    Index            { get; init; }
    public string Title            { get; init; } = string.Empty;
    public string Command          { get; init; } = string.Empty;
    public string Arguments        { get; init; } = string.Empty;
    public string InitialDirectory { get; init; } = string.Empty;
    public bool   IsGuiApp         { get; init; }
    public bool   CloseOnExit      { get; init; }
}
```

### 2. Read the file with LINQ-to-XML

```csharp
using System.Xml.Linq;

IEnumerable<ExternalTool> ReadExternalTools(string vsSettingsPath)
{
    var doc = XDocument.Load(vsSettingsPath);

    // Find the Environment_ExternalTools category.
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
            Index            = (int?)tool.Element("Index")            ?? -1,
            Title            = (string?)tool.Element("Title")          ?? "",
            Command          = (string?)tool.Element("Command")        ?? "",
            Arguments        = (string?)tool.Element("Arguments")      ?? "",
            InitialDirectory = (string?)tool.Element("InitialDirectory") ?? "",
            IsGuiApp         = (bool?)tool.Element("IsGUIapp")         ?? false,
            CloseOnExit      = (bool?)tool.Element("CloseOnExit")      ?? false
        };
    }
}
```

**Why LINQ-to-XML?**

* It’s in-box, no dependencies, and handles the large, somewhat messy XML that *vssettings* spits out.
* The **Descendants("Category")** search is cheap enough and avoids hard-wiring the full path.
* Values are cast with the *element value to nullable* trick – keeps the code compact and avoids exceptions when a tag is missing.

### 3. Use it

```csharp
var tools = ReadExternalTools(@"C:\Path\CurrentSettings.vssettings").ToList();

foreach (var t in tools.OrderBy(t => t.Index))
{
    Console.WriteLine($"#{t.Index}  {t.Title}");
    Console.WriteLine($"    {t.Command} {t.Arguments}");
}
```

---

### A few practical notes

| Issue                                  | Reality check & recommendation                                                                                                          |
| -------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------- |
| **Tokens like `%vsspv_user_appdata%`** | They’re IDE macros, not environment variables. If you need literal paths, strip the `%...%` placeholders or expand them yourself.       |
| **ExcludeRegisteredTool** nodes        | Ignore them unless you want to list built-in tools the user chose to hide.                                                              |
| **Performance**                        | The file you supplied is \~MB-scale; `XDocument` handles it fine. For truly huge files, drop to `XmlReader` and stream instead.         |
| **Editing**                            | If you later need to modify or add tools, keep the same approach: locate the category node, tweak child elements, then `doc.Save(...)`. |

That’s the straight path – no fluff, just the code you need to read the *External Tools* entries and move on.

---

Add a single helper that dumps whatever’s in `tools` straight to **tools.json** with pretty formatting—nothing fancy, just the essentials:

```csharp
using System.Text.Json;
using System.Text.Json.Serialization; // if you need custom converters

void WriteToolsJson(string outputPath, IEnumerable<ExternalTool> tools)
{
    var options = new JsonSerializerOptions
    {
        WriteIndented = true               // human-readable
    };

    // If you’re on .NET 7+ and want UTF-8, use File.WriteAllBytes with JsonSerializer.SerializeToUtf8Bytes
    File.WriteAllText(outputPath, JsonSerializer.Serialize(tools, options));
}
```

### How to use it

```csharp
var tools = ReadExternalTools(@"C:\Path\CurrentSettings.vssettings").ToList();
WriteToolsJson(@"C:\Path\tools.json", tools);
```

That’s it—runs in one line, indented, ready for diffing or hand-editing.
