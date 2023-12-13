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

    /// <summary>
    /// Gets input from the user, requires that the user matches the provided conditions.
    /// </summary>
    /// <param name="prompt">What to prompt the user with.</param>
    /// <param name="conditions">
    /// Conditions the user must meet. An array of anonymous functions which need to return a touple with a boolean
    /// with if the input was valid, and a message to print in case it was not.
    /// </param>
    /// <param name="tryAgainPrompt">
    /// What to prompt the user with in case they input an invalid string. By default, the regular prompt is used.
    /// </param>
    /// <returns></returns>
    public static string? GetInputFromUser(
        string prompt,
        Func<string, (bool, string?)>[]? conditions = null,
        string? tryAgainPrompt = null
    )
    {
        tryAgainPrompt ??= prompt;
        Write(prompt, ConsoleColor.Gray);

        var inputIsOk = false;
        string value = null;
        while (!inputIsOk)
        {
            value = Console.ReadLine();

            if (value == null)
            {
                WriteLine("Please provide a value.", ConsoleColor.Red);
                continue;
            }
            
            inputIsOk = true;
            if (conditions == null) break;
            foreach (var condition in conditions)
            {
                var conditionResult = condition(value);
                inputIsOk = inputIsOk && conditionResult.Item1;
                if (!inputIsOk && conditionResult.Item2 != null) WriteLine(conditionResult.Item2, ConsoleColor.Red);
            }
            if (!inputIsOk) Write(tryAgainPrompt, ConsoleColor.Gray);
        }

        return value;
    }
}