
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;


namespace FindInterfaces.Classes;
internal class Helpers
{
    /// <summary>
    /// Asynchronously finds all C# files in the solution that contain interface declarations.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result is a list of file paths
    /// (relative to the solution folder) containing interface declarations. If no interfaces are found,
    /// the list will be empty.
    /// </returns>
    /// <remarks>
    /// This method scans all C# files in the solution directory and its subdirectories for interface
    /// declarations using the Roslyn API. The operation is performed concurrently for better performance.
    ///
    /// Will pick up on methods that are declared as interfaces.
    /// </remarks>
    public static async Task<List<string?>> FindInterfacesInSolution(string folder)
    {
        var list = new List<string?>();
        
        var files = await Task.Run(() => GetFiles(folder));

        var tasks = files.Select(async file =>
        {
            var fileContents = await File.ReadAllTextAsync(file);
            var syntaxTree = CSharpSyntaxTree.ParseText(fileContents);
            var root = await syntaxTree.GetRootAsync();

            var interfaceNodes = 
                root.DescendantNodes().OfType<InterfaceDeclarationSyntax>();

            if (interfaceNodes.Any())
            {
                list.Add(file.Replace($"{folder}\\", ""));
            }
        });

        await Task.WhenAll(tasks);

        return list;
    }


    public static string[] GetFiles(string folder) 
        => Directory.GetFiles(folder, "*.cs", SearchOption.AllDirectories);
}



