using TimetableGenerator.TimetableGeneration.Entities;

namespace TimetableGenerator.TimetableGeneration;

/// <summary>
/// An abstract class which evaluates timetables and gives each a corresponding score.
/// </summary>
public abstract class Evaluator
{
    /// <summary>
    /// Evaluates a timetable.
    /// </summary>
    /// <param name="timetable">The timetable to evaluate.</param>
    /// <returns>The score given to the timetable.</returns>
    public static int EvaluateTimetable(TimetableEntity timetable)
    {
        var score = 1000;

        score += RateTakenTimetableFields(timetable);
        score += PenaliseRepeatedNonconsecutiveLessons(timetable);
        score += RateMovingBetweenClassrooms(timetable);
        score += RateLunchBreaks(timetable);
        score += RateDayLengths(timetable);
        score += PenaliseImportantSubjectsAsFirstHours(timetable);
        score += PenaliseMultipleDifficultSubjectsInDay(timetable);
        score += PenaliseMultipleHourBreaks(timetable);
        score += PenaliseMultipleBreaksInADay(timetable);
        score += PenaliseConsecutiveDifficultSubjects(timetable);
        score += RewardFreeFirstHours(timetable);
        score += RewardNoAfternoonLessons(timetable);

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
                if (lesson == null)
                {
                    lastLesson = null;
                    continue;
                }

                if (lessonsHad.Contains(lesson) && lastLesson != lesson)
                {
                    if (lesson.IsPracticalLesson) score -= 400;
                    else score -= 70;
                }

                lessonsHad.Add(lesson);
                lastLesson = lesson;
            }
        }

        return score;
    }

    private static int RateMovingBetweenClassrooms(TimetableEntity timetable)
    {
        var score = 0;

        foreach (var day in timetable.Days.Values)
        {
            LessonEntity? lastLesson = null;
            foreach (var lesson in day)
            {
                if (lesson == null)
                {
                    lastLesson = null;
                    continue;
                }

                if (lesson == lastLesson)
                {
                    score += 10;
                }
                else if (lastLesson != null)
                {
                    var floorDifference = Math.Abs(
                        Classrooms.ClassroomsAndFloors[lesson.Classroom] -
                        Classrooms.ClassroomsAndFloors[lastLesson.Classroom]
                    );

                    switch (floorDifference)
                    {
                        case 1:
                            score -= 10;
                            break;
                        case 2:
                            score -= 20;
                            break;
                        case >= 3:
                            score -= 50;
                            break;
                    }
                }

                lastLesson = lesson;
            }
        }

        return score;
    }

    private static int RateLunchBreaks(TimetableEntity timetable)
    {
        var score = 0;

        foreach (var day in timetable.Days.Values)
        {
            if (day[5] != null && day[6] != null && day[7] != null)
            {
                score -= 1000;
            }
            else if (day[5] == null) // Bonus for best lunch break hour
            {
                score += 250;
            }
        }

        return score;
    }

    private static int RateDayLengths(TimetableEntity timetable)
    {
        var score = 0;

        for (var i = 0; i < timetable.Days.Values.Count; i++)
        {
            switch (timetable.GetDayLengthByIndex(i))
            {
                case < 5:
                    score -= 100;
                    break;
                case 5:
                    score += 100;
                    break;
                case 6:
                    score += 80;
                    break;
                case 8:
                    score -= 100;
                    break;
                case 9:
                    score -= 300;
                    break;
            }
        }

        return score;
    }

    private static int PenaliseImportantSubjectsAsFirstHours(TimetableEntity timetable)
    {
        var score = 0;

        Subjects.SubjectTypes[] importantSubjects =
        {
            Subjects.SubjectTypes.M,
            Subjects.SubjectTypes.C,
            Subjects.SubjectTypes.PV,
        };

        foreach (var day in timetable.Days.Values)
        {
            var wasLastLessonNull = true;
            foreach (var lesson in day)
            {
                if (lesson != null && wasLastLessonNull && importantSubjects.Contains(lesson.Subject))
                {
                    score -= 50;
                }

                wasLastLessonNull = lesson == null;
            }
        }

        return score;
    }

    private static int PenaliseMultipleDifficultSubjectsInDay(TimetableEntity timetable)
    {
        var score = 0;

        Subjects.SubjectTypes[] difficultSubjects =
        {
            Subjects.SubjectTypes.M,
            Subjects.SubjectTypes.AM,
            Subjects.SubjectTypes.DS,
            Subjects.SubjectTypes.PSS,
        };

        foreach (var day in timetable.Days.Values)
        {
            var difficultSubjectCount
                = day.Count(lesson => lesson != null && difficultSubjects.Contains(lesson.Subject));
            switch (difficultSubjectCount)
            {
                case 3:
                    score -= 100;
                    break;
                case 4:
                    score -= 200;
                    break;
                case >= 5:
                    score -= 300;
                    break;
            }
        }

        return score;
    }

    private static int PenaliseMultipleHourBreaks(TimetableEntity timetable)
    {
        var score = 0;

        foreach (var day in timetable.Days.Values)
        {
            var consecutiveNullHourCount = 0;
            foreach (var lesson in day)
            {
                if (lesson == null) consecutiveNullHourCount++;
                else
                {
                    if (consecutiveNullHourCount >= 2)
                    {
                        score -= 1000;
                        return score;
                    }

                    consecutiveNullHourCount = 0;
                }
            }
        }

        return score;
    }

    private static int PenaliseMultipleBreaksInADay(TimetableEntity timetable)
    {
        var score = 0;

        foreach (var day in timetable.Days.Values)
        {
            var breakCount = 0;
            for (var i = 0; i < day.Length - 1; i++)
            {
                if (day.ElementAt(i) == null && day.ElementAt(i + 1) != null) breakCount++;
            }

            if (breakCount > 1) score -= 1000;
        }

        return score;
    }

    private static int PenaliseConsecutiveDifficultSubjects(TimetableEntity timetable)
    {
        Subjects.SubjectTypes[] difficultSubjects =
        {
            Subjects.SubjectTypes.M,
            Subjects.SubjectTypes.AM,
            Subjects.SubjectTypes.DS,
            Subjects.SubjectTypes.PSS,
        };

        var score = 0;

        foreach (var day in timetable.Days.Values)
        {
            LessonEntity? lastLesson = null;
            List<LessonEntity> lessonsHad = new();

            foreach (var lesson in day)
            {
                if (lesson == null) continue;
                if (difficultSubjects.Contains(lesson.Subject) && lastLesson == lesson) score -= 100;

                lessonsHad.Add(lesson);
                lastLesson = lesson;
            }
        }

        return score;
    }

    private static int RewardFreeFirstHours(TimetableEntity timetable)
    {
        var score = 0;

        foreach (var day in timetable.Days.Values)
        {
            if (day[0] == null && day[1] == null)
            {
                score += 230;
            }

            if (day[0] == null)
            {
                score += 110;
            }
        }

        return score;
    }

    private static int RewardNoAfternoonLessons(TimetableEntity timetable)
    {
        var score = 0;

        foreach (var day in timetable.Days.Values)
        {
            if (day[7] == null && day[8] == null)
            {
                score += 340;
            }
        }

        return score;
    }
}