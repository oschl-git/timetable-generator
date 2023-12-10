namespace TimetableGenerator.Helpers;

/// <summary>
/// A wrapper class around Console which allows printing colored strings in a simple manner.
/// </summary>
public static class ColoredConsole
{
    public const ConsoleColor DefaultColor = ConsoleColor.White;
    public const ConsoleColor DefaultPrefixColor = ConsoleColor.Gray;
    public const string DefaultPrefix = "> ";

    /// <summary>
    /// Writes the provided string with the provided color.
    /// </summary>
    /// <param name="content">String to write.</param>
    /// <param name="color">ConsoleColor to use.</param>
    public static void Write(string content = "", ConsoleColor color = DefaultColor)
    {
        Console.ForegroundColor = color;
        Console.Write(content);
        Console.ForegroundColor = DefaultColor;
    }

    /// <summary>
    /// Writes the provided string and a newline with the provided color.
    /// </summary>
    /// <param name="content">String to write.</param>
    /// <param name="color">ConsoleColor to use.</param>
    public static void WriteLine(string content = "", ConsoleColor color = DefaultColor)
    {
        Write(content + "\n", color);
    }
    
    /// <summary>
    /// Writes the provided string with a prefix with the provided colors for each.
    /// </summary>
    /// <param name="content">String to write.</param>
    /// <param name="prefix">Prefix to use.</param>
    /// <param name="contentColor">ConsoleColor for the content.</param>
    /// <param name="prefixColor">ConsoleColor for the prefix.</param>
    public static void WriteWithPrefix(
        string content = "",
        string prefix = DefaultPrefix,
        ConsoleColor contentColor = DefaultColor,
        ConsoleColor prefixColor = DefaultPrefixColor
    )
    {
        Write(prefix, prefixColor);
        Write(content, contentColor);
    }

    /// <summary>
    /// Writes the provided string with a prefix and a newline with the provided colors for each.
    /// </summary>
    /// <param name="content">String to write.</param>
    /// <param name="prefix">Prefix to use.</param>
    /// <param name="contentColor">ConsoleColor for the content.</param>
    /// <param name="prefixColor">ConsoleColor for the prefix.</param>
    public static void WriteLineWithPrefix(
        string content = "",
        string prefix = DefaultPrefix,
        ConsoleColor contentColor = DefaultColor,
        ConsoleColor prefixColor = DefaultPrefixColor
    )
    {
        WriteWithPrefix(content, prefix, contentColor, prefixColor);
        Console.Write("\n");
    }
    
    /// <summary>
    /// Writes ColoredStrings provided in a collection.
    /// </summary>
    /// <param name="stringArray">A collection of ColoredStrings to write.</param>
    public static void WriteArray(IEnumerable<ColoredString> stringArray)
    {
        foreach (var coloredString in stringArray)
        {
            Write(coloredString.Content, coloredString.Color);
        }
    }
}