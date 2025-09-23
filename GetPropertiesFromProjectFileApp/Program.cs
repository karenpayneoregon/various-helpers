using GetPropertiesFromProjectFileApp.Classes;

namespace GetPropertiesFromProjectFileApp;
internal partial class Program
{
    static void Main(string[] args)
    {
        
        var folders = Directory.GetCurrentDirectory().UpperFolders();
        var project = Path.Combine(folders.ElementAtOrDefault(2)!, 
            $"{typeof(Program).Namespace}.csproj");
        
        var props = ProjectFileReader.GetProperties(project);
        Console.WriteLine(props);

        SpectreConsoleHelpers.ExitPrompt();
    }
}
