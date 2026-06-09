using DemoApp.Classes;
using DemoApp.Classes.Core;
using Spectre.Console;

namespace DemoApp;
internal partial class Program
{
    private static void Main(string[] args)
    {
        string[] searchItems = ["karen", "Teams"];
        var text = "Karen uses Microsoft Teams at work.";
        Console.WriteLine(text.SearchAny(searchItems)
            ? "Search items found in the text."
            : "Search items not found in the text.");

        Console.WriteLine();

        text = "Sue uses Microsoft Teams at work.";
        Console.WriteLine(text.SearchAll(searchItems)
            ? "Search items found in the text."
            : "Not all search items found in the text.");
        
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }
    
}
