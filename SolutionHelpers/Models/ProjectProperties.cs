namespace SolutionHelpers.Models;
public class ProjectProperties
{
    public string PackageId { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Authors { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;

    public override string ToString() =>
        $"PackageId: {PackageId}, Version: {Version}, Authors: {Authors}, Company: {Company}";
}

