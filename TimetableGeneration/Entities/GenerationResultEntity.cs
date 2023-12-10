using TimetableGenerator.Collections;
using TimetableGenerator.Helpers;

namespace TimetableGenerator.TimetableGeneration.Entities;

public class GenerationResultEntity
{
    private int resultsToKeep;
    
    public Queue<TimetableEntity> unevaluatedTimetables { get; } = new();
    public OrderedList<EvaluatedTimetableEntity> evaluatedTimetables { get; } = new();
    public int GeneratedTimetablesCount { set; get; }
    public int EvaluatedTimetablesCount { set; get; }

    private readonly object locker = new();

    public GenerationResultEntity(int resultsToKeep)
    {
        this.resultsToKeep = resultsToKeep;
    }

    public void EnqueueUnevaluatedTimetable(TimetableEntity timetable)
    {
        lock (locker)
        {
            GeneratedTimetablesCount++;
            
            unevaluatedTimetables.Enqueue(timetable);
        }
    }

    public TimetableEntity? DequeueUnevaluatedTimetable()
    {
        lock (locker)
        {
            return unevaluatedTimetables.Count <= 0 ? null : unevaluatedTimetables.Dequeue();
        }
    }

    public void AddEvaluatedTimetable(EvaluatedTimetableEntity evaluatedTimetable)
    {
        lock (locker)
        {
            EvaluatedTimetablesCount++;
            
            if (evaluatedTimetables.Count > 0 && evaluatedTimetable.Score <= evaluatedTimetables[^1].Score) return;
            
            evaluatedTimetables.Add(evaluatedTimetable);

            if (evaluatedTimetables.Count > resultsToKeep)
            {
                evaluatedTimetables.RemoveAt(evaluatedTimetables.Count - 1);
            }
        }
    }

    public virtual IEnumerable<ColoredString> ToColoredStringList()
    {
        var output = new List<ColoredString>();

        foreach (var timetable in evaluatedTimetables)
        {
            output.AddRange(timetable.ToColoredStringList());
            output.Add(new ColoredString("\n"));
        }
        
        output.Add(new ColoredString("\n"));
        output.Add(new ColoredString("Total generated timetables: "));
        output.Add(new ColoredString(GeneratedTimetablesCount.ToString(), ConsoleColor.Red));
        
        output.Add(new ColoredString("\n"));
        output.Add(new ColoredString("Total evaluated timetables: "));
        output.Add(new ColoredString(EvaluatedTimetablesCount.ToString(), ConsoleColor.Green));
            
        return output;
    }
}