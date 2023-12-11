using TimetableGenerator.Collections;
using TimetableGenerator.Helpers;

namespace TimetableGenerator.TimetableGeneration.Entities;

/// <summary>
/// Contains all relevant results of timetable generation.
/// </summary>
public class GenerationResultEntity
{
    private int resultsToKeep;
    
    public Queue<TimetableEntity> unevaluatedTimetables { get; } = new();
    public OrderedList<EvaluatedTimetableEntity> evaluatedTimetables { get; } = new();
    public int GeneratedTimetablesCount { set; get; }
    public int EvaluatedTimetablesCount { set; get; }
    public int TimetablesBetterThanOriginalCount { set; get; }
    
    private int originalTimetableScore = new OriginalTimetableEntity().Score;

    private readonly object locker = new();

    public GenerationResultEntity(int resultsToKeep)
    {
        this.resultsToKeep = resultsToKeep;
    }

    /// <summary>
    /// Adds an unevaluated timetable to the queue.
    /// </summary>
    /// <param name="timetable">The timetable to add.</param>
    public void EnqueueUnevaluatedTimetable(TimetableEntity timetable)
    {
        lock (locker)
        {
            GeneratedTimetablesCount++;
            
            unevaluatedTimetables.Enqueue(timetable);
        }
    }

    /// <summary>
    /// Removes a timetable from the unevaluated timetables queue and returns it.
    /// </summary>
    /// <returns>The unevaluated timetable.</returns>
    public TimetableEntity? DequeueUnevaluatedTimetable()
    {
        lock (locker)
        {
            return unevaluatedTimetables.Count <= 0 ? null : unevaluatedTimetables.Dequeue();
        }
    }

    /// <summary>
    /// Adds an evaluated timetable to its correct sorted position in the evaluated timetables OrderedList.
    /// Ensures the list doesn't include more items than specified in the resultsToKeep property.
    /// </summary>
    /// <param name="evaluatedTimetable">The evaluated timetable to add.</param>
    public void AddEvaluatedTimetable(EvaluatedTimetableEntity evaluatedTimetable)
    {
        lock (locker)
        {
            EvaluatedTimetablesCount++;
            if (evaluatedTimetable.Score > originalTimetableScore) TimetablesBetterThanOriginalCount++;
            
            if (evaluatedTimetables.Count > 0 && evaluatedTimetable.Score <= evaluatedTimetables[^1].Score) return;
            
            evaluatedTimetables.Add(evaluatedTimetable);

            if (evaluatedTimetables.Count > resultsToKeep)
            {
                evaluatedTimetables.RemoveAt(evaluatedTimetables.Count - 1);
            }
        }
    }

    /// <summary>
    /// Converts the result into a pretty and printable ColoredString List.
    /// </summary>
    /// <returns>A List of ColoredStrings.</returns>
    public virtual IEnumerable<ColoredString> ToColoredStringList()
    {
        var output = new List<ColoredString>();

        var reversedList = new List<TimetableEntity>(evaluatedTimetables);
        reversedList.Reverse();
        
        foreach (var timetable in reversedList)
        {
            output.AddRange(timetable.ToColoredStringList());
            output.Add(new ColoredString("\n"));
        }
        
        output.Add(new ColoredString("The timetable right above is the best generated one. Scroll up to see more options.\n", ConsoleColor.Cyan));
        output.Add(new ColoredString("Blue lessons are theoretical", ConsoleColor.Blue));
        output.Add(new ColoredString(", "));
        output.Add(new ColoredString("red lessons are practical.\n", ConsoleColor.Red));
        
        output.Add(new ColoredString("\n"));
        output.Add(new ColoredString("Total generated timetables: "));
        output.Add(new ColoredString(GeneratedTimetablesCount.ToString(), ConsoleColor.Red));
        
        output.Add(new ColoredString("\n"));
        output.Add(new ColoredString("Total evaluated timetables: "));
        output.Add(new ColoredString(EvaluatedTimetablesCount.ToString(), ConsoleColor.Green));
        
        output.Add(new ColoredString("\n"));
        output.Add(new ColoredString("Timetables better than original (current): "));
        output.Add(new ColoredString(TimetablesBetterThanOriginalCount.ToString(), ConsoleColor.Magenta));
            
        return output;
    }
}