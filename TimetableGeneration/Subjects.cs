using TimetableGenerator.TimetableGeneration.Entities;

namespace TimetableGenerator.TimetableGeneration;

public abstract class Subjects
{
    public enum SubjectTypes
    {
        M,
        DS,
        PSS,
        A,
        TV,
        PIS,
        TP,
        C,
        CIT,
        WA,
        PV,
        AM,
    }

    public static Dictionary<LessonEntity, int> LessonFrequency { get; } = new()
    {
        { new LessonEntity(SubjectTypes.M, "Libuše Hrabalová", "25", false), 4 },
        { new LessonEntity(SubjectTypes.DS, "Antonín Vobecký", "25", false), 1 },
        { new LessonEntity(SubjectTypes.DS, "Antonín Vobecký", "19c", true), 2 },
        { new LessonEntity(SubjectTypes.PSS, "Lukáš Masopust", "25", false), 1 },
        { new LessonEntity(SubjectTypes.PSS, "Lukáš Masopust", "8a", true), 2 },
        { new LessonEntity(SubjectTypes.A, "Šárka Páltiková", "29", false), 2 },
        { new LessonEntity(SubjectTypes.A, "Šárka Páltiková", "5b", false), 1 },
        { new LessonEntity(SubjectTypes.A, "Šárka Páltiková", "26", false), 1 },
        { new LessonEntity(SubjectTypes.TV, "Leopold Lopocha", "TV", false), 2 },
        { new LessonEntity(SubjectTypes.PIS, "Lucie Brčáková", "25", false), 2 },
        { new LessonEntity(SubjectTypes.PIS, "Lucie Brčáková", "19", true), 2 },
        { new LessonEntity(SubjectTypes.TP, "Lukáš Masopust", "25", false), 1 },
        { new LessonEntity(SubjectTypes.C, "Kristina Studénková", "25", false), 3 },
        { new LessonEntity(SubjectTypes.CIT, "Jakub Mazuch", "17b", true), 2 },
        { new LessonEntity(SubjectTypes.WA, "Daniel Adámek", "25", false), 1 },
        { new LessonEntity(SubjectTypes.WA, "Simona Hemžalová", "19", true), 2 },
        { new LessonEntity(SubjectTypes.PV, "Alena Reichlová", "25", false), 1 },
        { new LessonEntity(SubjectTypes.PV, "Alena Reichlová", "18", true), 2 },
        { new LessonEntity(SubjectTypes.AM, "Filip Kallmünzer", "25", false), 2 },
    };

    public static IEnumerable<LessonEntity[]> GetTimetableEntries()
    {
        var entries = new List<LessonEntity[]>();
        foreach (var (lesson, frequency) in LessonFrequency)
        {
            if (lesson.IsPracticalLesson)
            {
                entries.Add(Enumerable.Repeat(lesson, frequency).ToArray());
            }
            else
            {
                for (var i = 0; i < frequency; i++) entries.Add(new[]{lesson});
            }
        }
        return entries;
    }
}