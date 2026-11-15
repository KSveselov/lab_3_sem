using System;
using NUnit.Framework;

namespace Recognizer.Tests;

[TestFixture]
public class ThresholdFilterTests
{
    [Test]
    public void ThresholdFilter_IncludesAllPixelsEqualToThreshold_WithoutChangingOriginal()
    {
        var image = new double[,] { { 0.6, 0.1, 0.9 }, { 0.3, 0.6, 0.2 } };
        var unchanged = (double[,])image.Clone();
        var expected = new double[,] { { 1, 0, 1 }, { 0, 1, 0 } };

        var result = ThresholdFilterTask.ThresholdFilter(image, 0.4);

        Assert.That(result, Is.EqualTo(expected));
        Assert.That(image, Is.EqualTo(unchanged));
    }

    [TestCase(0, 0, 0, 0)]
    [TestCase(0.2, 0, 0, 0)]
    [TestCase(0.5, 0, 0, 1)]
    [TestCase(1, 1, 1, 1)]
    public void ThresholdFilter_UsesTruncatedPixelCount(
        double fraction, double first, double second, double third)
    {
        var image = new double[,] { { 0.1, 0.5, 0.9 } };

        Assert.That(ThresholdFilterTask.ThresholdFilter(image, fraction),
            Is.EqualTo(new double[,] { { first, second, third } }));
    }

    [Test]
    public void ThresholdFilter_PreservesEmptyImageDimensions()
    {
        var result = ThresholdFilterTask.ThresholdFilter(new double[0, 3], 1);

        Assert.That(result.GetLength(0), Is.Zero);
        Assert.That(result.GetLength(1), Is.EqualTo(3));
    }

    [TestCase(-0.1)]
    [TestCase(1.1)]
    [TestCase(double.NaN)]
    public void ThresholdFilter_RejectsInvalidFraction(double fraction)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            ThresholdFilterTask.ThresholdFilter(new double[1, 1], fraction));
    }
}
