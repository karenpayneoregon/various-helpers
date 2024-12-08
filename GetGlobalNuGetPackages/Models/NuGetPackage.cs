namespace GetGlobalNuGetPackages.Models;

/// <summary>
/// Represents a NuGet package with its name, source, and enabled status.
/// </summary>
public class NuGetPackage
{
    public string Name { get; set; }
    public string Source { get; set; }
    public bool Enabled { get; set; }
    public bool HasCredentials { get; set; }
    public override string ToString() => Name;

}