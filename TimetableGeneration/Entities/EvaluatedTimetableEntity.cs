using TimetableGenerator.Helpers;

namespace TimetableGenerator.TimetableGeneration.Entities;

public class EvaluatedTimetableEntity : TimetableEntity, IComparable<EvaluatedTimetableEntity>
{
    public int Score { get; }


    public EvaluatedTimetableEntity(TimetableEntity timetable, int score)
    {
        Days = timetable.Days;
        Score = score;
    }

    public int CompareTo(object? obj)
    {
        throw new NotImplementedException();
    }

    public int CompareTo(EvaluatedTimetableEntity? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return other.Score.CompareTo(Score);
    }

    public override IEnumerable<ColoredString> ToColoredStringList()
    {
        return base.ToColoredStringList().Append(new ColoredString($"Score: {Score}", ConsoleColor.Magenta));
    }
}