using TimetableGenerator.Helpers;
using TimetableGenerator.TimetableGeneration.Entities;

namespace TimetableGenerator.TimetableGeneration;

public class Generator
{
    public static TimetableEntity GeneratePossibleTimetable()
    {
        var timetable = new TimetableEntity();
        var subjects = CollectionRandomizer.ShuffleDictionary(Subjects.LessonFrequency);
        var random = new Random();
        foreach (var (lesson, frequency) in Subjects.LessonFrequency)
        {
            for (var i = 0; i < frequency; i++)
            {
                var lessonAssigned = false;
                while (!lessonAssigned)
                {
                    var index = random.Next(0, 4);
                    if (timetable.GetDayLengthByIndex(index) > 9) continue;

                    if (timetable.GetDayLengthByIndex(index) == 6)
                    {
                        timetable.Days.ElementAt(index).Value[timetable.GetDayLengthByIndex(index) + 1] = lesson;
                        lessonAssigned = true;
                    }
                    else
                    {
                        timetable.Days.ElementAt(index).Value[timetable.GetDayLengthByIndex(index)] = lesson;
                        lessonAssigned = true;
                    }
                }
            }
        }

        return timetable;
    }
}