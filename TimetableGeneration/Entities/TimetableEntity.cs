using System.Text;
using TimetableGenerator.Helpers;

namespace TimetableGenerator.TimetableGeneration.Entities;

/// <summary>
/// An object which represents a timetable.
/// </summary>
public class TimetableEntity
{
    public Dictionary<string, LessonEntity?[]> Days { get; protected init; } = new()
    {
        { "Monday", new LessonEntity?[9] },
        { "Tuesday", new LessonEntity?[9] },
        { "Wednesday", new LessonEntity?[9] },
        { "Thursday", new LessonEntity?[9] },
        { "Friday", new LessonEntity?[9] },
    };

    /// <summary>
    /// The length of each day. Assumes that there are only 1 hour breaks for lunch, which are included in the length.
    /// </summary>
    /// <param name="index">The day index. E. g. 1 for Tuesday.</param>
    /// <returns>The length of the day.</returns>
    public int GetDayLengthByIndex(int index)
    {
        var day = Days.ElementAt(index).Value;
        var length = 0;
        var lastItemWasNull = false;

        foreach (var hour in day)
        {
            if (hour == null)
            {
                if (lastItemWasNull) break;
                lastItemWasNull = true;
            }
            else lastItemWasNull = false;

            length++;
        }

        return !lastItemWasNull ? length : length - 1;
    }

    /// <summary>
    /// Converts the timetable into a pretty and printable ColoredString List.
    /// </summary>
    /// <returns>A List of ColoredStrings.</returns>
    public virtual List<ColoredString> ToColoredStringList()
    {
        var output = new List<ColoredString>();
        output.Add(new ColoredString(GetTimetableHeader() + "\n", ConsoleColor.Gray));
        
        foreach (var (day, lessons) in Days)
        {
            output.Add(new ColoredString(
                StringProcessor.AddFillerSpaces($"{day}:", 10), ConsoleColor.Gray)
            );
            foreach (var lesson in lessons)
            {
                output.Add(lesson == null
                    ? new ColoredString(" [   ]", ConsoleColor.DarkGray)
                    : new ColoredString(
                        $" [{StringProcessor.AddFillerSpaces(lesson.Subject.ToString(), 3)}]",
                        lesson.IsPracticalLesson ? ConsoleColor.Red : ConsoleColor.Blue
                    ));
            }

            output.Add(new ColoredString("\n"));
        }

        return output;

        string GetTimetableHeader()
        {
            StringBuilder builder = new("             ");
            for (var i = 0; i < 9; i++)
            {
                builder.Append($"{i + 1}     ");
            }

            return builder.ToString();
        }
    }
}