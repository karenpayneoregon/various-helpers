namespace NuGetPackageSourceApp.Models;

/// <summary>
/// Represents a NuGet package.
/// </summary>
public class NuGetPackage
{
    public string Name { get; set; }
    public string Source { get; set; }
    public bool Enabled { get; set; }
    public bool HasCredentials { get; set; }
    public override string ToString() => Name;
}