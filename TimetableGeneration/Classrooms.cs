namespace TimetableGenerator.TimetableGeneration;

/// <summary>
/// An abstract class with information about classrooms.
/// </summary>
public abstract class Classrooms
{
    public static Dictionary<string, int> ClassroomsAndFloors { get; } = new()
    {
        { "25", 4 },
        { "19c", 3 },
        { "8a", 2 },
        { "29", 4 },
        { "26", 4 },
        { "5b", 1 },
        { "TV", 0 },
        { "19", 3 },
        { "18", 3 },
        { "17b", 3 },
    };
}