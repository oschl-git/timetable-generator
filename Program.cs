using TimetableGenerator;
using TimetableGenerator.Helpers;

var threadManager = new ThreadManager(60);
var result = threadManager.ProcessTimetableGeneration();
ColoredConsole.WriteArray(result.ToColoredStringList());