using TimetableGenerator.Helpers;
using TimetableGenerator.TimetableGeneration.Entities;

namespace TimetableGenerator.TimetableGeneration;

public class Generator
{
    public static TimetableEntity GeneratePossibleTimetable()
    {
        var timetable = new TimetableEntity();
        var timetableEntries = CollectionRandomizer.ShuffleList(Subjects.GetTimetableEntries());
        var random = new Random();
        foreach (var entry in timetableEntries)
        {
            var lessonAssigned = false;
            while (!lessonAssigned)
            {
                var index = random.Next(0, 5);
                if (timetable.GetDayLengthByIndex(index) > 7) continue;

                foreach (var lesson in entry)
                {
                    var includeLunchBreak = timetable.GetDayLengthByIndex(index) == 6;
                    var dayLength = timetable.GetDayLengthByIndex(index);
                    
                    if (includeLunchBreak)
                    {
                        timetable.Days.ElementAt(index).Value[dayLength + 1] = lesson;
                    }
                    else
                    {
                        timetable.Days.ElementAt(index).Value[dayLength] = lesson;
                    }
                }
                lessonAssigned = true;
            }
        }

        return timetable;
    }
}