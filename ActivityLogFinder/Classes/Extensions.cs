using System.Buffers;

namespace ActivityLogFinder.Classes;
public static class Extensions
{
    /// <summary>
    /// Searches the specified string for any of the provided tokens case-insensitive.
    /// </summary>
    /// <param name="sender">The string to search within.</param>
    /// <param name="tokens">An array of tokens to search for within the string.</param>
    /// <returns>
    /// <c>true</c> if any of the tokens are found within the string; otherwise, <c>false</c>.
    /// </returns>
    public static bool Search(this string sender, string[] tokens) 
        => sender.AsSpan().ContainsAny(
            SearchValues.Create(tokens, 
                StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Determines whether the specified line contains a warning or error.
    /// </summary>
    /// <param name="line">The line of text to be checked for warnings or errors.</param>
    /// <returns>
    /// <c>true</c> if the line contains a warning or error; otherwise, <c>false</c>.
    /// </returns>
    public static bool LineHasWarningOrError(this string line)
    {
        ReadOnlySpan<string> tokens = ["<type>Error</type>", "<type>Warning</type>"];
        return line.AsSpan().ContainsAny(SearchValues.Create(tokens, StringComparison.OrdinalIgnoreCase));
    }
    
    /// <summary>
    /// Determines whether the specified line contains a warning or error using conventional string comparison.
    /// </summary>
    /// <param name="line">The line of text to be checked for warnings or errors.</param>
    /// <returns>
    /// <c>true</c> if the line contains a warning or error; otherwise, <c>false</c>.
    /// </returns>
    public static bool LineHasWarningOrErrorConventional(this string line) =>
        line.IndexOf("<type>Error</type>", StringComparison.OrdinalIgnoreCase) > 1 && 
        line.IndexOf("<type>Warning</type>", StringComparison.OrdinalIgnoreCase) > 1;
}
