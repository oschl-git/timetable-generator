using TimetableGenerator.Helpers;

namespace TimetableGenerator.Tests;

using NUnit.Framework;

[TestFixture]
public class StringProcessorTest
{
    [Test]
    public void TestAddingFillerSpaces()
    {
        const string expectedString = "Karel     ";
        var actualString = StringProcessor.AddFillerSpaces("Karel", 10);
        
        Assert.That(expectedString, Is.EqualTo(actualString));
    }
}