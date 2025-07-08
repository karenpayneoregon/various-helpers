using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioExternalToolsApp.Classes;
public class FileOperations
{
    public void OpenSettingsFile(string filePath)
    {

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Settings file not found.");
            return;
        }

        try
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = $"C:\\Users\\{Environment.UserName}\\AppData\\Local\\Programs\\Microsoft VS Code\\Code.exe",
                Arguments = $"\"{filePath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = false,
                CreateNoWindow = true
            };

            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to open settings file in VS Code. Error: {ex.Message}");
        }
    }
}

