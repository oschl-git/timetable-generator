using System.Text;

namespace TimetableGenerator.Helpers;

/// <summary>
/// An abstract class with functions for string operations.
/// </summary>
public abstract class StringProcessor
{
    /// <summary>
    /// Adds filled spaces at the end of a string. Useful for keeping items aligned in the console.
    /// </summary>
    /// <param name="input">The string to write</param>
    /// <param name="length">The minimum length of the string with spaces.</param>
    /// <returns>The strings with the appropriate amount spaces at the end.</returns>
    public static string AddFillerSpaces(string input, int length)
    {
        var builder = new StringBuilder(input);
        for (var i = 0; i < length - input.Length; i++)
        {
            builder.Append(' ');
        }

        return builder.ToString();
    }
}