using TimetableGenerator.TimetableGeneration.Entities;

namespace TimetableGenerator.TimetableGeneration;

public abstract class Evaluator
{
    public static int EvaluateTimetable(TimetableEntity timetable)
    {
        var score = 10000;

        score += RateTakenTimetableFields(timetable);
        score += PenaliseRepeatedNonconsecutiveLessons(timetable);

        return score;
    }

    private static int RateTakenTimetableFields(TimetableEntity timetable)
    {
        var score = 0;
        foreach (var day in timetable.Days.Values)
        {
            if (day[8] != null) score -= 200;
            else if (day[6] != null) score -= 50;
            else if (day[7] != null) score -= 100;
        }

        // Special Friday penalty for afternoon lessons:
        var friday = timetable.Days.Values.ElementAt(4);
        if (friday[8] != null) score -= 500;
        else if (friday[6] != null) score -= 200;
        else if (friday[7] != null) score -= 300;

        return score;
    }

    private static int PenaliseRepeatedNonconsecutiveLessons(TimetableEntity timetable)
    {
        var score = 0;

        foreach (var day in timetable.Days.Values)
        {
            LessonEntity? lastLesson = null;
            List<LessonEntity> lessonsHad = new();

            foreach (var lesson in day)
            {
                if (lesson == null) continue;
                if (lessonsHad.Contains(lesson) && lastLesson != lesson) score -= 150;
                
                lessonsHad.Add(lesson);
                lastLesson = lesson;
            }
        }

        return score;
    }
}