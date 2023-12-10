namespace TimetableGenerator.Collections;

public class OrderedList<T> : List<T>
{
    public new void Add(T item)
    {
        var x = BinarySearch(item);
        Insert((x >= 0) ? x : ~x, item);
    }
}