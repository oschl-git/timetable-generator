namespace TimetableGenerator.TimetableGeneration.Entities;

public class GenerationResultEntity
{
    public Queue<TimetableEntity> unevaluatedTimetables { get; set; } = new();
    public SortedList<int, TimetableEntity> evaluatedTimetables { get; set; } = new();

    public void AddUnevaluatedTimetable(TimetableEntity timetable)
    {
        this.unevaluatedTimetables.
    }
}