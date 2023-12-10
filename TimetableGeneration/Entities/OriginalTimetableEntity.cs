namespace TimetableGenerator.TimetableGeneration.Entities;

/// <summary>
/// Represents the original (current) timetable.
/// </summary>
public class OriginalTimetableEntity : EvaluatedTimetableEntity
{
    public OriginalTimetableEntity() : base(new TimetableEntity(), 0)
    {
        // Monday:
        Days.ElementAt(0).Value[0] = new LessonEntity(Subjects.SubjectTypes.M, "Libuše Hrabalová", "25", false);
        Days.ElementAt(0).Value[1] = new LessonEntity(Subjects.SubjectTypes.DS, "Antonín Vobecký", "19c", true);
        Days.ElementAt(0).Value[2] = new LessonEntity(Subjects.SubjectTypes.DS, "Antonín Vobecký", "19c", true);
        Days.ElementAt(0).Value[3] = new LessonEntity(Subjects.SubjectTypes.PSS, "Lukáš Masopust", "8a", true);
        Days.ElementAt(0).Value[4] = new LessonEntity(Subjects.SubjectTypes.PSS, "Lukáš Masopust", "8a", true);
        Days.ElementAt(0).Value[5] = new LessonEntity(Subjects.SubjectTypes.A, "Šárka Páltiková", "29", false);
        Days.ElementAt(0).Value[7] = new LessonEntity(Subjects.SubjectTypes.TV, "Leopold Lopocha", "TV", false);
        
        // Tuesday:
        Days.ElementAt(1).Value[0] = new LessonEntity(Subjects.SubjectTypes.PIS, "Lucie Brčáková", "25", false);
        Days.ElementAt(1).Value[1] = new LessonEntity(Subjects.SubjectTypes.M, "Libuše Hrabalová", "25", false);
        Days.ElementAt(1).Value[2] = new LessonEntity(Subjects.SubjectTypes.PIS, "Lucie Brčáková", "19", true);
        Days.ElementAt(1).Value[3] = new LessonEntity(Subjects.SubjectTypes.PIS, "Lucie Brčáková", "19", true);
        Days.ElementAt(1).Value[4] = new LessonEntity(Subjects.SubjectTypes.TP, "Lukáš Masopust", "25", false);
        Days.ElementAt(1).Value[5] = new LessonEntity(Subjects.SubjectTypes.A, "Šárka Páltiková", "5b", false);
        Days.ElementAt(1).Value[6] = new LessonEntity(Subjects.SubjectTypes.C, "Kristina Studénková", "25", false);

        // Wednesday:
        Days.ElementAt(2).Value[0] = new LessonEntity(Subjects.SubjectTypes.CIT, "Jakub Mazuch", "17b", true);
        Days.ElementAt(2).Value[1] = new LessonEntity(Subjects.SubjectTypes.CIT, "Jakub Mazuch", "17b", true);
        Days.ElementAt(2).Value[2] = new LessonEntity(Subjects.SubjectTypes.WA, "Daniel Adámek", "25", false);
        Days.ElementAt(2).Value[3] = new LessonEntity(Subjects.SubjectTypes.DS, "Antonín Vobecký", "25", false);
        Days.ElementAt(2).Value[4] = new LessonEntity(Subjects.SubjectTypes.PV, "Alena Reichlová", "25", false);
        Days.ElementAt(2).Value[6] = new LessonEntity(Subjects.SubjectTypes.PSS, "Lukáš Masopust", "25", false);
        
        // Thursday:
        Days.ElementAt(3).Value[0] = new LessonEntity(Subjects.SubjectTypes.AM, "Filip Kallmünzer", "25", false);
        Days.ElementAt(3).Value[1] = new LessonEntity(Subjects.SubjectTypes.M, "Libuše Hrabalová", "25", false);
        Days.ElementAt(3).Value[2] = new LessonEntity(Subjects.SubjectTypes.WA, "Simona Hemžalová", "19", true);
        Days.ElementAt(3).Value[3] = new LessonEntity(Subjects.SubjectTypes.WA, "Simona Hemžalová", "19", true);
        Days.ElementAt(3).Value[5] = new LessonEntity(Subjects.SubjectTypes.A, "Šárka Páltiková", "26", false);
        Days.ElementAt(3).Value[6] = new LessonEntity(Subjects.SubjectTypes.C, "Kristina Studénková", "25", false);
        Days.ElementAt(3).Value[7] = new LessonEntity(Subjects.SubjectTypes.PIS, "Lucie Brčáková", "25", false);
        Days.ElementAt(3).Value[8] = new LessonEntity(Subjects.SubjectTypes.TV, "Leopold Lopocha", "TV", false);
        
        // Friday:
        Days.ElementAt(4).Value[0] = new LessonEntity(Subjects.SubjectTypes.C, "Kristina Studénková", "25", false);
        Days.ElementAt(4).Value[1] = new LessonEntity(Subjects.SubjectTypes.A, "Šárka Páltiková", "29", false);
        Days.ElementAt(4).Value[2] = new LessonEntity(Subjects.SubjectTypes.M, "Libuše Hrabalová", "25", false);
        Days.ElementAt(4).Value[3] = new LessonEntity(Subjects.SubjectTypes.PV, "Alena Reichlová", "18", true);
        Days.ElementAt(4).Value[4] = new LessonEntity(Subjects.SubjectTypes.PV, "Alena Reichlová", "18", true);
        Days.ElementAt(4).Value[5] = new LessonEntity(Subjects.SubjectTypes.AM, "Filip Kallmünzer", "25", false);

        Score = Evaluator.EvaluateTimetable(this);
    }
}