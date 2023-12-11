# TimetableGenerator
A multi-theaded console application for generating timetables for a given set of lessons. In seconds, it can generate millions of timetables, which are then processed by the Evaluator which assigns each timetable a corresponding score based on defined criteria. The best timetables are printed to the console, along with other useful information.

## How to run?
The application is written in C# using .NET 7.0. Run a development version with `dotnet run` or make a release build for your operating system. For example, if you want to create a x64 self-contained release Linux build, run `dotnet publish -c Release -r linux-x64 --self-contained`.

## Tools used:
- .NET 7.0
- JetBrains Rider
- NUnit
