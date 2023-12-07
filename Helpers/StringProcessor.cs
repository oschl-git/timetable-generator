using System.Text;

namespace TimetableGenerator.Helpers;

public abstract class StringProcessor
{
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