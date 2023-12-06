namespace TimetableGenerator.Helpers;

public class CollectionRandomizer
{
    public static IEnumerable<T> ShuffleArray<T>(T[] inputArray)
    {
        var array = (T[])inputArray.Clone();

        var random = new Random();
        for (var i = array.Length - 1; i > 0; i--)
        {
            var randomIndex = random.Next(0, i + 1);
            (array[i], array[randomIndex]) = (array[randomIndex], array[i]);
        }

        return array;
    }

    public static Dictionary<TKey, TValue> ShuffleDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary) where TKey : notnull
    {
        var list = dictionary.ToList();

        var random = new Random();
        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = random.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }

        var shuffledDictionary = list.ToDictionary(pair => pair.Key, pair => pair.Value);

        return shuffledDictionary;
    }
}