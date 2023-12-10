using TimetableGenerator;
using TimetableGenerator.Helpers;

var threadManager = new ThreadManager(10);
var result = threadManager.ProcessTimetableGeneration();
ColoredConsole.WriteArray(result.ToColoredStringList());