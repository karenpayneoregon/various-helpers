using System.Buffers;

namespace DemoApp.Classes;

public static class Extensions
{
    /// <summary>
    /// Determines whether the specified string contains any of the provided tokens.
    /// </summary>
    /// <param name="sender">The string to search within.</param>
    /// <param name="tokens">An array of tokens to search for in the string.</param>
    /// <returns>
    /// <see langword="true"/> if any of the tokens are found in the string; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// The comparison is performed using <see cref="StringComparison.OrdinalIgnoreCase"/>.
    /// </remarks>
    public static bool SearchAny(this string sender, params string[] tokens)
        => sender.AsSpan().ContainsAny(
            SearchValues.Create(tokens, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Determines whether all specified tokens are contained within the given string.
    /// </summary>
    /// <param name="sender">The string to search within.</param>
    /// <param name="tokens">An array of tokens to search for in the string.</param>
    /// <returns>
    /// <see langword="true"/> if all tokens are found in the string; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// The comparison is performed using <see cref="StringComparison.OrdinalIgnoreCase"/>.
    /// </remarks>
    public static bool SearchAll(this string sender, params string[] tokens)
    {
        if (string.IsNullOrEmpty(sender))
            return false;

        var span = sender.AsSpan();

        foreach (var token in tokens)
        {
            if (!span.Contains(token.AsSpan(), StringComparison.OrdinalIgnoreCase))
                return false;
        }

        return true;
    }
}


