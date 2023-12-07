namespace TimetableGenerator.TimetableGeneration.Entities;

public class LessonEntity
{
    public Subjects.SubjectTypes Subject { get; }
    public string Teacher { get; }
    public string Classroom { get; }
    public bool IsPracticalLesson { get; }

    public LessonEntity(Subjects.SubjectTypes subject, string teacher, string classroom, bool isPracticalLesson)
    {
        Subject = subject;
        Teacher = teacher;
        Classroom = classroom;
        IsPracticalLesson = isPracticalLesson;
    }
}