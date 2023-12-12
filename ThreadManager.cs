using TimetableGenerator.TimetableGeneration;
using TimetableGenerator.TimetableGeneration.Entities;

namespace TimetableGenerator;

/// <summary>
/// Manages threads for multi-threaded generation of timetables.
/// </summary>
public class ThreadManager
{
    public GenerationResultEntity Result { get; }
    public bool Running { get; set; }
    
    private List<Thread> threads;
    private int secondsToLive;
    
    /// <summary>
    /// Constructs the ThreadManager.
    /// </summary>
    /// <param name="secondsToLive">How long to generate for.</param>
    /// <param name="resultsToKeep">How many of the best results to keep and include in the result.</param>
    public ThreadManager(int secondsToLive = 60, int resultsToKeep = 100)
    {
        threads = new List<Thread>();
        this.secondsToLive = secondsToLive;
        Result = new GenerationResultEntity(resultsToKeep);
        CreateThreads();
    }
    
    private void CreateThreads()
    {
        var evaluatorThreadCount = Environment.ProcessorCount / 3;
        var generatorThreadCount = Environment.ProcessorCount - evaluatorThreadCount;
        
        if (evaluatorThreadCount < 1) evaluatorThreadCount = 1;
        if (generatorThreadCount < 1) generatorThreadCount = 1;
        
        threads.Add(new Thread(() =>
        {
            Watchdog.Watch(secondsToLive, this);
        }));

        for (var i = 0; i < generatorThreadCount; i++)
        {
            threads.Add(new Thread(() =>
            {
                while (Running)
                {
                    Result.EnqueueUnevaluatedTimetable(Generator.GeneratePossibleTimetable());
                }
            }));
        }

        for (var i = 0; i < evaluatorThreadCount; i++)
        {
            threads.Add(new Thread(() =>
            {
                while (Running)
                {
                    var timetable = Result.DequeueUnevaluatedTimetable();
                    if (timetable == null) continue;
                    Result.AddEvaluatedTimetable(
                        new EvaluatedTimetableEntity(timetable, Evaluator.EvaluateTimetable(timetable))
                    );
                }
            }));
        }
    }

    /// <summary>
    /// Processes timetable generation for amount of seconds specified in the class constructor.
    /// </summary>
    /// <returns>GenerationResultEntity with information about what was generated.</returns>
    public GenerationResultEntity ProcessTimetableGeneration()
    {
        foreach (var thread in threads) thread.Start();
        foreach (var thread in threads) thread.Join();

        return Result;
    }
}