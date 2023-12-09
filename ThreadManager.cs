namespace TimetableGenerator;

public class ThreadManager
{
    private List<Thread> threads;
    private int liveForSeconds;

    public ThreadManager(int liveForSeconds = 60)
    {
        threads = new List<Thread>();
        this.liveForSeconds = liveForSeconds;
        
        CreateThreads();
    }

    private void CreateThreads()
    {
        var generatorThreadCount = Environment.ProcessorCount / 3 - 1;
        var evaluatorThreadCount = Environment.ProcessorCount - generatorThreadCount;

        for (var i = 0; i < generatorThreadCount; i++) {
            threads.Add(new Thread(() =>
            {
                
            }));
        }
        
        for (var i = 0; i < evaluatorThreadCount; i++) {
            
        }
        
        Console.WriteLine(threads.Count);
    }
}