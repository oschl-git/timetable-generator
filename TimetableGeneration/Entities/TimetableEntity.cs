namespace TimetableGenerator.TimetableGeneration.Entities;

public class TimetableEntity
{
    public Dictionary<string, LessonEntity?[]> Days { set; get; } = new()
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
                if (lastItemWasNull)
                {
                    return length - 1;
                }
                lastItemWasNull = true;
            }

            length++;
        }

        return length;
    }
}