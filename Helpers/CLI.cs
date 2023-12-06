namespace TimetableGenerator.Helpers;

public static class CLI
{
    private const ConsoleColor DefaultColor = ConsoleColor.White;
    private const ConsoleColor DefaultPrefixColor = ConsoleColor.Gray;
    private const string DefaultPrefix = "> ";


    public static void Write(string content, ConsoleColor color = DefaultColor)
    {
        Console.ForegroundColor = color;
        Console.Write(content);
        Console.ForegroundColor = DefaultColor;
    }

    public static void WriteLine(string content, ConsoleColor color = DefaultColor)
    {
        Write(content + "\n", color);
    }

    public static void WriteWithPrefix(
        string content, string prefix = DefaultPrefix,
        ConsoleColor contentColor = DefaultColor,
        ConsoleColor prefixColor = DefaultPrefixColor
    )
    {
        Write(prefix, prefixColor);
        Write(content, contentColor);
    }

    public static void WriteLineWithPrefix(
        string content, string prefix = DefaultPrefix,
        ConsoleColor contentColor = DefaultColor,
        ConsoleColor prefixColor = DefaultPrefixColor
    )
    {
        WriteWithPrefix(content, prefix, contentColor, prefixColor);
        Console.Write("\n");
    }
}