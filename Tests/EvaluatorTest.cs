using TimetableGenerator.TimetableGeneration;
using TimetableGenerator.TimetableGeneration.Entities;

namespace TimetableGenerator.Tests;

using NUnit.Framework;

[TestFixture]
public class EvaluatorTest
{
    [Test]
    public void TestEvaluating()
    {
        var originalTimetable = new OriginalTimetableEntity();
        const int expectedScore = -100;

        var actualScore = Evaluator.EvaluateTimetable(originalTimetable);
        
        Assert.That(actualScore, Is.EqualTo(expectedScore));
    }
}