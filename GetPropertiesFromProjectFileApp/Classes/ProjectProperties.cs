namespace GetPropertiesFromProjectFileApp.Classes;
public class ProjectProperties
{
    public string PackageId { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public List<string> Authors { get; set; } = [];
    public string Company { get; set; } = string.Empty;

    public override string ToString() =>
        $"PackageId: {PackageId}, Version: {Version}, " +
        $"Authors: [{string.Join(", ", Authors)}], Company: {Company}";
}

