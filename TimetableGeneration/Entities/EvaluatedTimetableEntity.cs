using TimetableGenerator.Helpers;

namespace TimetableGenerator.TimetableGeneration.Entities;

/// <summary>
/// An extension of TimetableEntity which includes a score.
/// </summary>
public class EvaluatedTimetableEntity : TimetableEntity, IComparable<EvaluatedTimetableEntity>
{
    public int Score { get; }

    public EvaluatedTimetableEntity(TimetableEntity timetable, int score)
    {
        Days = timetable.Days;
        Score = score;
    }
    
    public int CompareTo(EvaluatedTimetableEntity? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return other.Score.CompareTo(Score);
    }

    /// <summary>
    /// Converts the timetable into a pretty and printable ColoredString List which includes the score.
    /// </summary>
    /// <returns>A List of ColoredStrings.</returns>
    public override IEnumerable<ColoredString> ToColoredStringList()
    {
        return base.ToColoredStringList().Append(new ColoredString($"Score: {Score}", ConsoleColor.Magenta));
    }
}