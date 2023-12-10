namespace TimetableGenerator.Helpers;

/// <summary>
/// Abstract class with functions for random operations with collections.
/// </summary>
public abstract class CollectionRandomizer
{
    /// <summary>
    /// Returns a shuffled version of the provided list.
    /// </summary>
    /// <param name="list">The list to shuffle.</param>
    /// <typeparam name="T">The data type of items inside the list.</typeparam>
    /// <returns>The shuffled list.</returns>
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