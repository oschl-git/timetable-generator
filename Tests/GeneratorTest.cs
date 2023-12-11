using TimetableGenerator.TimetableGeneration;

namespace TimetableGenerator.Tests;

using NUnit.Framework;

[TestFixture]
public class GeneratorTest
{
    [Test]
    public void TestGenerating()
    {
        for (var i = 0; i < 10; i++) Assert.DoesNotThrow(() => Generator.GeneratePossibleTimetable());
    }
}