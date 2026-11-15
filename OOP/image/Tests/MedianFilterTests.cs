using NUnit.Framework;

namespace Recognizer.Tests;

[TestFixture]
public class MedianFilterTests
{
    [Test]
    public void MedianFilter_ProcessesCornersEdgesAndInterior_WithoutChangingOriginal()
    {
        var original = new double[,] { { 9, 1, 8 }, { 2, 7, 3 }, { 6, 4, 5 } };
        var unchanged = (double[,])original.Clone();
        var expected = new double[,] { { 4.5, 5, 5 }, { 5, 5, 4.5 }, { 5, 4.5, 4.5 } };

        var result = MedianFilterTask.MedianFilter(original);

        Assert.That(result, Is.EqualTo(expected));
        Assert.That(original, Is.EqualTo(unchanged));
    }

    [Test]
    public void MedianFilter_ProcessesSingleRowAndSingleColumn()
    {
        var row = new double[,] { { 9 }, { 1 }, { 5 } };
        var column = new double[,] { { 9, 1, 5 } };

        Assert.That(MedianFilterTask.MedianFilter(row),
            Is.EqualTo(new double[,] { { 5 }, { 5 }, { 3 } }));
        Assert.That(MedianFilterTask.MedianFilter(column),
            Is.EqualTo(new double[,] { { 5, 5, 3 } }));
        Assert.That(MedianFilterTask.MedianFilter(new double[,] { { 7 } }),
            Is.EqualTo(new double[,] { { 7 } }));
    }

    [TestCase(0, 0)]
    [TestCase(0, 3)]
    [TestCase(3, 0)]
    public void MedianFilter_PreservesEmptyImageDimensions(int width, int height)
    {
        var result = MedianFilterTask.MedianFilter(new double[width, height]);

        Assert.That(result.GetLength(0), Is.EqualTo(width));
        Assert.That(result.GetLength(1), Is.EqualTo(height));
    }
}
