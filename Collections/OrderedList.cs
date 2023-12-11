namespace TimetableGenerator.Collections;

/// <summary>
/// A List with an extension method that adds each item to its correct sorted position by the default comparer.
/// </summary>
/// <typeparam name="T">Any data type.</typeparam>
public class OrderedList<T> : List<T>
{
    /// <summary>
    /// Adds item to its correct sorted position by the default comparer.
    /// https://stackoverflow.com/a/46294791
    /// </summary>
    /// <param name="item">The item to add.</param>
    public new void Add(T item)
    {
        var x = BinarySearch(item);
        Insert(x >= 0 ? x : ~x, item);
    }
}