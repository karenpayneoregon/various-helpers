using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace ActivityLogFinder;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        Console.Title = "SearchValues code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
