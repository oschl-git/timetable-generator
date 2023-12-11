using TimetableGenerator;
using TimetableGenerator.Helpers;

var secondsToLiveInput = ColoredConsole.GetInputFromUser(
    "How many seconds should the generator run for: ",
    new Func<string?, (bool, string?)>[]
    {
        input =>
        {
            var inputIsOk = int.TryParse(input, out _) && int.Parse(input) > 0;
            return (inputIsOk, "Value must be a positive integer.");
        }
    },
    "Try again: "
);

if (secondsToLiveInput == null || !int.TryParse(secondsToLiveInput, out _))
{
    throw new ArgumentException("The user was allowed to input a disallowed value.");
}
var secondsToLive = int.Parse(secondsToLiveInput);

var resultsToKeepInput = ColoredConsole.GetInputFromUser(
    "How many of the best results should be kept: ",
    new Func<string?, (bool, string?)>[]
    {
        input =>
        {
            var inputIsOk = int.TryParse(input, out _) && int.Parse(input) > 0;
            return (inputIsOk, "Value must be a positive integer.");
        }
    },
    "Try again: "
);

if (resultsToKeepInput == null || !int.TryParse(resultsToKeepInput, out _))
{
    throw new ArgumentException("The user was allowed to input a disallowed value.");
}
var resultsToKeep = int.Parse(resultsToKeepInput);

var threadManager = new ThreadManager(secondsToLive, resultsToKeep);
var result = threadManager.ProcessTimetableGeneration();
ColoredConsole.WriteArray(result.ToColoredStringList());