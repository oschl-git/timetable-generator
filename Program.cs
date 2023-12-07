using TimetableGenerator.Helpers;
using TimetableGenerator.TimetableGeneration;
using TimetableGenerator.TimetableGeneration.Entities;

for (var i = 0; i < 10; i++)
{
    ColoredConsole.WriteArray(Generator.GeneratePossibleTimetable().ToColoredStringArray());
    ColoredConsole.WriteLine();
}