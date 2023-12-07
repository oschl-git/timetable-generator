namespace TimetableGenerator.Helpers;

public abstract class CollectionRandomizer
{
    public static List<T> ShuffleList<T>(IEnumerable<T> list)
    {
        var shuffledList = list.ToList();
        var random = new Random();
        var n = shuffledList.Count;

        for (var i = n - 1; i > 0; i--)
        {
            var j = random.Next(0, i + 1);
            (shuffledList[i], shuffledList[j]) = (shuffledList[j], shuffledList[i]);
        }

        return shuffledList;
    }
}