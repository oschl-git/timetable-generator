using System.Text;
using TimetableGenerator.Helpers;

namespace TimetableGenerator.TimetableGeneration.Entities;

public class TimetableEntity
{
    public Dictionary<string, LessonEntity?[]> Days { get; } = new()
    {
        { "Monday", new LessonEntity?[9] },
        { "Tuesday", new LessonEntity?[9] },
        { "Wednesday", new LessonEntity?[9] },
        { "Thursday", new LessonEntity?[9] },
        { "Friday", new LessonEntity?[9] },
    };

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

    public List<ColoredString> ToColoredStringArray()
    {
        var output = new List<ColoredString>();
        foreach (var (day, lessons) in Days)
        {
            output.Add(new ColoredString($"{day}:"));
            foreach (var lesson in lessons)
            {
                output.Add(lesson == null
                    ? new ColoredString(" .", ConsoleColor.Gray)
                    : new ColoredString($" {lesson.Subject}",
                        lesson.IsPracticalLesson ? ConsoleColor.Red : ConsoleColor.Blue));
            }

            output.Add(new ColoredString("\n"));
        }

        return output;
    }
}