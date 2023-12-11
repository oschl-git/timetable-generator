using TimetableGenerator.Collections;

namespace TimetableGenerator.Tests;

using NUnit.Framework;

[TestFixture]
public class OrderedListTest
{
    [Test]
    public void TestAddingItems()
    {
        var orderedList = new OrderedList<int>();
        var expectedList = new List<int> { 1, 4, 5, 10 };
        
        orderedList.Add(10);
        orderedList.Add(1);
        orderedList.Add(5);
        orderedList.Add(4);
        
        Assert.That(orderedList, Is.EqualTo(expectedList));
    }
}