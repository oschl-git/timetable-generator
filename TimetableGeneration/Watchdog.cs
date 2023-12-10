using TimetableGenerator.Helpers;

namespace TimetableGenerator.TimetableGeneration;

/// <summary>
/// An abstract class which starts and shuts down ThreadManager threads after a specified amount of time.
/// </summary>
public abstract class Watchdog
{
    /// <summary>
    /// Starts and shuts down ThreadManager threads after a specified amount of time.
    /// </summary>
    /// <param name="secondsToLive">How many seconds to generate for.</param>
    /// <param name="threadManager">Reference to the thread manager which should be started and stopped.</param>
    public static void Watch(int secondsToLive, ThreadManager threadManager)
    {
        threadManager.Running = true;
        
        var secondsLeft = secondsToLive;
        for (var i = 0; i < secondsToLive; i++)
        {
            ColoredConsole.Write("Generating... ");
            ColoredConsole.Write(secondsLeft.ToString(), ConsoleColor.Magenta);
            ColoredConsole.WriteLine(" seconds remaining.");
            
            Thread.Sleep(1000);
            secondsLeft--;
        }

        threadManager.Running = false;
    }
}