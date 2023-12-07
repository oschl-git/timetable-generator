namespace TimetableGenerator.Helpers;

public class ColoredString
{
    public string Content { get; }
    public ConsoleColor Color { get; }

    public ColoredString(string content, ConsoleColor color = ColoredConsole.DefaultColor)
    {
        Content = content;
        Color = color;
    }
}