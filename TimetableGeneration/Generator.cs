using TimetableGenerator.Helpers;
using TimetableGenerator.TimetableGeneration.Entities;

namespace TimetableGenerator.TimetableGeneration;

/// <summary>
/// An abstract class which generates timetables.
/// </summary>
public abstract class Generator
{
    /// <summary>
    /// Generates a random timetable.
    /// </summary>
    /// <returns>The timetable.</returns>
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
                // Choose random day
                var index = random.Next(0, 5);
                // Skip if day would be too long
                if (timetable.GetDayLengthByIndex(index) > 7) continue;

                foreach (var lesson in entry)
                {
                    var dayLength = timetable.GetDayLengthByIndex(index);

                    var includeLunchBreak = timetable.GetDayLengthByIndex(index) == 6;
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

        // If there's only 7 hours in a day, move the last lesson to lunch break
        for (var i = 0; i < 5; i++)
        {
            if (timetable.GetDayLengthByIndex(i) == 8 && timetable.Days.ElementAt(i).Value[8] == null)
            {
                timetable.Days.ElementAt(i).Value[6] = timetable.Days.ElementAt(i).Value[7];
                timetable.Days.ElementAt(i).Value[7] = null;
            }
        }

        // Introduce more randomness by swapping random fields
        for (var i = random.Next(0, 7); i < 7; i++)
        {
            var day = random.Next(0, 5);
            var hour = random.Next(0, 9);
            var newHour = random.Next(0, 9);

            (timetable.Days.ElementAt(day).Value[newHour], timetable.Days.ElementAt(day).Value[hour]) =
                (timetable.Days.ElementAt(day).Value[hour], timetable.Days.ElementAt(day).Value[newHour]);
        }

        return timetable;
    }
}