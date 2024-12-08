namespace GetGlobalNuGetPackages.Classes;
internal static class Extensions
{
    /// <summary>
    /// Converts a boolean value to its corresponding text representation.
    /// </summary>
    /// <param name="value">The boolean value to convert.</param>
    /// <returns>Returns "Yes" if the value is <c>true</c>; otherwise, returns "No".</returns>
    public static string ToText(this bool value) => value ? "Yes" : "No";
}
