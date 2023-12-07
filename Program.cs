using TimetableGenerator;
using TimetableGenerator.Helpers;
using TimetableGenerator.TimetableGeneration;
using TimetableGenerator.TimetableGeneration.Entities;

for (var i = 0; i < 10; i++)
{
    var timetable = Generator.GeneratePossibleTimetable();
    ColoredConsole.WriteArray(timetable.ToColoredStringList());
    ColoredConsole.WriteLine("Rating: " + Evaluator.EvaluateTimetable(timetable), ConsoleColor.Green);
    ColoredConsole.WriteLine();
}